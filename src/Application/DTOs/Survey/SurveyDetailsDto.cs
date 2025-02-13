using oig_assessment.Application.DTOs.Question;
using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Application.DTOs.Survey;
public class SurveyDetailsDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public TimeOnly StartDateTime { get; set; }
    public TimeOnly EndDateTime { get; set; }
    public SurveyStatus Status { get; set; } = SurveyStatus.Concept;

    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public List<QuestionDto> Questions { get; set; } = null!;
}
