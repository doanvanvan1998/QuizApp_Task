using AutoMapper;
using QuizApp_Task.DTO;
using QuizApp_Task.Entities;
using QuizApp_Task.Model;
using QuizApp_Task.Repository;
using System.Net.Sockets;

namespace QuizApp_Task.Service.impl
{
    public class QuizServiceImpl : IQuizService
    {
        private readonly IQuizRepository _quizRepository;
        private readonly IMapper _mapper;

        public QuizServiceImpl(IQuizRepository repository, IMapper mapper)
        {
            _quizRepository = repository;
            _mapper = mapper;
        }

    public Task<QuizDto> Create(QuizModel model)
        {
            QuizEntity ticketEntity = _mapper.Map<QuizEntity>(model);
            QuizEntity result = _quizRepository.Add(ticketEntity);
            _quizRepository.SaveChanges();
            return Task.FromResult(_mapper.Map<QuizDto>(result));
        }

        public Task<bool> Delete(Guid id)
        {
            QuizEntity? entity = _quizRepository.GetById(id);
            if (entity == null)
                return Task.FromResult(false);
            else
            {
                _quizRepository.Delete(entity);
                _quizRepository.SaveChanges();
                return Task.FromResult(true);
            }
        }

        public Task<IEnumerable<QuizDto>> GetAll()
        {
            return Task.FromResult(_quizRepository.getAll().Select(x => _mapper.Map<QuizDto>(x)));
        }

        public Task<QuizDto> GetById(Guid id)
        {
            QuizEntity? entity = _quizRepository.GetById(id);
            if (entity == null)
                throw new Exception($"Quiz not found with id: { id }");
            else
                return Task.FromResult(_mapper.Map<QuizDto>(entity));
        }

        public Task<QuizDto> Update(Guid id, QuizModel model)
        {
            QuizEntity? entity = _quizRepository.GetById(id);
            if (entity == null)
                throw new Exception($"Quiz not found with id: {id}");
            else
            {
                entity.Title = model.Title;
                entity.Description = model.Description;
                entity.Duration = model.Duration;
                entity.ThumbnailUrl = model.ThumbnailUrl;

                bool result = _quizRepository.Update(entity);
                if (result)
                    _quizRepository.SaveChanges();
                else
                    throw new Exception("Update failed");
                return Task.FromResult(_mapper.Map<QuizDto>(entity));
            }
        }
    }
}
