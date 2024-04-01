using AutoMapper;
using LMS.BL.DTO;
using LMS.BL.Interface;
using LMS.BL.Repository;
using LMS.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ExamController : ControllerBase
    {
        private readonly IExamRep examRep;
        private readonly IMapper mapper;
        private readonly ICourseRep courseRep;
        private readonly IStudentRep stdRep;
        private readonly IQuestionRep quesRep;

        public ExamController(IExamRep examRep, IMapper mapper, ICourseRep courseRep, IStudentRep stdRep , IQuestionRep quesRep)
        {
            this.examRep = examRep;
            this.mapper = mapper;
            this.courseRep = courseRep;
            this.stdRep = stdRep;
            this.quesRep = quesRep;
        }

        [HttpGet]
        public ActionResult<IEnumerable<ExamsWithQuestionsAndCoursesDTO>> GetAll()
        {
            var exams = examRep.GetAllData();
            var examDTO = mapper.Map<IEnumerable<ExamsWithQuestionsAndCoursesDTO>>(exams);
            return Ok(examDTO);
        }

        [HttpGet("{id:int}")]
        public ActionResult<ExamsWithQuestionsAndCoursesDTO> GetId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { error = "Invalid ID", message = "ID must be a positive integer." });

                var exam = examRep.GetById(id);

                var examDtos = mapper.Map<ExamsWithQuestionsAndCoursesDTO>(exam);
                return Ok(examDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", message = "An error occurred while processing the request." });
            }
        }

        [HttpGet("{name:alpha}")]
        public ActionResult<ExamsWithQuestionsAndCoursesDTO> GetName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest(new { error = "Invalid Name", message = "Name cannot be empty or null." });


                var exam = examRep.GetByName(name);

                var examDtos = mapper.Map<ExamsWithQuestionsAndCoursesDTO>(exam);
                return Ok(examDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { error = "Not Found", message = "Exam with provided Name not found" });
            }
        }


        [HttpPost]
        public ActionResult CreateExam([FromBody] ExamsWithQuestionsAndCoursesDTO exam)
        {
            try
            {
                if (exam is null)
                    return BadRequest("Invalid Exam Data");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var course = courseRep.GetById(exam.Course_ID);

                if (course == null)
                    return BadRequest("Invalid Course ID");

                var model = mapper.Map<Exam>(exam);

                //foreach (var questionId in exam.QuestionIDs)
                //{
                //    var question = quesRep.GetById(questionId);

                //    if (question != null)
                //    {
                //        Console.WriteLine(question);
                //        model.Questions.Add(question);
                //    }
                //    else
                //    {
                //        return BadRequest($"Invalid Question ID: {questionId}");
                //    }
                //}

                var questions = exam.AllQuestion.Select(question => mapper.Map<Questions>(question)).ToList();


                foreach (var question in questions)
                {
                    model.Questions.Add(question);
                }


                examRep.Add(model);

                return CreatedAtAction(nameof(GetId), new { id = model.Id }, new { Message = "Exam added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }

        }

        [HttpPut("{id:int}")]
        public ActionResult updateCourse(int id, ExamsWithQuestionsAndCoursesDTO examDto)
        {
            try
            {
                //if (examDto is null || id != examDto.Id)
                //    return BadRequest("Invalid Exam Data");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var existingExam = examRep.GetById(id);

                if (existingExam is null)
                    return NotFound("Exam Not Found");

                var course = courseRep.GetById(examDto.Course_ID);

                if (course == null)
                    return BadRequest("Invalid Course ID");

                mapper.Map(examDto, existingExam);

                existingExam.Id = id;

                var questions = examDto.AllQuestion.Select(question => mapper.Map<Questions>(question)).ToList();

                foreach (var question in questions)
                {
                    existingExam.Questions.Add(question);
                }

                examRep.Update(existingExam);

                return Ok(new { Message = "Exam Updated Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult deleteExam(int id)
        {
            try
            {
                var existingExam = examRep.GetById(id);

                if (existingExam is null)
                    return NotFound("Exam not found.");

                examRep.Delete(existingExam);

                return Ok(new { Message = "Exam deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
