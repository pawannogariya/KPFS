using KPFS.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace KPFS.Data
{
    public class KpfsDbContext : IdentityDbContext<User, Role, string, IdentityUserClaim<string>, UserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public KpfsDbContext(DbContextOptions<KpfsDbContext> options) : base(options)
        {
        }

        public DbSet<FundHouse> FundHouses { get; set; }
        public DbSet<Fund> Funds { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<Investor> Investors { get; set; }
        public DbSet<Closure> Closures { get; set; }
        public DbSet<FundManager> FundManagers { get; set; }
        public DbSet<Drawdown> Drawdowns { get; set; }
        public DbSet<TemporaryInvestment> TemporaryInvestments { get; set; }
        public DbSet<PortfolioCompany> PortfolioCompanies { get; set; }


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

            //SeedData(builder);
        }

        private void SeedData(ModelBuilder builder)
        {
            builder.Entity<Role>().HasData
            (
                new Role() { Id = "ada877bb-8dda-11ee-b507-e86a64b47aae", Name = Constants.Roles.User, ConcurrencyStamp = "1", NormalizedName = Constants.Roles.User },
                new Role() { Id = "b9ba635b-8dda-11ee-b507-e86a64b47aae", Name = Constants.Roles.Reviewer, ConcurrencyStamp = "2", NormalizedName = Constants.Roles.Reviewer },
                new Role() { Id = "c1170fd6-8dda-11ee-b507-e86a64b47aae", Name = Constants.Roles.Admin, ConcurrencyStamp = "3", NormalizedName = Constants.Roles.Admin }
            );

            var adminEmail = "pawan.nogariya@gmail.com";
            var adminPassword = "P@ssword123";
            var passwordHasher = new PasswordHasher<User>();
            builder.Entity<User>().HasData
            (
                new User()
                {
                    Id = "668f96df-8e03-11ee-b507-e86a64b47aae",
                    FirstName = "Super",
                    LastName = "Admin",
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true,
                    NormalizedUserName = adminEmail.ToUpper(),
                    NormalizedEmail = adminEmail.ToUpper(),
                    PasswordHash = passwordHasher.HashPassword(null, adminPassword),
                    TwoFactorEnabled = true,
                    LockoutEnabled = true,
                    IsActive = true
                }
            );

            builder.Entity<UserRole>().HasData
             (
                 new UserRole() { UserId = "668f96df-8e03-11ee-b507-e86a64b47aae", RoleId = "c1170fd6-8dda-11ee-b507-e86a64b47aae" }
             );


            builder.Entity<FundHouse>().HasData
            (
                new FundHouse() { Id = 1, ShortName = "KPFS", FullName = "KPFS Fund House", CreatedBy = "668f96df-8e03-11ee-b507-e86a64b47aae", CreatedOn = DateTime.UtcNow, IsDeleted = false }
            );

            //builder.Entity<Fund>().HasData
            //(
            //    new Fund() { Id = 1, Name = "KPFS Fund", FundHouseId = 1, CreatedBy = "668f96df-8e03-11ee-b507-e86a64b47aae", CreatedOn = DateTime.UtcNow, IsDeleted = false }
            //);

            //  builder.Entity<BankAccount>().HasData
            // (
            //     new BankAccount() { Id = 1, BankName = "SBI", AccountNumber = "12345678", FundId = 1, CreatedBy = "668f96df-8e03-11ee-b507-e86a64b47aae", CreatedOn = DateTime.UtcNow, IsDeleted = false },
            //     new BankAccount() { Id = 2, BankName = "ICICI", AccountNumber = "48395847", FundId = 1, CreatedBy = "668f96df-8e03-11ee-b507-e86a64b47aae", CreatedOn = DateTime.UtcNow, IsDeleted = false },
            //     new BankAccount() { Id = 3, BankName = "HDFC", AccountNumber = "49573625", FundId = 1, CreatedBy = "668f96df-8e03-11ee-b507-e86a64b47aae", CreatedOn = DateTime.UtcNow, IsDeleted = false }
            // );

            //  builder.Entity<FundManager>().HasData
            //(
            //    new FundManager() { Id = 1, FundId = 1, ManagerFirstName = "John", ManagerLastName = "Edward", Email = "pawan.nogariya@gmail.com", CreatedBy = "668f96df-8e03-11ee-b507-e86a64b47aae", CreatedOn = DateTime.UtcNow, IsDeleted = false },
            //    new FundManager() { Id = 2, FundId = 1, ManagerFirstName = "Rohit", ManagerLastName = "Sharma", Email = "pawan.nogariya@gmail.com", CreatedBy = "668f96df-8e03-11ee-b507-e86a64b47aae", CreatedOn = DateTime.UtcNow, IsDeleted = false }
            //);

            //  builder.Entity<Closure>().HasData
            //(
            // new Closure() { Id = 1, FundId = 1, Name = "Default Closure", CreatedBy = "668f96df-8e03-11ee-b507-e86a64b47aae", CreatedOn = DateTime.UtcNow, IsDeleted = false }
            //);
        }
    }
}
