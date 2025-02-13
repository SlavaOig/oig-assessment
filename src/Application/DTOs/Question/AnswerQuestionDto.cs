using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oig_assessment.Application.DTOs.Question;
public class AnswerQuestionDto
{
    public Guid QuestionId { get; set; }
    public Guid SurveyId { get; set; }
    public string QuestionAnswer { get; set; } = string.Empty;
}
