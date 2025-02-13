using oig_assessment.Domain.Entities.Rules;
using oig_assessment.Domain.ValueObjects;

namespace oig_assessment.Domain.Entities;

public class Answer : Entity
{
    public AnswerId Id { get; private set; } = null!;
    public string QuestionAnswer { get; private set; } = string.Empty;

    public UserId AnsweredBy { get; private set; } = null!;

    public SurveyId SurveyId { get; private set; } = null!;
    public QuestionId QuestionId { get; private set; } = null!;

    private Answer() { }


    private Answer(
        string questionAnswer,
        Guid answeredBy,
        Guid surveyId,
        Guid questionId
        )
    {
        Id = new AnswerId(Guid.NewGuid());
        QuestionAnswer = questionAnswer;
        SurveyId = new SurveyId(surveyId);
        QuestionId = new QuestionId(questionId);
        AnsweredBy = new UserId(answeredBy);
    }

    public static Answer Create(
        string questionAnswer,
        Guid answeredBy,
        Guid surveyId,
        Guid questionId)
    {
        var answer = new Answer(
            questionAnswer,
            answeredBy,
            surveyId,
            questionId
            );

        return answer;
    }

    public void CheckAnswerRule(string answerQuestion)
    {
        CheckRule(new AnswerStringRule(answerQuestion));
    }
}

