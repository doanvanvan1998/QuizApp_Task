using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp_Task.DTO;
using QuizApp_Task.Model;
using QuizApp_Task.Service;

namespace QuizApp_Task.Controllers
{
    [Route("api/question")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionService _questionService;
        private readonly IQuizService _quizService;

        public QuestionController(IQuestionService questionService)
        {
            _questionService = questionService;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            return Ok(_questionService.GetAll().Result);
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] Guid id)
        {
            try
            {
                var question = _questionService.GetById(id);
                return Ok(question);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] QuestionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var question = _questionService.Create(model);
                    /*return CreatedAtAction(nameof(getById), new { Id = quiz.Id }, quiz);*/
                    return Ok(question);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] QuestionModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    QuestionDto question = _questionService.Update(id, model).Result;
                    return Ok(question);
                }
                catch (Exception ex)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                }
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] Guid id)
        {
            bool isDelete = _questionService.Delete(id).Result;
            return Ok(isDelete ? "Delete Success" : "Delete Fail");
        }

        [HttpGet("{id}/quiz")]
        public IActionResult getAllByQuizId([FromRoute] Guid id)
        {
            try
            {
                var question = _questionService.getAllByQuizId(id);
                return Ok(question);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
