using oig_assessment.Domain.Entities;
using oig_assessment.Domain.ValueObjects;
using oig_assessment.Infrastructure.Identity;

namespace oig_assessment.Infrastructure.Data;

public class DataSeeder
{
    private readonly WriteSurveyDbContext _writeDbContext;

    public DataSeeder (WriteSurveyDbContext writeDbContext)
    {
        _writeDbContext = writeDbContext;
    }

    public async Task SeedUsers()
    {
        if (!_writeDbContext.Users.Any())
        {
            var adminUser =  _writeDbContext.Users.FirstOrDefault(u => u.LoginName == "admin");

            var simpleUser = _writeDbContext.Users.FirstOrDefault(u => u.LoginName == "simpleUser");

            if (adminUser == null)
            {
                adminUser = new User
                {
                    LoginName = "admin",
                    Password = PasswordHasher.HashPassword("p@ssword"),
                    Role = UserRole.Admin
                };

                _writeDbContext.Users.Add(adminUser);
            }

            if (simpleUser == null)
            {
                simpleUser = new User
                {
                    LoginName = "simpleUser",
                    Password = PasswordHasher.HashPassword("userPassword"),
                    Role = UserRole.SimpleUser
                };

                _writeDbContext.Users.Add(simpleUser);
            }

            await _writeDbContext.SaveChangesAsync();
        }
    }
}
