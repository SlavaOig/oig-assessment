using oig_assessment.Domain.ValueObjects;

namespace BlazorWeb.Shared.Models.Survey;

public class PublicSurveyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SurveyStatus Status { get; set; } = null!;

    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; } = null!;

    public bool IsEditable { get; set; }
    public bool IsAnswerable { get; set; }
}
