using AutoMapper;
using QuizApp_Task.DTO;
using QuizApp_Task.Entities;
using QuizApp_Task.Model;
using QuizApp_Task.Repository;

namespace QuizApp_Task.Service.impl
{
    public class AnswerServiceImpl : IAnswerService
    {
        private readonly IAnswerRepository _answerRepository;
        private readonly IMapper _mapper;
        private readonly IQuestionRepository _questionRepository;

        public AnswerServiceImpl(IAnswerRepository answerRepository, IMapper mapper, IQuestionRepository questionRepository)
        {
            _answerRepository = answerRepository;
            _mapper = mapper;
            _questionRepository = questionRepository;
        }

        public Task<AnswerDto> Create(AnswerModel model)
        {
            QuestionEntity? question = _questionRepository.GetById(model.QuestionId);
            if (question == null)
            {
                throw new Exception($"Question is not found with id: {model.QuestionId}");
            }

            AnswerEntity entity = _mapper.Map<AnswerEntity>(model);
            AnswerEntity result = _answerRepository.Add(entity);
            _answerRepository.SaveChanges();
            return Task.FromResult(_mapper.Map<AnswerDto>(result));
        }

        public Task<bool> Delete(Guid id)
        {
            AnswerEntity? entity = _answerRepository.GetById(id);
            if (entity == null)
                return Task.FromResult(false);
            else
            {
                _answerRepository.Delete(entity);
                _answerRepository.SaveChanges();
                return Task.FromResult(true);
            }
        }

        public Task<IEnumerable<AnswerDto>> GetAll()
        {
            return Task.FromResult(_answerRepository.getAll().Select(x => _mapper.Map<AnswerDto>(x)));
        }

        public IEnumerable<AnswerDto> getAllByQuestionId(Guid id)
        {
            return _answerRepository.getAllByQuestionId(id).Select(x => _mapper.Map<AnswerDto>(x));
        }

        public Task<AnswerDto> GetById(Guid id)
        {
            AnswerEntity? entity = _answerRepository.GetById(id);
            if (entity == null)
                throw new Exception($"Answer not found with id: {id}");
            else
                return Task.FromResult(_mapper.Map<AnswerDto>(entity));
        }

        public Task<AnswerDto> Update(Guid id, AnswerModel model)
        {
            AnswerEntity? entity = _answerRepository.GetById(id);
            if (entity == null)
                throw new Exception($"Answer not found with id: {id}");
            else
            {
                entity.Content = model.Content;
                entity.IsCorrect = model.IsCorrect;

                bool result = _answerRepository.Update(entity);
                if (result)
                    _answerRepository.SaveChanges();
                else
                    throw new Exception("Update failed");
                return Task.FromResult(_mapper.Map<AnswerDto>(entity));
            }
        }
    }
}
