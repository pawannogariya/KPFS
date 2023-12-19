using KPFS.Data;
using KPFS.Data.Entities;
using KPFS.Web.AppSettings;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Transactions;

namespace KPFS.Web
{
    public class DataSeeder
    {
        public async static Task Initialize(IServiceScope serviceScope)
        {
            using (var dbContext = serviceScope.ServiceProvider.GetRequiredService<KpfsDbContext>())
            {
                var appSettings = serviceScope.ServiceProvider.GetRequiredService<IOptions<ApplicationSettings>>().Value;
                var logger = serviceScope.ServiceProvider.GetRequiredService<ILogger<DataSeeder>>();
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<User>>();

                using (var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        if (!dbContext.Roles.Any())
                        {
                            await dbContext.Roles.AddAsync(new Role()
                            {
                                Id = 1,
                                Name = Data.Constants.Roles.User,
                                DisplayName = Data.Constants.Roles.User,
                                ConcurrencyStamp = "1",
                                NormalizedName = Data.Constants.Roles.User.ToUpper(),
                                HierarchyOrder = 0
                            });

                            await dbContext.Roles.AddAsync(new Role()
                            {
                                Id = 2,
                                Name = Data.Constants.Roles.Reviewer,
                                DisplayName = Data.Constants.Roles.Reviewer,
                                ConcurrencyStamp = "2",
                                NormalizedName = Data.Constants.Roles.Reviewer.ToUpper(),
                                HierarchyOrder = 1,
                            });

                            await dbContext.Roles.AddAsync(new Role()
                            {
                                Id = 3,
                                Name = Data.Constants.Roles.Admin,
                                DisplayName = Data.Constants.Roles.Admin,
                                ConcurrencyStamp = "3",
                                NormalizedName = Data.Constants.Roles.Admin.ToUpper(),
                                HierarchyOrder = 2
                            });
                        }

                        foreach (var admin in appSettings.AdminCredentials)
                        {
                            var adminId = 0;
                            if (!dbContext.Users.Any(u => u.UserName == admin.Email))
                            {
                                adminId = adminId + 1;
                                var passwordHasher = new PasswordHasher<User>();


                                await dbContext.Users.AddAsync(new User()
                                {
                                    Id = adminId,
                                    FirstName = string.IsNullOrWhiteSpace(admin.FirstName) ? "Super" : admin.FirstName,
                                    LastName = string.IsNullOrWhiteSpace(admin.LastName) ? "Admin" : admin.LastName,
                                    UserName = admin.Email,
                                    Email = admin.Email,
                                    EmailConfirmed = true,
                                    NormalizedUserName = admin.Email.ToUpper(),
                                    NormalizedEmail = admin.Email.ToUpper(),
                                    PasswordHash = passwordHasher.HashPassword(null, admin.Password),
                                    TwoFactorEnabled = true,
                                    LockoutEnabled = true,
                                    IsActive = true,
                                    SecurityStamp = Guid.NewGuid().ToString("D")
                                });

                                await dbContext.UserRoles.AddAsync(new UserRole()
                                {
                                    UserId = adminId,
                                    RoleId = 3
                                });
                            }
                        }

                        //if (!dbContext.Users.Any())
                        //{
                        //    var adminEmail = appSettings.Admins.Email;
                        //    var adminPassword = appSettings.Admins.Password;
                        //    var passwordHasher = new PasswordHasher<User>();

                        //    await dbContext.Users.AddAsync(new User()
                        //    {
                        //        Id = "668f96df-8e03-11ee-b507-e86a64b47aae",
                        //        FirstName = "Super",
                        //        LastName = "Admin",
                        //        UserName = adminEmail,
                        //        Email = adminEmail,
                        //        EmailConfirmed = true,
                        //        NormalizedUserName = adminEmail.ToUpper(),
                        //        NormalizedEmail = adminEmail.ToUpper(),
                        //        PasswordHash = passwordHasher.HashPassword(null, adminPassword),
                        //        TwoFactorEnabled = true,
                        //        LockoutEnabled = true,
                        //        IsActive = true
                        //    });

                        //    await dbContext.UserRoles.AddAsync(new UserRole()
                        //    {
                        //        UserId = "668f96df-8e03-11ee-b507-e86a64b47aae",
                        //        RoleId = "c1170fd6-8dda-11ee-b507-e86a64b47aae"
                        //    });
                        //}

                        if (!dbContext.FundHouses.Any())
                        {
                            await dbContext.FundHouses.AddAsync(new FundHouse()
                            {
                                Id = 1,
                                ShortName = "KPFS",
                                FullName = "KPFS Fund House",
                                CreatedBy = 1,
                                CreatedOn = DateTime.UtcNow,
                                IsDeleted = false
                            });
                        }

                        //if (!dbContext.Funds.Any())
                        //{
                        //    await dbContext.Funds.AddAsync(new Fund()
                        //    {
                        //        Id = 1,
                        //        ShortName = "KPFS Fund",
                        //        FullName = "KPFS Fund",
                        //        SebiRegistrationNumber="1234",

                        //        CreatedBy = "668f96df-8e03-11ee-b507-e86a64b47aae",
                        //        CreatedOn = DateTime.UtcNow,
                        //        IsDeleted = false
                        //    });
                        //}

                        await dbContext.SaveChangesAsync();

                        transactionScope.Complete();
                    }
                    catch (Exception ex)
                    {
                        transactionScope.Dispose();
                        logger.LogError(ex.ToString());

                        throw;
                    }
                }
            }
        }
    }
}
