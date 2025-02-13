namespace oig_assessment.Domain.ValueObjects;

public class UserRole
{
    private UserRole(int value, string name)
    {
        Value = value;
        Name = name;
    }

    private enum UserRoleEnum
    {
        None = 0,
        Admin = 1,
        SimpleUser = 2
    }


    public static UserRole None => new UserRole((int)UserRoleEnum.None, "None");
    public static UserRole Admin => new UserRole((int)UserRoleEnum.Admin, "Admin");
    public static UserRole SimpleUser => new UserRole((int)UserRoleEnum.SimpleUser, "SimpleUser");

    public int Value { get; }

    public string Name { get; } = null!;

    private UserRole () { }
    public static UserRole FromValue(int value)
    {
        return value switch
        {
            (int)UserRoleEnum.None => None,
            (int)UserRoleEnum.Admin => Admin,
            (int)UserRoleEnum.SimpleUser => SimpleUser,

            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "UserRole not supported")
        };
    }
}
