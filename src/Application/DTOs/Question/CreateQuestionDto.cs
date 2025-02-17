using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Application.DTOs.Question;
public class CreateQuestionDto
{
    public string QuestionText { get; set; } = string.Empty;   
}
