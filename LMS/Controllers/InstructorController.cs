using AutoMapper;
using LMS.BL.DTO;
using LMS.BL.Interface;
using LMS.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize(Policy = "AdminAndSubAdminPolicy")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRep instRep;
        private readonly IMapper mapper;
        private readonly ICourseRep courseRep;
       

        public InstructorController(IInstructorRep instRep, IMapper mapper , ICourseRep courseRep)
        {
            this.instRep = instRep;
            this.mapper = mapper;
            this.courseRep = courseRep;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InstructorsWithCourseNameDTO>> GetAll()
        {
            var instructors = instRep.GetAllData();
            var instDtos = mapper.Map<IEnumerable<InstructorsWithCourseNameDTO>>(instructors);
            return Ok(instDtos);
        }

        [HttpGet("{id:int}")]
        public ActionResult<InstructorsWithCourseNameDTO> GetId(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest(new { error = "Invalid ID", message = "ID must be a positive integer." });
                }

                var instructor = instRep.GetById(id);
                if (instructor == null || instructor.userID != id)
                {
                    return NotFound(new { error = "Instructor not found", message = $"Instructor with ID {id} is not found." });
                }

                var instDtos = mapper.Map<InstructorsWithCourseNameDTO>(instructor);
                return Ok(instDtos);
            }
            catch (Exception ex)
            {

                return StatusCode(500, new { error = "Internal Server Error", message = "An error occurred while processing the request." });
            }
        }


        [HttpGet("{name}")]
        public ActionResult<InstructorsWithCourseNameDTO> GetByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest(new { error = "Invalid Name", message = "Name cannot be empty or null." });
                

                var instructor = instRep.GetByName(name);
                if (instructor == null)
                    return NotFound(new { error = "Instructor not found", message = $"Instructor with name {name} is not found." });
                

                var instDtos = mapper.Map<InstructorsWithCourseNameDTO>(instructor);
                return Ok(instDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", message = "An error occurred while processing the request." });
            }
        }


        [HttpPost]
        public IActionResult Add([FromBody] InstructorsWithCourseNameDTO inst)
        {
            try
            {
                if (inst == null)
                    return BadRequest("Invalid instructor data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = mapper.Map<Instructors>(inst);
                data.Users.Role = "instructor";
                

                foreach (var nameOfCourse in inst.CourseName)
                {
                    var course = courseRep.GetByName(nameOfCourse);

                    if (course != null)
                    {
                        var instructorCourse = new InstructorCourse
                        {
                            inst_ID = data.userID,
                            Course_ID = course.Id
                        };

                        data.InstructorCourse.Add(instructorCourse);
                    }
                    else
                    {
                        return BadRequest("Invalid course name: " + nameOfCourse);
                    }
                }

                instRep.Add(data);

                return CreatedAtAction(nameof(GetId), new { id = data.userID }, new { Message = "Instructor added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] InstructorsWithCourseNameDTO inst)
        {
            try
            {
                if (inst == null || id != inst.Id)
                    return BadRequest("Invalid instructor data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingInstructor = instRep.GetById(id);

                if (existingInstructor == null)
                    return NotFound("Instructor not found.");

                mapper.Map(inst, existingInstructor);
                existingInstructor.Users.Role = "instructor";

                existingInstructor.InstructorCourse.Clear();

                foreach (var courseName in inst.CourseName)
                {
                    var course = courseRep.GetByName(courseName);

                    if (course != null)
                    {
                        var instructorCourse = new InstructorCourse
                        {
                            inst_ID = existingInstructor.userID,
                            Course_ID = course.Id
                        };

                        existingInstructor.InstructorCourse.Add(instructorCourse);
                    }
                    else
                    {
                        return BadRequest("Invalid course name: " + courseName);
                    }
                }

                instRep.Update(existingInstructor);

                return Ok(new { Message = "Instructor updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var existingInstructor = instRep.GetById(id);

                if (existingInstructor == null)
                    return NotFound("Instructor not found.");

                instRep.Delete(existingInstructor);

                return Ok(new { Message = "Instructor deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


    }

}
