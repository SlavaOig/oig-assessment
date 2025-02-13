namespace oig_assessment.Application.Models.Read;
public sealed class SurveyReadModel
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public int Status { get; set; }
    public Guid CreatedBy { get; set; }
    public Guid? UpdatedBy { get; set; }
    public List<QuestionReadModel> Questions { get; set; } = null!;
}
