using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EasyHousingSolutionWebAPI.Models;

public partial class EasyHousingDbContext : DbContext
{
    public EasyHousingDbContext()
    {
    }

    public EasyHousingDbContext(DbContextOptions<EasyHousingDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Buyer> Buyers { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Image> Images { get; set; }

    public virtual DbSet<Property> Properties { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }

    public virtual DbSet<State> States { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=.\\sqlexpress;Trusted_Connection=True;Database=EasyHousingDb;TrustServerCertificate=yes");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Buyer>(entity =>
        {
            entity.HasKey(e => e.BuyerId).HasName("pk_BuyerId");

            entity.ToTable("Buyer");

            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(25)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("pk_CartId");

            entity.ToTable("Cart");

            //entity.HasOne(d => d.Buyer).WithMany(p => p.Carts)
            //    .HasForeignKey(d => d.BuyerId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_CartBuyerId");

            //entity.HasOne(d => d.Property).WithMany(p => p.Carts)
            //    .HasForeignKey(d => d.PropertyId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_CartPropertyId");
        });

        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("pk_CityId");

            entity.ToTable("City");

            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .IsUnicode(false);

            //entity.HasOne(d => d.State).WithMany(p => p.Cities)
            //    .HasForeignKey(d => d.StateId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_StateId");
        });

        modelBuilder.Entity<Image>(entity =>
        {
            entity.HasKey(e => e.ImageId).HasName("pk_ImageId");

            entity.Property(e => e.Image1)
                .HasColumnType("image")
                .HasColumnName("Image");

            //entity.HasOne(d => d.Property).WithMany(p => p.Images)
            //    .HasForeignKey(d => d.PropertyId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_ImagesPropertyId");
        });

        modelBuilder.Entity<Property>(entity =>
        {
            entity.HasKey(e => e.PropertyId).HasName("pk_PropertyId");

            entity.ToTable("Property");

            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.Description)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.InitialDeposit).HasColumnType("money");
            entity.Property(e => e.Landmark)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PriceRange).HasColumnType("money");
            entity.Property(e => e.PropertyName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PropertyOption)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.PropertyType)
                .HasMaxLength(15)
                .IsUnicode(false);

            //entity.HasOne(d => d.Seller).WithMany(p => p.Properties)
            //    .HasForeignKey(d => d.SellerId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_PropertySellerId");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasKey(e => e.SellerId).HasName("pk_SellerId");

            entity.ToTable("Seller");

            entity.Property(e => e.Address)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EmailId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNo)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(25)
                .IsUnicode(false);

            //entity.HasOne(d => d.City).WithMany(p => p.Sellers)
            //    .HasForeignKey(d => d.CityId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_SellerCityId");

            //entity.HasOne(d => d.State).WithMany(p => p.Sellers)
            //    .HasForeignKey(d => d.StateId)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_SellerStateId");

            //entity.HasOne(d => d.UserNameNavigation).WithMany(p => p.Sellers)
            //    .HasForeignKey(d => d.UserName)
            //    .OnDelete(DeleteBehavior.ClientSetNull)
            //    .HasConstraintName("fk_SellerUserName");
        });

        modelBuilder.Entity<State>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("pk_StateId");

            entity.ToTable("State");

            entity.Property(e => e.StateName)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("pk_UserName");

            entity.Property(e => e.UserName)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.Password)
                .HasMaxLength(25)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
