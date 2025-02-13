using AutoMapper;
using oig_assessment.Domain.Entities;
using oig_assessment.Application.Surveys.Commands;

namespace oig_assessment.Application.Common;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreateSurveyCommand, Survey>();
        CreateMap<UpdateSurveyCommand, Survey>();
        CreateMap<DeleteSurveyCommand, Survey>();
        CreateMap<UpdateSurveyStatusCommand, Survey>();

        CreateMap<CreateSurveyAnswersCommand, Answer>();
    }
}
