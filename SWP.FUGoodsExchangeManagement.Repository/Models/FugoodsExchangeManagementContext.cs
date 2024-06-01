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

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<PostApply> PostApplies { get; set; }

    public virtual DbSet<PostMode> PostModes { get; set; }

    public virtual DbSet<ProductImage> ProductImages { get; set; }

    public virtual DbSet<ProductPost> ProductPosts { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Campus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Campus__3214EC0791E0429A");

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
            entity.HasKey(e => e.Id).HasName("PK__Category__3214EC07BE2F0EC9");

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
            entity.HasKey(e => e.Id).HasName("PK__Chat__3214EC07AF6BFC5C");

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
                .HasConstraintName("FK__Chat__BuyerId__440B1D61");

            entity.HasOne(d => d.ProductPost).WithMany(p => p.Chats)
                .HasForeignKey(d => d.ProductPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Chat__ProductPos__4222D4EF");
        });

        modelBuilder.Entity<ChatDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ChatDeta__3214EC07C28A5BE2");

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
                .HasConstraintName("FK__ChatDetai__ChatI__4316F928");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payment__3214EC078F64EB19");

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
                .HasConstraintName("FK__Payment__PostMod__403A8C7D");

            entity.HasOne(d => d.ProductPost).WithMany(p => p.Payments)
                .HasForeignKey(d => d.ProductPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Payment__Product__3C69FB99");
        });

        modelBuilder.Entity<PostApply>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostAppl__3214EC07F09A8A21");

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
                .HasConstraintName("FK__PostApply__Buyer__37A5467C");

            entity.HasOne(d => d.ProductPost).WithMany(p => p.PostApplies)
                .HasForeignKey(d => d.ProductPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PostApply__Produ__3A81B327");
        });

        modelBuilder.Entity<PostMode>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PostMode__3214EC07C6B1F16B");

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
            entity.HasKey(e => e.Id).HasName("PK__ProductI__3214EC070054E086");

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
                .HasConstraintName("FK__ProductIm__Produ__3D5E1FD2");
        });

        modelBuilder.Entity<ProductPost>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__ProductP__3214EC0744F7DED1");

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
                .HasConstraintName("FK__ProductPo__Campu__3E52440B");

            entity.HasOne(d => d.Category).WithMany(p => p.ProductPosts)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPo__Categ__412EB0B6");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.ProductPosts)
                .HasForeignKey(d => d.CreatedBy)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPo__Creat__398D8EEE");

            entity.HasOne(d => d.PostMode).WithMany(p => p.ProductPosts)
                .HasForeignKey(d => d.PostModeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductPo__PostM__3F466844");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Report__3214EC0781570B97");

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
                .HasConstraintName("FK__Report__CreatedB__38996AB5");

            entity.HasOne(d => d.ProductPost).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ProductPostId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Report__ProductP__3B75D760");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__User__3214EC07635784BE");

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
                .HasMaxLength(255)
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
