using oig_assessment.Domain.Entities.Common;
using oig_assessment.Domain.Entities.Rules;
using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Domain.Entities;

public class Survey : AuditableEntity
{
    public SurveyId Id { get; private set; } = new SurveyId(Guid.NewGuid());
    public string Name { get; private set; } = string.Empty;
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public SurveyStatus Status { get; private set; } = SurveyStatus.Concept;

    private readonly List<Question> _questions = null!;

    public IReadOnlyList<Question> Questions => _questions.AsReadOnly();


    private Survey() { }

    private Survey(
        string name,
        DateTime startDate,
        DateTime endDate,
        Guid createdBy
        )
    {
        Id = new SurveyId(Guid.NewGuid());
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        CreatedBy = new UserId(createdBy);
        Status = SurveyStatus.Concept;
        _questions = new List<Question>();
    }

    public static Survey Create(
        string name,
        DateTime startDate,
        DateTime endDate,
        Guid createdBy)
    {
        var survey = new Survey(
            name,
            startDate,
            endDate,
            createdBy
            );

        return survey;
    }

    public void Update(string name, DateTime startDate, DateTime endDate, Guid updatedBy)
    {
        Name = name;
        StartDate = startDate;
        EndDate = endDate;
        UpdatedBy = new UserId(updatedBy);
    }

    public void CreateRangeQuestions(string questionText, Guid parentSurveyId, Guid createdBy)
    {
        var question = Question.Create(
            questionText,
            createdBy,
            parentSurveyId);

        _questions.Add(question);
    }


    public void UpdateQuestions(List<(Guid? Id, string QuestionText)> questionsData, Guid updatedBy)
    {
        var existingQuestionsDictionary = _questions.ToDictionary(q => q.Id, q => q);
        var existingIds = new HashSet<QuestionId>();

        var questionsToCreate = new List<Question>();

        foreach (var (id, questionText) in questionsData)
        {
            if (id.HasValue && existingQuestionsDictionary.TryGetValue(new QuestionId(id.Value), out var existingQuestion))
            {
                existingQuestion.Update(questionText, updatedBy);
                existingIds.Add(existingQuestion.Id);
            }
            else
            {
                var newQuestion = Question.Create(
                    questionText,
                    updatedBy,
                    this.Id
                );

                questionsToCreate.Add(newQuestion);
            }
        }

        _questions.RemoveAll(q => !existingIds.Contains(q.Id));

        _questions.AddRange(questionsToCreate);
    }


    public void CheckDateRule(DateTime dateToCheck) 
    {
        CheckRule(new SurveyDateRule(dateToCheck));
    }

    public void CheckEndDateRule(TimeOnly starTime, TimeOnly endTime)
    {
        CheckRule(new SurveyTimeRule(starTime, endTime));
    }

    public void ChangeStatus(SurveyStatus newStatus)
    {
        Status = newStatus;
    }

    public bool CanChangeStatus(SurveyStatus newStatus)
    {
        return SurveyStatus.GetAllowedStatuses(newStatus.Value).Contains(newStatus);
    }
}
