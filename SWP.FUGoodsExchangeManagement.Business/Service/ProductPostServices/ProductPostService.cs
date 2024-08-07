﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using SWP.FUGoodsExchangeManagement.Business.Service.AuthenticationServices;
using SWP.FUGoodsExchangeManagement.Business.Utils;
using SWP.FUGoodsExchangeManagement.Business.VnPayService;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.PaymentDTOs.Response;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.RequestModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.ProductPostDTOs.ResponseModels;
using SWP.FUGoodsExchangeManagement.Repository.DTOs.VnPayDTOs;
using SWP.FUGoodsExchangeManagement.Repository.Enums;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ProductPostRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ProductPostRepository;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SWP.FUGoodsExchangeManagement.Business.Service.ProductPostServices
{
    public class ProductPostService : IProductPostService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAuthenticationService _authenticationService;

        private const int ItemPerPage = 8;

        public ProductPostService(IUnitOfWork unitOfWork, IMapper mapper, IAuthenticationService authenticationService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _authenticationService = authenticationService;
        }

        public async Task<string> CreateWaitingProductPost(ProductPostCreateRequestModel requestModel, string token)
        {
            var userId = _authenticationService.decodeToken(token, "userId");

            ProductPost newProductPost = _mapper.Map<ProductPost>(requestModel);
            var postId = Guid.NewGuid().ToString();
            newProductPost.Id = postId;
            newProductPost.Status = ProductPostStatus.Unpaid.ToString();
            newProductPost.CreatedBy = userId;
            newProductPost.CreatedDate = DateTime.Now;
            var chosenPostMode = await _unitOfWork.PostModeRepository.GetSingle(p => p.Id.Equals(requestModel.PostModeId));

            var newPayment = new Payment
            {
                Id = Guid.NewGuid().ToString(),
                PaymentDate = DateTime.Now,
                Price = chosenPostMode.Price,
                ProductPostId = postId,
                PostModeId = requestModel.PostModeId,
                Status = PaymentStatus.Pending.ToString()
            };


            var postImages = new List<ProductImage>();
            foreach (var url in requestModel.ImagesUrl)
            {
                postImages.Add(new ProductImage
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductPostId = postId,
                    Url = url
                });
            }

            await _unitOfWork.ProductPostRepository.Insert(newProductPost);
            await _unitOfWork.PaymentRepository.Insert(newPayment);
            await _unitOfWork.ProductImagesRepository.InsertRange(postImages);
            await _unitOfWork.SaveChangeAsync();

            return newPayment.Id;
        }

        public async Task<List<ProductPostResponseModel>> ViewAllPostWithStatus(int? pageIndex, PostSearchModel searchModel, string status)
        {
            return await ViewAllPostWithStatus(pageIndex, status, searchModel, null, 0);
        }

        public async Task<List<ProductPostResponseModel>> ViewOwnPostWithStatus(int? pageIndex, PostSearchModel searchModel, string status, string token)
        {
            return await ViewAllPostWithStatus(pageIndex, status, searchModel, token, 1);
        }

        public async Task<List<ProductPostResponseModel>> ViewOwnPostExceptMine(int? pageIndex, PostSearchModel searchModel, string token)
        {
            string userId = null;
            if (token != null)
            {
                userId = _authenticationService.decodeToken(token, "userId");
            }
            Func<IQueryable<ProductPost>, IOrderedQueryable<ProductPost>> orderBy;
            orderBy = o => o.OrderBy(p => p.Price).ThenBy(p => p.CreatedDate);
            Expression<Func<ProductPost, bool>> filter;

            filter = p => p.Status.Equals(ProductPostStatus.Open.ToString()) || p.Status.Equals(ProductPostStatus.Pending.ToString());

            if (userId != null)
            {
                filter = filter.And(p => !p.CreatedBy.Equals(userId));
            }

            var listPostApply = await _unitOfWork.PostApplyRepository.Get(p => p.BuyerId.Equals(userId));
            var exceptList = listPostApply.Select(l => l.ProductPostId).ToList();
            if (exceptList.Count() > 0)
            {
                filter = filter.And(p => !exceptList.Contains(p.Id));
            }

            if (searchModel != null)
            {
                if (searchModel.orderPriceDescending.HasValue && searchModel.orderPriceDescending.Value)
                {
                    orderBy = orderBy.AndThen(q => q.OrderByDescending(p => p.Price));
                }
                else if (searchModel.orderPriceDescending.HasValue && !searchModel.orderPriceDescending.Value)
                {
                    orderBy = orderBy.AndThen(q => q.OrderBy(p => p.Price));
                }

                if (searchModel.orderDateDescending.HasValue && searchModel.orderDateDescending.Value)
                {
                    orderBy = orderBy.AndThen(q => q.OrderByDescending(p => p.CreatedDate));
                }
                else if (searchModel.orderDateDescending.HasValue && !searchModel.orderDateDescending.Value)
                {
                    orderBy = orderBy.AndThen(q => q.OrderBy(p => p.CreatedDate));
                }

                if (!searchModel.Campus.IsNullOrEmpty())
                {
                    filter = filter.And(p => p.Campus.Name.ToLower().Equals(searchModel.Campus.ToLower()));
                }
                if (!searchModel.Title.IsNullOrEmpty())
                {
                    filter = filter.And(p => p.Title.ToLower().Contains(searchModel.Title.ToLower()));
                }
                if (!searchModel.Category.IsNullOrEmpty())
                {
                    filter = filter.And(p => p.Category.Name.ToLower().Contains(searchModel.Category.ToLower()));
                }
            }

            var allWaitingPost = await _unitOfWork.ProductPostRepository.Get(filter, orderBy, includeProperties: "Category,PostMode,Campus,CreatedByNavigation", pageIndex ?? 1, ItemPerPage);
            var waitingPostListId = allWaitingPost.Select(a => a.Id).ToList();
            var allImages = await _unitOfWork.ProductImagesRepository.Get(i => waitingPostListId.Contains(i.ProductPostId));

            var responseList = allWaitingPost.Select(a => new ProductPostResponseModel
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Price = a.Price,
                CreatedBy = new PostAuthor
                {
                    FullName = a.CreatedByNavigation.Fullname,
                    Email = a.CreatedByNavigation.Email,
                    PhoneNumber = a.CreatedByNavigation.PhoneNumber
                },
                Category = a.Category.Name,
                Campus = a.Campus.Name,
                CreatedDate = a.CreatedDate,
                ExpiredDate = a.ExpiredDate ?? null,
                PostMode = a.PostMode.Type,
                ImageUrls = allImages.Where(ai => ai.ProductPostId.Equals(a.Id)).Select(ai => ai.Url).ToList(),
            }).ToList();
            return responseList;
        }

        public async Task<ProductPostResponseModel> ViewDetailsOfPost(string id)
        {
            var chosenPost = await _unitOfWork.ProductPostRepository.GetSingle(p => p.Id.Equals(id), includeProperties: "Category,PostMode,Campus,CreatedByNavigation");
            if (chosenPost == null)
            {
                throw new CustomException("The chosen post is not existed");
            }
            var allImages = await _unitOfWork.ProductImagesRepository.Get(i => i.ProductPostId.Equals(id));
            return new ProductPostResponseModel
            {
                Id = chosenPost.Id,
                Title = chosenPost.Title,
                Description = chosenPost.Description,
                Price = chosenPost.Price,
                CreatedBy = new PostAuthor
                {
                    FullName = chosenPost.CreatedByNavigation.Fullname,
                    Email = chosenPost.CreatedByNavigation.Email,
                    PhoneNumber = chosenPost.CreatedByNavigation.PhoneNumber
                },
                Category = chosenPost.Category.Name,
                Campus = chosenPost.Campus.Name,
                CreatedDate = chosenPost.CreatedDate,
                ExpiredDate = chosenPost.ExpiredDate ?? null,
                PostMode = chosenPost.PostMode.Type,
                ImageUrls = allImages.Select(ai => ai.Url).ToList(),
            };
        }

        public async Task UpdateProductPost(string id, ProductPostUpdateRequestModel requestModel)
        {
            var chosenPost = await _unitOfWork.ProductPostRepository.GetSingle(p => p.Id.Equals(id));
            if (chosenPost == null)
            {
                throw new CustomException("The chosen post is not existed");
            }

            if (chosenPost.Status.Equals(ProductPostStatus.Pending.ToString()) || chosenPost.Status.Equals(ProductPostStatus.Closed.ToString()))
            {
                throw new CustomException("The chosen post cannot be edited");
            }
            var allPostImages = await _unitOfWork.ProductImagesRepository.Get(i => i.ProductPostId.Equals(id));
            var imageUrls = allPostImages.Select(i => i.Url).ToList();
            if (requestModel != null)
            {
                _mapper.Map(requestModel, chosenPost);
            }

            var exceptImageUrl = imageUrls.Except(requestModel.ImagesUrl);
            if (exceptImageUrl.Count() > 0)
            {
                var deleteImage = allPostImages.Where(i => exceptImageUrl.Contains(i.Url)).ToList();
                await _unitOfWork.ProductImagesRepository.DeleteRange(deleteImage);
            }

            var addedImageUrl = requestModel.ImagesUrl.Except(imageUrls);
            var addImageList = new List<ProductImage>();
            if (addedImageUrl.Count() > 0)
            {
                foreach (var url in addedImageUrl)
                {
                    addImageList.Add(new ProductImage
                    {
                        Id = Guid.NewGuid().ToString(),
                        ProductPostId = id,
                        Url = url
                    });
                }
                await _unitOfWork.ProductImagesRepository.InsertRange(addImageList);
            }
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<string> ExtendExpiredDate(string id, string postModeId, string token)
        {
            var userId = _authenticationService.decodeToken(token, "userId");
            var chosenPost = await _unitOfWork.ProductPostRepository.GetSingle(p => p.Id.Equals(id));
            if (chosenPost == null)
            {
                throw new CustomException("The chosen post is not existed");
            }

            if (!chosenPost.Status.Equals(ProductPostStatus.Expired.ToString()))
            {
                throw new CustomException("The chosen post cannot be extended");
            }

            var chosenPostMode = await _unitOfWork.PostModeRepository.GetSingle(p => p.Id.Equals(postModeId));

            var newPayment = new Payment
            {
                Id = Guid.NewGuid().ToString(),
                PaymentDate = DateTime.Now,
                Price = chosenPostMode.Price,
                ProductPostId = id,
                PostModeId = postModeId,
                Status = PaymentStatus.Pending.ToString(),
            };
            await _unitOfWork.PaymentRepository.Insert(newPayment);
            await _unitOfWork.SaveChangeAsync();

            return newPayment.Id;
        }

        public async Task ApprovePost(string status, string id)
        {
            if (!status.Equals(ProductPostStatus.Cancel.ToString()) && !status.Equals(ProductPostStatus.Open.ToString()))
            {
                throw new CustomException("Please input valid status");
            }
            var chosenPost = await _unitOfWork.ProductPostRepository.GetSingle(a => a.Id.Equals(id));

            if (chosenPost != null)
            {
                if (!chosenPost.Status.Equals(ProductPostStatus.Waiting.ToString()))
                {
                    throw new CustomException("This post is not in waiting status");
                }
                chosenPost.Status = status;
            }
            else throw new CustomException("There is no existed post with chosen Id");

            var postMode = await _unitOfWork.PostModeRepository.GetSingle(p => p.Id.Equals(chosenPost.PostModeId));
            if (status.Equals(ProductPostStatus.Open.ToString()))
            {
                chosenPost.ExpiredDate = DateTime.Now.AddDays(int.Parse(postMode.Duration));
            }
            _unitOfWork.ProductPostRepository.Update(chosenPost);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task ClosePost(string id, string token, string postApplyId)
        {
            var chosenPost = await _unitOfWork.ProductPostRepository.GetSingle(a => a.Id.Equals(id));
            var userId = _authenticationService.decodeToken(token, "userId");

            if (chosenPost != null)
            {
                if (!chosenPost.Status.Equals(ProductPostStatus.Pending.ToString()))
                {
                    throw new CustomException("This post is not in pending status");
                }

                if (!chosenPost.CreatedBy.Equals(userId))
                {
                    throw new CustomException("This post is not created by you, so you cant close it");
                }

                chosenPost.Status = ProductPostStatus.Closed.ToString();
            }
            else throw new CustomException("There is no existed post with chosen Id");
            var deletePostApplyList = await _unitOfWork.PostApplyRepository.Get(p => !p.Id.Equals(postApplyId) && p.ProductPostId.Equals(id));
            var chosenPostApply = await _unitOfWork.PostApplyRepository.GetSingle(p =>  p.Id.Equals(postApplyId));
            chosenPostApply.Status = PostApplyStatus.Success.ToString();

            await _unitOfWork.PostApplyRepository.DeleteRange(deletePostApplyList.ToList());
            _unitOfWork.ProductPostRepository.Update(chosenPost);
            _unitOfWork.PostApplyRepository.Update(chosenPostApply);

            await _unitOfWork.SaveChangeAsync();
        }

        private async Task<List<ProductPostResponseModel>> ViewAllPostWithStatus(int? pageIndex, string? status, PostSearchModel searchModel, string token, int option)
        {
            string userId = null;
            if (token != null)
            {
                userId = _authenticationService.decodeToken(token, "userId");
            }
            Func<IQueryable<ProductPost>, IOrderedQueryable<ProductPost>> orderBy;
            orderBy = o => o.OrderBy(p => p.Price).ThenBy(p => p.CreatedDate);
            Expression<Func<ProductPost, bool>> filter = p => true;
            if (!status.IsNullOrEmpty())
            {
                if (!Enum.GetNames(typeof(ProductPostStatus)).Contains(status))
                {
                    throw new CustomException("Please input valid status");
                }
                filter = p => p.Status.Equals(status);
            }

            if (userId != null && option == 1)
            {
                filter = filter.And(p => p.CreatedBy.Equals(userId));
            }
            else if (userId != null && option == 0)
            {
                filter = filter.And(p => !p.CreatedBy.Equals(userId));
            }

            if (searchModel != null)
            {
                if (searchModel.orderPriceDescending.HasValue && searchModel.orderPriceDescending.Value)
                {
                    orderBy = orderBy.AndThen(q => q.OrderByDescending(p => p.Price));
                }
                else if (searchModel.orderPriceDescending.HasValue && !searchModel.orderPriceDescending.Value)
                {
                    orderBy = orderBy.AndThen(q => q.OrderBy(p => p.Price));
                }

                if (searchModel.orderDateDescending.HasValue && searchModel.orderDateDescending.Value)
                {
                    orderBy = orderBy.AndThen(q => q.OrderByDescending(p => p.CreatedDate));
                }
                else if (searchModel.orderDateDescending.HasValue && !searchModel.orderDateDescending.Value)
                {
                    orderBy = orderBy.AndThen(q => q.OrderBy(p => p.CreatedDate));
                }

                if (!searchModel.Campus.IsNullOrEmpty())
                {
                    filter = filter.And(p => p.Campus.Name.ToLower().Equals(searchModel.Campus.ToLower()));
                }
                if (!searchModel.Title.IsNullOrEmpty())
                {
                    filter = filter.And(p => p.Title.ToLower().Contains(searchModel.Title.ToLower()));
                }
                if (!searchModel.Category.IsNullOrEmpty())
                {
                    filter = filter.And(p => p.Category.Name.ToLower().Contains(searchModel.Category.ToLower()));
                }
            }

            var allWaitingPost = await _unitOfWork.ProductPostRepository.Get(filter, orderBy, includeProperties: "Category,PostMode,Campus,CreatedByNavigation", pageIndex ?? 1, ItemPerPage);
            var waitingPostListId = allWaitingPost.Select(a => a.Id).ToList();
            var allImages = await _unitOfWork.ProductImagesRepository.Get(i => waitingPostListId.Contains(i.ProductPostId));

            var responseList = allWaitingPost.Select(a => new ProductPostResponseModel
            {
                Id = a.Id,
                Title = a.Title,
                Description = a.Description,
                Price = a.Price,
                Status = a.Status,
                CreatedBy = new PostAuthor
                {
                    FullName = a.CreatedByNavigation.Fullname,
                    Email = a.CreatedByNavigation.Email,
                    PhoneNumber = a.CreatedByNavigation.PhoneNumber
                },
                Category = a.Category.Name,
                Campus = a.Campus.Name,
                CreatedDate = a.CreatedDate,
                ExpiredDate = a.ExpiredDate ?? null,
                PostMode = a.PostMode.Type,
                ImageUrls = allImages.Where(ai => ai.ProductPostId.Equals(a.Id)).Select(ai => ai.Url).ToList(),
            }).ToList();
            return responseList;
        }

        public async Task ExtendExpiredDateAfterPaymentSuccess(string id, string postModeId)
        {
            var chosenPost = await _unitOfWork.ProductPostRepository.GetSingle(p => p.Id.Equals(id));
            if (chosenPost == null)
            {
                throw new CustomException("The chosen post is not existed");
            }

            var chosenPostMode = await _unitOfWork.PostModeRepository.GetSingle(p => p.Id.Equals(postModeId));

            if (chosenPost.Status.Equals(ProductPostStatus.Unpaid.ToString()))
            {
                // waiting means wait for admin approval
                chosenPost.Status = ProductPostStatus.Waiting.ToString();
            }
            else if (chosenPost.Status.Equals(ProductPostStatus.Expired.ToString()))
            {
                // open means post that approved but is expired and then extend
                chosenPost.Status = ProductPostStatus.Open.ToString();
            }
            else
            {
                throw new Exception($"Post status is {chosenPost.Status} and is not allowed to update!");
            }

            chosenPost.ExpiredDate = DateTime.Now.AddDays(int.Parse(chosenPostMode.Duration));
            chosenPost.PostModeId = postModeId;

            _unitOfWork.ProductPostRepository.Update(chosenPost);
            await _unitOfWork.SaveChangeAsync();
        }

        public async Task<List<PaymentResponseModel>> GetPostPaymentRecords(int? pageIndex, string postId)
        {
            var list = await _unitOfWork.PaymentRepository.Get(filter: p => p.ProductPostId.Equals(postId),
                                                                   orderBy: p => p.OrderByDescending(p => p.PaymentDate),
                                                                   pageIndex: pageIndex ?? 1,
                                                                   pageSize: ItemPerPage,
                                                                   includeProperties: "PostMode"
                                                                   );
            var paymentRecords = _mapper.Map<List<PaymentResponseModel>>(list);
            return paymentRecords;
        }
    }
}
