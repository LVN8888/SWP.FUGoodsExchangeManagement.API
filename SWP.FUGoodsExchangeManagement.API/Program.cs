using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SWP.FUGoodsExchangeManagement.API.Hubs;
using SWP.FUGoodsExchangeManagement.API.Middleware;
using SWP.FUGoodsExchangeManagement.Business.Service.AuthenticationServices;
using SWP.FUGoodsExchangeManagement.Business.Service.CampusServices;
using SWP.FUGoodsExchangeManagement.Business.Service.CategoryServices;
using SWP.FUGoodsExchangeManagement.Business.Service.ChatServices;
using SWP.FUGoodsExchangeManagement.Business.Service.MailServices;
using SWP.FUGoodsExchangeManagement.Business.Service.OTPServices;
using SWP.FUGoodsExchangeManagement.Business.Service.PaymentServices;
using SWP.FUGoodsExchangeManagement.Business.Service.PostApplyServices;
using SWP.FUGoodsExchangeManagement.Business.Service.PostModeServices;
using SWP.FUGoodsExchangeManagement.Business.Service.ProductPostServices;
using SWP.FUGoodsExchangeManagement.Business.Service.ReportServices;
using SWP.FUGoodsExchangeManagement.Business.Service.SecretServices;
using SWP.FUGoodsExchangeManagement.Business.Service.StatisticalServices;
using SWP.FUGoodsExchangeManagement.Business.Service.UserServices;
using SWP.FUGoodsExchangeManagement.Business.VnPayService;
using SWP.FUGoodsExchangeManagement.Repository.Mappers;
using SWP.FUGoodsExchangeManagement.Repository.Models;
using SWP.FUGoodsExchangeManagement.Repository.Repository.CampusRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.CategoryRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ChatDetailRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ChatRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.OTPRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.PaymentRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.PostApplyRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.PostModeRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ProductImagesRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ProductPostRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ProductPostRepository;
using SWP.FUGoodsExchangeManagement.Repository.Repository.ReportRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.TokenRepositories;
using SWP.FUGoodsExchangeManagement.Repository.Repository.UserRepositories;
using SWP.FUGoodsExchangeManagement.Repository.UnitOfWork;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// =============================================================================================================
builder.Services.AddDbContext<FugoodsExchangeManagementContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddSignalR();
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

builder.Services.AddSingleton<GlobalExceptionMiddleware>();
builder.Services.AddSingleton<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IMailService, MailService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IOTPService, OTPService>();
builder.Services.AddScoped<ICampusService, CampusService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IProductPostService, ProductPostService>();
builder.Services.AddScoped<IPostModeService, PostModeService>();
builder.Services.AddScoped<IStatisticalService, StatisticalService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IPostApplyService, PostApplyService>();
builder.Services.AddScoped<IVnPayService, VnPayService>();
builder.Services.AddScoped<IChatService, ChatService>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<ITokenRepository, TokenRepository>();
builder.Services.AddTransient<IOTPRepository, OTPRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<ICampusRepository, CampusRepository>();
builder.Services.AddTransient<IProductImagesRepository, ProductImagesRepository>();
builder.Services.AddTransient<IProductPostRepository, ProductPostRepository>();
builder.Services.AddTransient<IPaymentRepository, PaymentRepository>();
builder.Services.AddTransient<IPostModeRepository, PostModeRepository>();
builder.Services.AddTransient<IPostApplyRepository, PostApplyRepository>();
builder.Services.AddTransient<IReportRepository, ReportRepository>();
builder.Services.AddTransient<IChatRepository, ChatRepository>();
builder.Services.AddTransient<IChatDetailRepository, ChatDetailRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "AllowAll",
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});

builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer("Bearer", options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:JwtKey"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,
            ValidateLifetime = true,
        };
    });

// Add authorization
builder.Services.AddAuthorization();

// Add Swagger
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
         {
            new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
            new string[] {}
         }
     });
});
// =============================================================================================================

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
//{
app.UseSwagger();
app.UseSwaggerUI();
//}

app.UseHttpsRedirection();

// Add CORS
app.UseCors("AllowAll");

app.UseAuthorization();

// Add middleware
app.UseMiddleware<GlobalExceptionMiddleware>();

app.MapControllers();

app.MapHub<ChatHub>("/chathub");

app.Run();
