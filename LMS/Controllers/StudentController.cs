using AutoMapper;
using LMS.BL.DTO;
using LMS.BL.Interface;
using LMS.BL.Repository;
using LMS.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRep stdRep;
        private readonly IMapper mapper;
        private readonly IUserRep userRep;

        public StudentController(IStudentRep stdRep, IMapper mapper, IUserRep userRep)
        {
            this.stdRep = stdRep;
            this.mapper = mapper;
            this.userRep = userRep;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentWithExamAndInstrcutorCourses>> GetAllStudents()
        {
            var students = stdRep.GetAllData();
            var studentDtos = mapper.Map<IEnumerable<StudentWithExamAndInstrcutorCourses>>(students);
            return Ok(studentDtos);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<StudentWithExamAndInstrcutorCourses> GetStudentById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { error = "Invalid ID", message = "ID must be a positive integer." });


                var student = stdRep.GetById(id);
                if (student == null || student.userID != id)
                    return NotFound(new { error = "Student not found", message = $"Student with ID {id} is not found." });


                var studentDtos = mapper.Map<StudentWithExamAndInstrcutorCourses>(student);
                return Ok(studentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", message = "An error occurred while processing the request." });
            }
        }

        [HttpGet]
        [Route("{name:alpha}")]
        public ActionResult<StudentWithExamAndInstrcutorCourses> GetStudentByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest(new { error = "Invalid Name", message = "Name cannot be empty or null." });


                var student = stdRep.GetByName(name);

                var studentDtos = mapper.Map<StudentWithExamAndInstrcutorCourses>(student);
                return Ok(studentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { error = "Not Found", message = "Student with provided Name not found" });
            }
        }


        [HttpPost]
        public IActionResult Add([FromBody] StudentCrudDTO std)
        {
            try
            {
                if (std == null)
                    return BadRequest("Invalid student data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = mapper.Map<Students>(std);
                data.Users.Role = "student";

                stdRep.Add(data);

                return CreatedAtAction(nameof(GetStudentById), new { id = data.userID }, new { Message = "Student added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] StudentCrudDTO std)
        {
            try
            {
                if (std is null || id != std.Id)
                    return BadRequest("Invalid student data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingStudent = stdRep.GetById(id);

                if (existingStudent == null)
                    return NotFound("Student not found.");

                mapper.Map(std, existingStudent);
                existingStudent.Users.Role = "student";

                stdRep.Update(existingStudent);

                return Ok(new { Message = "Student updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteInstructor(int id)
        {
            try
            {
                var existingstudent = stdRep.GetById(id);
                var UserInstructor = userRep.GetById(id);

                if (existingstudent is null && UserInstructor is null)
                    return NotFound("Student not found.");

                stdRep.Delete(existingstudent);
                userRep.Delete(UserInstructor);

                return Ok(new { Message = "Student deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

    }
}
