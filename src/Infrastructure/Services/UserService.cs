using oig_assessment.Application.Shared;
using Microsoft.EntityFrameworkCore;
using oig_assessment.Application.DTOs;
using oig_assessment.Application.DTOs.User;
using oig_assessment.Application.Interfaces;
using oig_assessment.Domain.Entities;
using oig_assessment.Infrastructure.Data;
using oig_assessment.Infrastructure.Identity;
using oig_assessment.Infrastructure.Security;

namespace SurveyApp.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly WriteSurveyDbContext _writeDbContext;
    private readonly JwtProvider _jwtProvider;

    public UserService(JwtProvider jwtProvider,
        WriteSurveyDbContext writeDbContext
        )
    {
        _jwtProvider = jwtProvider;
        _writeDbContext = writeDbContext;
    }

    public async Task<Result<string>> Login(LoginRequest request)
    {
        var user = await _writeDbContext.Users.AsNoTracking()
            .FirstOrDefaultAsync(u => u.LoginName == request.LoginName);

        if (user == null)
        {
            return Result<string>.Failure("User not found"); 
        }

        if (user.Password != PasswordHasher.HashPassword(request.Password))
        {
            return Result<string>.Failure("User login name and (or) password are uncorrect");
        }

        return Result<string>.SuccessResult(_jwtProvider.GenerateToken(user));
    }


    public async Task<Result<string>> Register(RegisterRequest request)
    {
        var existingUser = await _writeDbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.LoginName == request.LoginName);

        if (existingUser != null)
        {
            return Result<string>.Failure("User with this login name already exists.");
        }

        var user = new User
        {
            LoginName = request.LoginName,
            Password = PasswordHasher.HashPassword(request.Password),
            Role = oig_assessment.Domain.ValueObjects.UserRole.SimpleUser
        };

        _writeDbContext.Users.Add(user);
        await _writeDbContext.SaveChangesAsync();

        var token = _jwtProvider.GenerateToken(user);
        return Result<string>.SuccessResult(token);
    }



    public async Task<List<UserDto>> GetAllUsers()
    {
        var users = await _writeDbContext.Users.ToListAsync();

        var usersToReturn = users.Select(u => new UserDto 
        {
            LoginName = u.LoginName,
            Id = u.Id.Value,
            Role = u.Role.Name,
            CreatedOn = u.CreatedOn
        }).ToList();

        return usersToReturn;
    }
}
