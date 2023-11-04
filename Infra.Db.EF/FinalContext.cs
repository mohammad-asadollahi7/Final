using Domain.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.Db.EF;

public class FinalContext : IdentityDbContext<ApplicationUser, IdentityRole<int>, int>
{
    public FinalContext()
    {
    }

    public FinalContext(DbContextOptions<FinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Auction> Auctions { get; set; }

    public virtual DbSet<AuctionOrder> AuctionOrders { get; set; }

    public virtual DbSet<Booth> Booths { get; set; }

    public virtual DbSet<Cart> Carts { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Comment> Comments { get; set; }

    public virtual DbSet<CustomAttributes> CustomAttributes { get; set; }

    public virtual DbSet<CustomAttributesTemplate> CustomAttributesTemplates { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<NonAuctionPrice> NonAuctionPrices { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<Picture> Pictures { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<ProductInventory> ProductInventories { get; set; }

    public virtual DbSet<ProductPicture> ProductPictures { get; set; }

    public virtual DbSet<ProductsBooth> ProductsBooths { get; set; }

    public virtual DbSet<Seller> Sellers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.City).HasMaxLength(50);
            entity.Property(e => e.Province).HasMaxLength(50);
            entity.Property(e => e.Street).HasMaxLength(50);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Address)
                .HasPrincipalKey<Customer>(p => p.AddressId)
                .HasForeignKey<Address>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Addresses_Customers");
        });

        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasIndex(e => e.ApplicationUserId, "IX_Admins_ApplicationUserId").IsUnique();

            entity.HasOne(d => d.ApplicationUser).WithOne(p => p.Admin).HasForeignKey<Admin>(d => d.ApplicationUserId);
        });


        modelBuilder.Entity<Auction>(entity =>
        {
            entity.ToTable("Auction");
            entity.HasIndex(e => e.ProductId, "IX_Auction").IsUnique();

            entity.Property(e => e.FromDate).HasColumnType("datetime");
            entity.Property(e => e.MinPrice).HasColumnType("decimal(18, 0)");
            entity.Property(e => e.ToDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<AuctionOrder>(entity =>
        {
            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Customer).WithMany(p => p.AuctionOrders)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuctionOrders_Customers");

            entity.HasOne(d => d.Product).WithMany(p => p.AuctionOrders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AuctionOrders_Products");
        });

        modelBuilder.Entity<Booth>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.Medal)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Booth)
                .HasForeignKey<Booth>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Booths_Pictures");
        });

        modelBuilder.Entity<Cart>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Carts_1");

            entity.Property(e => e.OrderAt).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.Carts)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Carts_Customers1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasIndex(e => e.PictureId, "IX_Categories_PictureId").IsUnique();

            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Picture).WithOne(p => p.Category)
                .HasForeignKey<Category>(d => d.PictureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Category_Pictures");
        });

        modelBuilder.Entity<Comment>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.Description).HasMaxLength(255);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.SubmittedDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany()
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Customers");

            entity.HasOne(d => d.Product).WithMany()
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Comments_Products1");
        });

        modelBuilder.Entity<CustomAttributes>(entity =>
        {
            entity.HasIndex(e => e.ProductId, "IX_CustomAttributes_ProductId");

            entity.Property(e => e.AttributeTitle).HasMaxLength(255);
            entity.Property(e => e.AttributeValue).HasMaxLength(255);

            entity.HasOne(d => d.Product).WithMany(p => p.CustomAttributes)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_CustomAttributes_Products");
        });

        modelBuilder.Entity<CustomAttributesTemplate>(entity =>
        {
            entity.ToTable("CustomAttributesTemplate");

            entity.HasIndex(e => e.CategoryId, "IX_CustomAttributesTemplate_CategoryId");

            entity.HasOne(d => d.Category).WithMany(p => p.CustomAttributesTemplates)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CustomAttributesTemplate_Categories");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Customers_1");

            entity.HasIndex(e => e.ApplicationUserId, "IX_Customers").IsUnique();

            entity.HasIndex(e => e.AddressId, "IX_Customers_1").IsUnique();

            entity.HasOne(d => d.ApplicationUser).WithOne(p => p.Customer)
                .HasForeignKey<Customer>(d => d.ApplicationUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Customers_AspNetUsers");
        });

        modelBuilder.Entity<NonAuctionPrice>(entity =>
        {
            entity.ToTable("NonAuctionPrice");

            entity.HasIndex(e => e.ProductId, "IX_NonAuctionPrice").IsUnique();

            entity.Property(e => e.Price).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Product).WithOne(p => p.NonAuctionPrice)
                .HasForeignKey<NonAuctionPrice>(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NonAuctionPrice_Products");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_NonAuctionOrders");

            entity.Property(e => e.DiscountedPrice).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Cart).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CartId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_NonAuctionOrders_Carts");

            entity.HasOne(d => d.Product).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Orders_Products");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.EnglishTitle)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.PersianTitle).HasMaxLength(255);

            entity.HasOne(d => d.Booth).WithMany(p => p.Products)
                .HasForeignKey(d => d.BoothId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Booths");

            entity.HasOne(d => d.Auction).WithOne(p => p.Product)
                .HasPrincipalKey<Auction>(p => p.ProductId)
                .HasForeignKey<Product>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Products_Auction");

            entity.HasMany(d => d.Categories).WithMany(p => p.Products)
                .UsingEntity<Dictionary<string, object>>(
                    "ProductsCategory",
                    r => r.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProductsCategories_Categories"),
                    l => l.HasOne<Product>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_ProductsCategories_Products"),
                    j =>
                    {
                        j.HasKey("ProductId", "CategoryId");
                        j.ToTable("ProductsCategories");
                        j.HasIndex(new[] { "CategoryId" }, "IX_ProductsCategories_CategoryId");
                    });
        });

        modelBuilder.Entity<ProductInventory>(entity =>
        {
            entity.ToTable("ProductInventory");

            entity.HasIndex(e => e.ProductId, "IX_ProductInventory_ProductId");

            entity.Property(e => e.IsSold).HasColumnName("isSold");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductInventories)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductInventory_Products1");
        });

        modelBuilder.Entity<ProductPicture>(entity =>
        {
            entity.HasIndex(e => e.PictureId, "IX_ProductPictures").IsUnique();

            entity.HasIndex(e => e.ProductId, "IX_ProductPictures_ProductId");

            entity.HasOne(d => d.Picture).WithOne(p => p.ProductPicture)
                .HasForeignKey<ProductPicture>(d => d.PictureId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductPictures_Pictures");

            entity.HasOne(d => d.Product).WithMany(p => p.ProductPictures)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductPictures_Products");
        });

        modelBuilder.Entity<ProductsBooth>(entity =>
        {
            entity.Property(e => e.Type).HasComment("0 equal NonAuction\r\n1 equal Auction\r\n");

            entity.HasOne(d => d.Booth).WithMany(p => p.ProductsBooths)
                .HasForeignKey(d => d.BoothId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ProductsBooths_Booths");
        });

        modelBuilder.Entity<Seller>(entity =>
        {
            entity.HasIndex(e => e.ApplicationUserId, "IX_Sellers").IsUnique();

            entity.HasIndex(e => e.AddressId, "IX_Sellers_1").IsUnique();

            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.HasOne(d => d.Address).WithOne(p => p.Seller)
                .HasForeignKey<Seller>(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sellers_Addresses");

            entity.HasOne(d => d.ApplicationUser).WithOne(p => p.Seller)
                .HasForeignKey<Seller>(d => d.ApplicationUserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sellers_AspNetUsers");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Seller)
                .HasForeignKey<Seller>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Sellers_Booths");
        });

        base.OnModelCreating(modelBuilder);
    }

}
