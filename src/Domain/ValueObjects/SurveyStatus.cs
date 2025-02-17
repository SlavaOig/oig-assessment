namespace oig_assessment.Domain.ValueObjects;

public class SurveyStatus
{
    private SurveyStatus(int value, string name)
    {
        Value = value;
        Name = name;
    }

    private enum SurveyStatusEnum
    {
        None = 0,
        Concept = 1,
        Available = 2,
        Active = 3,
        Closed = 5
    }

    public static SurveyStatus None => new SurveyStatus((int)SurveyStatusEnum.None, "None");
    public static SurveyStatus Concept => new SurveyStatus((int)SurveyStatusEnum.Concept, "Concept");
    public static SurveyStatus Available => new SurveyStatus((int)SurveyStatusEnum.Available, "Available");
    public static SurveyStatus Active => new SurveyStatus((int)SurveyStatusEnum.Active, "Active");
    public static SurveyStatus Closed => new SurveyStatus((int)SurveyStatusEnum.Closed, "Closed");
    
    public int Value { get; }

    public string Name { get; } = null!;

    public SurveyStatus() { }


    public override bool Equals(object? obj)
    {
        if (obj is not SurveyStatus other) return false;
        return Value == other.Value;
    }

    public override int GetHashCode() => Value.GetHashCode();

    public static SurveyStatus FromValue(int value)
    {
        return value switch
        {
            (int)SurveyStatusEnum.None => None,
            (int)SurveyStatusEnum.Concept => Concept,
            (int)SurveyStatusEnum.Available => Available,
            (int)SurveyStatusEnum.Active => Active,
            (int)SurveyStatusEnum.Closed => Closed,

            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "SurveyStatus not supported")
        };
    }

    public static List<SurveyStatus> GetAllowedStatuses(int value)
    {
        return value switch
        {
            (int)SurveyStatusEnum.Concept => new List<SurveyStatus> { Available },
            (int)SurveyStatusEnum.Available => new List<SurveyStatus> { Active, Closed },
            (int)SurveyStatusEnum.Active => new List<SurveyStatus> { Closed },
            _ => new List<SurveyStatus>()
        };
    }

}
