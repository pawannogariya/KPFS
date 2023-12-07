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

                using(var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
                {
                    try
                    {
                        if (!dbContext.Roles.Any())
                        {
                            await dbContext.Roles.AddAsync(new Role()
                            {
                                Id = "ada877bb-8dda-11ee-b507-e86a64b47aae",
                                Name = Data.Constants.Roles.User,
                                ConcurrencyStamp = "1",
                                NormalizedName = Data.Constants.Roles.User
                            });

                            await dbContext.Roles.AddAsync(new Data.Entities.Role()
                            {
                                Id = "b9ba635b-8dda-11ee-b507-e86a64b47aae",
                                Name = Data.Constants.Roles.Reviewer,
                                ConcurrencyStamp = "2",
                                NormalizedName = Data.Constants.Roles.Reviewer
                            });

                            await dbContext.Roles.AddAsync(new Data.Entities.Role()
                            {
                                Id = "c1170fd6-8dda-11ee-b507-e86a64b47aae",
                                Name = Data.Constants.Roles.Admin,
                                ConcurrencyStamp = "3",
                                NormalizedName = Data.Constants.Roles.Admin
                            });
                        }

                        if(!dbContext.Users.Any())
                        {
                            var adminEmail = appSettings.AdminCredentials.Email;
                            var adminPassword = appSettings.AdminCredentials.Password;
                            var passwordHasher = new PasswordHasher<User>();

                            await dbContext.Users.AddAsync(new User()
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
                            });

                            await dbContext.UserRoles.AddAsync(new UserRole() 
                            { 
                                UserId = "668f96df-8e03-11ee-b507-e86a64b47aae", 
                                RoleId = "c1170fd6-8dda-11ee-b507-e86a64b47aae" 
                            });
                        }

                        if(!dbContext.FundHouses.Any())
                        {
                            await dbContext.FundHouses.AddAsync(new FundHouse()
                            {
                                Id = 1,
                                ShortName = "KPFS",
                                FullName = "KPFS Fund House",
                                CreatedBy = "668f96df-8e03-11ee-b507-e86a64b47aae",
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
