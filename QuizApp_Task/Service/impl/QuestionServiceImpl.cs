using AutoMapper;
using QuizApp_Task.DTO;
using QuizApp_Task.Entities;
using QuizApp_Task.Model;
using QuizApp_Task.Repository;

namespace QuizApp_Task.Service.impl
{
    public class QuestionServiceImpl : IQuestionService
    {
        private readonly IQuestionRepository _questionRepository;
        private readonly IMapper _mapper;
        private readonly IQuizRepository _quizRepository;
        private readonly IAnswerRepository _answerRepository;

        public QuestionServiceImpl(IQuestionRepository questionRepository, IMapper mapper, IQuizRepository quizRepository, IAnswerRepository answerRepository)
        {
            _questionRepository = questionRepository;
            _mapper = mapper;
            _quizRepository = quizRepository;
            _answerRepository = answerRepository;
        }

        public Task<QuestionDto> Create(QuestionModel model)
        {

            QuizEntity? quiz = _quizRepository.GetById(model.QuizId);
            if (quiz == null)
            {
                throw new Exception($"Quiz is not found with id: {model.QuizId}");
            }

            QuestionEntity entity = _mapper.Map<QuestionEntity>(model);
            QuestionEntity result = _questionRepository.Add(entity);
            _questionRepository.SaveChanges();
            return Task.FromResult(_mapper.Map<QuestionDto>(result));
        }

        public Task<bool> Delete(Guid id)
        {
            QuestionEntity? entity = _questionRepository.GetById(id);
            if (entity == null)
                return Task.FromResult(false);
            else
            {
                _questionRepository.Delete(entity);
                _questionRepository.SaveChanges();
                return Task.FromResult(true);
            }
        }

        public Task<IEnumerable<QuestionDto>> GetAll()
        {
            return Task.FromResult(_questionRepository.getAll().Select(x => _mapper.Map<QuestionDto>(x)));
        }

        public IEnumerable<QuestionDto> getAllByQuizId(Guid id)
        {
            return _questionRepository.getAllByQuizId(id).Select(x => _mapper.Map<QuestionDto>(x));
        }

        public Task<QuestionDto> GetById(Guid id)
        {
            QuestionEntity? entity = _questionRepository.GetById(id);
            if (entity == null)
                throw new Exception($"Question not found with id: {id}");

            QuestionDto questionDto = _mapper.Map<QuestionDto>(entity);

            return Task.FromResult(questionDto);
        }

        public Task<QuestionDto> Update(Guid id, QuestionModel model)
        {
            QuestionEntity? entity = _questionRepository.GetById(id);
            if (entity == null)
                throw new Exception($"Question not found with id: {id}");
            else
            {
                entity.Content = model.Content;
                entity.QuestionType = model.QuestionType;

                bool result = _questionRepository.Update(entity);
                if (result)
                    _questionRepository.SaveChanges();
                else
                    throw new Exception("Update failed");
                return Task.FromResult(_mapper.Map<QuestionDto>(entity));
            }
        }
    }
}
