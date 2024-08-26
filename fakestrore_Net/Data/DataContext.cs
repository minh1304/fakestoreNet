using Microsoft.EntityFrameworkCore;

namespace fakestrore_Net.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderProduct> OrderProducts { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CartProduct>()
                .HasKey(op => new { op.ProductId, op.CartId });

            modelBuilder.Entity<CartProduct>()
                .HasOne(op => op.Cart)
                .WithMany(o => o.CartProducts)
                .HasForeignKey(op => op.CartId);

            modelBuilder.Entity<CartProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.CartProducts)
                .HasForeignKey(op => op.ProductId);


            modelBuilder.Entity<OrderProduct>()
        .       HasKey(op => new { op.OrderId, op.ProductId });

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Order)
                .WithMany(o => o.OrderProducts)
                .HasForeignKey(op => op.OrderId);

            modelBuilder.Entity<OrderProduct>()
                .HasOne(op => op.Product)
                .WithMany(p => p.OrderProducts)
                .HasForeignKey(op => op.ProductId);

            // Configure other entity relationships...

            base.OnModelCreating(modelBuilder);
        }
    }
}
