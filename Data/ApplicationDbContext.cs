using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Koeheya.Data
{
    [Table("User")]
    public class ApplicationUser : Microsoft.AspNetCore.Identity.IdentityUser
    {
        [Key]
        [Column("UserId")]
        public int UserId { get; set; }

        [Column("UserName")]
        public override string? UserName { get; set; }
        
        [NotMapped]
        public string? Password { get; set; }

        [NotMapped]
        public override string? Id { get; set; }
        
        [NotMapped]
        public override string? NormalizedUserName { get; set; }
        
        [NotMapped]
        public override string? Email { get; set; }
        
        [NotMapped]
        public override string? NormalizedEmail { get; set; }
        
        [NotMapped]
        public override bool EmailConfirmed { get; set; }
        
        [NotMapped]
        public override string? PasswordHash { get; set; }
        
        [NotMapped]
        public override string? SecurityStamp { get; set; }
        
        [NotMapped]
        public override string? ConcurrencyStamp { get; set; }
        
        [NotMapped]
        public override string? PhoneNumber { get; set; }
        
        [NotMapped]
        public override bool PhoneNumberConfirmed { get; set; }
        
        [NotMapped]
        public override bool TwoFactorEnabled { get; set; }
        
        [NotMapped]
        public override DateTimeOffset? LockoutEnd { get; set; }
        
        [NotMapped]
        public override bool LockoutEnabled { get; set; }
        
        [NotMapped]
        public override int AccessFailedCount { get; set; }
    }

    [Table("Heya")]
    public class Heya
    {
        [Key]
        [Column("Id")]
        public Guid Id { get; set; }

        [Column("Owner")]
        public string? Owner { get; set; }

        [Column("Name")]
        public string? Name { get; set; }

        [Column("Width")]
        public int Width { get; set; }

        [Column("Height")]
        public int Height { get; set; }

        [Column("X")]
        public int X { get; set; }

        [Column("Y")]
        public int Y { get; set; }
    }

    public class ApplicationDbContextFactory : Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddUserSecrets<ApplicationDbContext>().Build();
            var connectionString = configuration["DefaultConnection"];

            switch (ApplicationDbContext.ConnectionAddressString)
            {
                case ApplicationDbContext.ConnectDatabaseStaging:
                case ApplicationDbContext.ConnectDatabaseProcution:
                    connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
                    break;
            }

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

#pragma warning disable CS8604
            optionsBuilder.UseNpgsql(connectionString);
#pragma warning restore CS8604

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }

    public class ApplicationDbContext : DbContext
    {
        public const string ConnectDatabaseLocal = "Local";
        public const string ConnectDatabaseStaging = "Staging";
        public const string ConnectDatabaseProcution = "Production";

        public static string? ConnectionAddressString => Environment.GetEnvironmentVariable("DatabaseConnectionAddress");

        public DbSet<ApplicationUser>? ApplicationUsers { get; set; }

        public DbSet<Heya>? Heyas { get; set; }

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder ob)
        {
            var configuration = new ConfigurationBuilder().AddUserSecrets<ApplicationDbContext>().Build();
            var connectionString = configuration["DefaultConnection"];

            switch (ConnectionAddressString)
            {
                case ConnectDatabaseStaging:
                case ConnectDatabaseProcution:
                    connectionString = Environment.GetEnvironmentVariable("DefaultConnection");
                    break;
            }

#pragma warning disable CS8604
            ob.UseNpgsql(connectionString);
#pragma warning restore CS8604

            ob.UseLazyLoadingProxies();
        }
    }
}