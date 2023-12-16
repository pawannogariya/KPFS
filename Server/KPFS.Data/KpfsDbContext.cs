using KPFS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace KPFS.Data
{
    public class KpfsDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public KpfsDbContext(DbContextOptions<KpfsDbContext> options) : base(options)
        {
        }

        public DbSet<FundHouse> FundHouses { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<InvestorDetail> InvestorDetails { get; set; }
        public DbSet<Closure> Closures { get; set; }
        public DbSet<FundManager> FundManagers { get; set; }
        public DbSet<Drawdown> Drawdowns { get; set; }
        public DbSet<TemporaryInvestment> TemporaryInvestments { get; set; }
        public DbSet<PortfolioCompany> PortfolioCompanies { get; set; }
        public DbSet<FundInvestor> FundInvestors { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<User>()
                .HasMany(x => x.UserRoles);

            builder.Entity<UserRole>()
                .HasOne(x => x.User)
                  .WithMany(x => x.UserRoles)
                  .HasForeignKey(x => x.UserId);

            builder.Entity<UserRole>()
                .HasOne(x => x.Role)
                  .WithMany(x => x.UserRoles)
                  .HasForeignKey(x => x.RoleId);

            builder.Entity<Fund>()
                    .HasIndex(u => u.ShortName)
                    .IsUnique();

            builder.Entity<FundHouse>()
                   .HasIndex(u => u.ShortName)
                   .IsUnique();
        }
    }
}
