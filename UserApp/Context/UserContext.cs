using Microsoft.EntityFrameworkCore;
using UserApp.Model;

namespace UserApp.Context
{
    public class UserContext : DbContext
    {
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        private string _connectionString;

        public UserContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies().UseNpgsql(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
           
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("users_pkey");
                entity.HasIndex(e => e.Email).IsUnique();

                entity.ToTable("users");

                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.Password).HasColumnName("password");
                entity.Property(e => e.RoleId).HasConversion<int>();
            });

            modelBuilder.Entity<Role>().Property(e => e.RoleId).HasConversion<int>();

            modelBuilder
                .Entity<Role>()
                .HasData(
                    Enum.GetValues(typeof(RoleId))
                        .Cast<RoleId>()
                        .Select(e => new Role
                        {
                            RoleId = e,
                            Name = e.ToString()
                        }));
        }


    }
}
