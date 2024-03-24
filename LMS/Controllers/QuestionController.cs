using AutoMapper;
using LMS.BL.DTO;
using LMS.BL.Interface;
using LMS.BL.Repository;
using LMS.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuestionRep questionRep;
        private readonly IMapper mapper;
        private readonly IExamRep examRep;

        public QuestionController(IQuestionRep questionRep, IMapper mapper, IExamRep examRep)
        {
            this.questionRep = questionRep;
            this.mapper = mapper;
            this.examRep = examRep;
        }

        [HttpGet]
        public ActionResult<IEnumerable<QuestionWithExamNameDTO>> GetAll()
        {
            var questions = questionRep.GetAllData();
            var questionDTO = mapper.Map<IEnumerable<QuestionWithExamNameDTO>>(questions);
            return Ok(questionDTO);
        }

        [HttpGet("{id:int}")]
        public ActionResult<QuestionWithExamNameDTO> GetId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { error = "Invalid ID", message = "ID must be a positive integer." });

                var question = questionRep.GetById(id);

                var questionDtos = mapper.Map<QuestionWithExamNameDTO>(question);
                return Ok(questionDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", message = "An error occurred while processing the request." });
            }
        }


        [HttpPost]
        public ActionResult CreateQuestion([FromBody] QuestionCrudDTO question)
        {
            try
            {
                if (question is null)
                    return BadRequest("Invalid Question Data");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var examId = examRep.GetById(question.Exam_ID);

                if (examId is null)
                    return BadRequest("Invalid Exam");

              
                var model = mapper.Map<Questions>(question);

                
                questionRep.Add(model);

                return CreatedAtAction(nameof(GetId), new { id = model.Id }, new { Message = "Question added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }

        }

        [HttpPut("{id:int}")]
        public ActionResult updateQuestion(int id, QuestionCrudDTO question)
        {
            try
            {
                //if (question is null || id != question.Id)
                //    return BadRequest("Invalid Question Data");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var existingQuestion = questionRep.GetById(id);

                if (existingQuestion is null)
                    return NotFound("Question Not Found");

                var exam = examRep.GetById(question.Exam_ID);

                if (exam == null)
                    return BadRequest("Invalid Exam ID");

                mapper.Map(question, existingQuestion);

                existingQuestion.Id = id;

                questionRep.Update(existingQuestion);

                return Ok(new { Message = "Question Updated Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult deleteQuestion(int id)
        {
            try
            {
                var existingQuestion = questionRep.GetById(id);

                if (existingQuestion is null)
                    return NotFound("Question not found.");

                questionRep.Delete(existingQuestion);

                return Ok(new { Message = "Question deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
