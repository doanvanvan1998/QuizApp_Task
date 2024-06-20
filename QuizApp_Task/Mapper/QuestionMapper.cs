using AutoMapper;
using QuizApp_Task.DTO;
using QuizApp_Task.Entities;
using QuizApp_Task.Model;

namespace QuizApp_Task.Mapper
{
    public class QuestionMapper : Profile
    {
        public QuestionMapper() 
        {
            CreateMap<QuestionModel, QuestionEntity>();
            CreateMap<QuestionEntity, QuestionDto>().ForMember(dest => dest.AnswerDtos, opt => opt.MapFrom(src => src.AnswerEntities));
        }
    }
}
