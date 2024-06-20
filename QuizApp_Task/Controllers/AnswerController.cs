using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizApp_Task.DTO;
using QuizApp_Task.Model;
using QuizApp_Task.Service;

namespace QuizApp_Task.Controllers
{
    [Route("api/answer")]
    [ApiController]
    public class AnswerController : ControllerBase
    {
        private readonly IAnswerService _answerService;

        public AnswerController(IAnswerService answerService)
        {
            _answerService = answerService;
        }

        [HttpGet]
        public IActionResult getAll()
        {
            return Ok(_answerService.GetAll().Result);
        }

        [HttpGet("{id}")]
        public IActionResult getById([FromRoute] Guid id)
        {
            try
            {
                var answer = _answerService.GetById(id);
                return Ok(answer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] AnswerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    var answer = _answerService.Create(model);
                    /*return CreatedAtAction(nameof(getById), new { Id = quiz.Id }, quiz);*/
                    return Ok(answer);
                }
                catch (Exception e)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
                }
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] AnswerModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                try
                {
                    AnswerDto answer = _answerService.Update(id, model).Result;
                    return Ok(answer);
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
            bool isDelete = _answerService.Delete(id).Result;
            return Ok(isDelete ? "Delete Success" : "Delete Fail");
        }

        [HttpGet("{id}/question")]
        public IActionResult getAllByQuizId([FromRoute] Guid id)
        {
            try
            {
                var answer = _answerService.getAllByQuestionId(id);
                return Ok(answer);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
