using AutoMapper;
using QuizApp_Task.DTO;
using QuizApp_Task.Entities;
using QuizApp_Task.Model;

namespace QuizApp_Task.Mapper
{
    public class AnswerMapper : Profile
    {
        public AnswerMapper() 
        {
            CreateMap<AnswerModel, AnswerEntity>();
            CreateMap<AnswerEntity, AnswerDto>();
        }
    }
}
