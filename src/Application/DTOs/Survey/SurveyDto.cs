using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Application.DTOs.Survey;
public class SurveyDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public SurveyStatus Status { get; set; } = SurveyStatus.Concept;

    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
}
