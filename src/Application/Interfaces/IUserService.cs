using oig_assessment.Application.Shared;
using oig_assessment.Application.DTOs;
using oig_assessment.Application.DTOs.User;

namespace oig_assessment.Application.Interfaces;
public interface IUserService
{
    Task<Result<string>> Login(LoginRequest request);
    Task<Result<string>> Register(RegisterRequest request);
    Task<List<UserDto>> GetAllUsers();
}
