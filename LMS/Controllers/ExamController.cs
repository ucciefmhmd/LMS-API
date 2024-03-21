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

        public ExamController(IExamRep examRep, IMapper mapper, ICourseRep courseRep, IStudentRep stdRep )
        {
            this.examRep = examRep;
            this.mapper = mapper;
            this.courseRep = courseRep;
            this.stdRep = stdRep;
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

                foreach (var IdOfStd in exam.StudentIDs)
                {
                    var examstd = stdRep.GetById(IdOfStd);

                    if (examstd != null)
                    {
                        var studentExam = new StudentExam
                        {
                            Std_ID = examstd.userID,
                            Exam_ID = model.Id
                        };

                        model.StudentExam.Add(studentExam);
                    }
                    else
                        return BadRequest("Invalid Student Id: " + IdOfStd);

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
                if (examDto is null || id != examDto.Id)
                    return BadRequest("Invalid Exam Data");
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);
                var existingExam = examRep.GetById(id);

                if (existingExam is null)
                    return NotFound("Exam Not Found");

                var course = courseRep.GetById(examDto.Course_ID);

                if (course == null)
                    return BadRequest("Invalid Course ID");

                mapper.Map(examDto, existingExam);

                existingExam.StudentExam.Clear();

                foreach (var IdOfStd in examDto.StudentIDs)
                {
                    var examstd = stdRep.GetById(IdOfStd);

                    if (examstd != null)
                    {
                        var studentExam = new StudentExam
                        {
                            Std_ID = examstd.userID,
                            Exam_ID = existingExam.Id
                        };

                        existingExam.StudentExam.Add(studentExam);
                    }
                    else
                        return BadRequest("Invalid Student Id: " + IdOfStd);

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
