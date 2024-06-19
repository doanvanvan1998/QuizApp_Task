using AutoMapper;
using QuizApp_Task.DTO;
using QuizApp_Task.Entities;
using QuizApp_Task.Model;

namespace QuizApp_Task.Mapper
{
    public class QuizMapper :Profile
    {
        public QuizMapper() {

            CreateMap<QuizModel, QuizEntity>();
            CreateMap<QuizEntity, QuizDto>();
        }
    }
}
