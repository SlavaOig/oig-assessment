using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using oig_assessment.Domain.ValueObjects;

public class UserRoleConverter : ValueConverter<UserRole, int>
{
    public UserRoleConverter()
        : base(
            status => status.Value,  
            value => UserRole.FromValue(value)
        )
    { }
}
