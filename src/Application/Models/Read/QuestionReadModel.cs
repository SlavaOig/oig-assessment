namespace oig_assessment.Application.Models.Read;
public sealed class QuestionReadModel
{
    public Guid Id { get; set; }
    public Guid SurveyId { get; set; }

    public string QuestionText { get; set; } = null!;
    public Guid CreatedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    
    public Guid? UpdatedBy { get; set; }
}
