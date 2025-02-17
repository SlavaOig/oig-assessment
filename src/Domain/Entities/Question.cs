using oig_assessment.Domain.Entities.Common;
using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Domain.Entities;

public class Question : AuditableEntity
{
    public QuestionId Id { get; private set; } = new QuestionId(Guid.NewGuid());
    public SurveyId SurveyId { get; private set; } = new SurveyId(Guid.NewGuid());
    public string QuestionText { get; private set; } = string.Empty;
    
    private Question() { }

    private Question(
        string questionText,
        Guid createdBy,
        Guid parentSurvetId
        )
    {
        Id = new QuestionId(Guid.NewGuid());
        QuestionText = questionText;
        CreatedBy = new UserId(createdBy);
        SurveyId = new SurveyId(parentSurvetId);
    }

    public static Question Create(
        string questionText,
        Guid createdBy,
        Guid parentSurvetId
        )
    {
        var question = new Question(questionText, createdBy, parentSurvetId);
        
        return question;
    }

    public void Update(
        string questionText,
        Guid updatedBy
        )
    {
        QuestionText = questionText;
        UpdatedBy = new UserId(updatedBy);
    }
}
