using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Application.DTOs.Question;
public class QuestionDto
{
    public Guid? Id { get; set; }
    public string QuestionText { get; set; } = string.Empty;
    
    public SurveyId SurveyId { get; set; } = null!;

    public UserId CreatedBy { get; set; } = null!;

    public DateTime CreatedOn { get; set; }

    public UserId? UpdatedBy { get; set; }
}
