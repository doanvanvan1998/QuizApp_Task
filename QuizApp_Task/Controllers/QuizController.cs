using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp_Task.DTO;
using QuizApp_Task.Model;
using QuizApp_Task.Service;

namespace QuizApp_Task.Controllers
{
    [Route("api/quiz")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;

        public QuizController(IQuizService quizService)
        {
            _quizService = quizService;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            return Ok(_quizService.GetAll().Result);
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] Guid id)
        {
            try
            {
                var quiz = _quizService.GetById(id);
                return Ok(quiz);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] QuizModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var quiz = _quizService.Create(model);
                    /*return CreatedAtAction(nameof(getById), new { Id = quiz.Id }, quiz);*/
                    return Ok(quiz);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] QuizModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    QuizDto quiz = _quizService.Update(id, model).Result;
                    return Ok(quiz);
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
            bool isDelete = _quizService.Delete(id).Result;
            return Ok(isDelete ? "Delete Success" : "Delete Fail");
        }

    }
}
