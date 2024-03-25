using AutoMapper;
using LMS.BL.DTO;
using LMS.BL.Interface;
using LMS.BL.Repository;
using LMS.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Policy = "AdminAndSubAdminPolicy")]
    public class CoursesController : ControllerBase
    {
        private readonly ICourseRep courseRep;
        private readonly IMapper mapper;

        public CoursesController(ICourseRep courseRep , IMapper mapper)
        {
            this.courseRep = courseRep;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CoursesWithNumberOfExamDTO>> GetAll()
        {
            var courses = courseRep.GetAllData();
            var courseDTO = mapper.Map<IEnumerable<CoursesWithNumberOfExamDTO>>(courses);
            return Ok(courseDTO);
        }

        [HttpGet("{id:int}")]
        public ActionResult<CoursesWithNumberOfExamDTO> GetId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { error = "Invalid ID", message = "ID must be a positive integer." });

                var courses = courseRep.GetById(id);

                var courseDtos = mapper.Map<CoursesWithNumberOfExamDTO>(courses);
                return Ok(courseDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", message = "An error occurred while processing the request." });
            }
        }

        [HttpGet("{name:alpha}")]
        public ActionResult<CoursesWithNumberOfExamDTO> GetName(string name)
        {
            try
            {
                if (name is null)
                    return BadRequest(new { error = "Invalid Name", message = "Name cannot be null." });

                var course = courseRep.GetByName(name);

                var courseDtos = mapper.Map<CoursesWithNumberOfExamDTO>(course);
                return Ok(courseDtos);
        }
            catch (Exception ex)
            {
                return StatusCode(404, new { error = "Not Found", message = "Course with provided Name not found" });
            }
        }

        [HttpPost]
        public ActionResult CreateCourse([FromBody] CoursesDTO course)
        {
            try
            { 
                if (course is null)
                    return BadRequest("Invalid Course Data");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var model = mapper.Map<Courses>(course);
                courseRep.Add(model);

                return CreatedAtAction(nameof(GetId), new { id = model.Id }, new { Message = "Course added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }

        }

        [HttpPut("{id:int}")]
        public ActionResult updateCourse(int id, CoursesDTO courseDto)
        {
            try {
                //if (courseDto is null || id != courseDto.Id)
                //    return BadRequest("Invalid Course Data");
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);
                var existingCourse = courseRep.GetById(id);

                if (existingCourse is null)
                    return NotFound("Course Not Found");

                mapper.Map(courseDto, existingCourse);

                existingCourse.Id = id;
                courseRep.Update(existingCourse);

                return Ok(new { Message = "Course Updated Successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id:int}")]
        public ActionResult deleteCourse(int id)
        {
            try
            {
                var existingCourse = courseRep.GetById(id);

                if (existingCourse is null)
                    return NotFound("Course not found.");

                courseRep.Delete(existingCourse);

                return Ok(new { Message = "Course deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }
    }
}
