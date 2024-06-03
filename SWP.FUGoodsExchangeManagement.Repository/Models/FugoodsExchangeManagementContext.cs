using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace SWP.FUGoodsExchangeManagement.Repository.Models;

public partial class FugoodsExchangeManagementContext : DbContext
{
    public FugoodsExchangeManagementContext()
    {
    }

    public FugoodsExchangeManagementContext(DbContextOptions<FugoodsExchangeManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Campus> Campuses { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<ChatDetail> ChatDetails { get; set; }

    public virtual DbSet<Otpcode> Otpcodes { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PostApply> PostApplies { get; set; }

    public virtual DbSet<PostMode> PostModes { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductPost> ProductPosts { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Campus__3214EC0747FAEA24");

            entity.ToTable("Campus");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC0708712A4F");

            entity.ToTable("Category");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Chat__3214EC07E14D3471");

            entity.ToTable("Chat");

            entity.HasIndex(e => e.ProductPostId, "idx_chat_productpostid");

            entity.HasIndex(e => e.BuyerId, "idx_chat_userid");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.BuyerId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ProductPostId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Buyer).WithMany(p => p.Chats)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Chat__BuyerId__5629CD9C");

            entity.HasOne(d => d.ProductPost).WithMany(p => p.Chats)
                .HasForeignKey(d => d.ProductPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Chat__ProductPos__73BA3083");
        });

        modelBuilder.Entity<ChatDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChatDeta__3214EC078FA4DC87");

            entity.HasIndex(e => e.ChatId, "idx_chatdetails_chatid");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ChatId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Time).HasColumnType("datetime");

            entity.HasOne(d => d.Chat).WithMany(p => p.ChatDetails)
                .HasForeignKey(d => d.ChatId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ChatDetai__ChatI__75A278F5");
        });

        modelBuilder.Entity<Otpcode>(entity =>
        {
            entity.ToTable("OTPCode");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreatedAt).HasColumnType("datetime");
            entity.Property(e => e.Otp)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength()
                .HasColumnName("OTP");
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.User).WithMany(p => p.Otpcodes)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OTPCode_User");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC0766149180");

            entity.ToTable("Payment");

            entity.HasIndex(e => e.PostModeId, "idx_payment_postmodeid");

            entity.HasIndex(e => e.ProductPostId, "idx_payment_productpostid");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PostModeId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Price)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductPostId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.PostMode).WithMany(p => p.Payments)
                .HasForeignKey(d => d.PostModeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__PostMod__787EE5A0");

            entity.HasOne(d => d.ProductPost).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ProductPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__Product__778AC167");
        });

        modelBuilder.Entity<PostApply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostAppl__3214EC07EBB69B17");

            entity.ToTable("PostApply");

            entity.HasIndex(e => e.ProductPostId, "idx_postapply_productpostid");

            entity.HasIndex(e => e.BuyerId, "idx_postapply_userid");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.BuyerId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Message)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.ProductPostId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.Buyer).WithMany(p => p.PostApplies)
                .HasForeignKey(d => d.BuyerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostApply__Buyer__49C3F6B7");

            entity.HasOne(d => d.ProductPost).WithMany(p => p.PostApplies)
                .HasForeignKey(d => d.ProductPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostApply__Produ__797309D9");
        });

        modelBuilder.Entity<PostMode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostMode__3214EC07ACF31163");

            entity.ToTable("PostMode");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Duration)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Price)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Type)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<ProductImage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3214EC073D6B35B6");

            entity.HasIndex(e => e.ProductPostId, "idx_productimages_productpostid");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ProductPostId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Url)
                .HasMaxLength(500)
                .IsUnicode(false);

            entity.HasOne(d => d.ProductPost).WithMany(p => p.ProductImages)
                .HasForeignKey(d => d.ProductPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductIm__Produ__7B5B524B");
        });

        modelBuilder.Entity<ProductPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductP__3214EC07D63DEE42");

            entity.ToTable("ProductPost");

            entity.HasIndex(e => e.CategoryId, "idx_productpost_categoryid");

            entity.HasIndex(e => e.CreatedBy, "idx_productpost_userid");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CampusId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CategoryId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Description)
                .HasMaxLength(2048)
                .IsUnicode(false);
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.PostModeId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title)
                .HasMaxLength(100)
                .IsUnicode(false);

            entity.HasOne(d => d.Campus).WithMany(p => p.ProductPosts)
                .HasForeignKey(d => d.CampusId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPo__Campu__7C4F7684");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductPosts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPo__Categ__7E37BEF6");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductPosts)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPo__Creat__4BAC3F29");

            entity.HasOne(d => d.PostMode).WithMany(p => p.ProductPosts)
                .HasForeignKey(d => d.PostModeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPo__PostM__7D439ABD");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.ExpiredDate).HasColumnType("datetime");
            entity.Property(e => e.RefreshToken1)
                .IsUnicode(false)
                .HasColumnName("RefreshToken");
            entity.Property(e => e.UserId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshToken_User");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Report__3214EC07F6812D3F");

            entity.ToTable("Report");

            entity.HasIndex(e => e.ProductPostId, "idx_report_productpostid");

            entity.HasIndex(e => e.CreatedBy, "idx_report_userid");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Content)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.ProductPostId)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Reports)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__CreatedB__4AB81AF0");

            entity.HasOne(d => d.ProductPost).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ProductPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__ProductP__01142BA1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC071D94A641");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "idx_user_email");

            entity.Property(e => e.Id)
                .HasMaxLength(36)
                .IsUnicode(false)
                .IsFixedLength();
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Fullname)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(12)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(36)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
