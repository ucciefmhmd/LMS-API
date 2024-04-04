using AutoMapper;
using LMS.BL.DTO;
using LMS.BL.Interface;
using LMS.BL.Repository;
using LMS.DAL.Database;
using LMS.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly LMSContext db;
        private readonly IUploadFile uploadFile;
        private readonly IStudentRep stdRep;
        private readonly IMapper mapper;
        private readonly IUserRep userRep;
        private readonly ICourseRep courseRep;
        private readonly IInstructorRep instRep;
        public string ServerRootPath { get { return $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; } }

        public StudentController(LMSContext db, IUploadFile uploadFile ,IStudentRep stdRep, IMapper mapper, IUserRep userRep, ICourseRep courseRep, IInstructorRep instRep)
        {
            this.db = db;
            this.uploadFile = uploadFile;
            this.stdRep = stdRep;
            this.mapper = mapper;
            this.userRep = userRep;
            this.courseRep = courseRep;
            this.instRep = instRep;
        }

        [HttpGet]
        public ActionResult<IEnumerable<StudentWithExamAndInstrcutorCourses>> GetAllStudents()
        {
            var students = stdRep.GetAllData();
            var studentDtos = mapper.Map<IEnumerable<StudentWithExamAndInstrcutorCourses>>(students);
            foreach (var item in studentDtos)
            {
                if (item.UserAttachmentPath != null)
                {
                    if (item.UserAttachmentPath.StartsWith("\\"))
                    {
                        if (!string.IsNullOrEmpty(item.UserAttachmentPath))
                        {
                            item.UserAttachmentPath = ServerRootPath + item.UserAttachmentPath.Replace('\\', '/');
                        }
                    }
                }
            }
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

                    if (studentDtos.UserAttachmentPath != null)
                    {
                        if (studentDtos.UserAttachmentPath.StartsWith("\\"))
                        {
                            if (!string.IsNullOrEmpty(studentDtos.UserAttachmentPath))
                            {
                            studentDtos.UserAttachmentPath = ServerRootPath + studentDtos.UserAttachmentPath.Replace('\\', '/');
                            }
                        }
                    }
                
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
                if (studentDtos.UserAttachmentPath != null)
                {
                    if (studentDtos.UserAttachmentPath.StartsWith("\\"))
                    {
                        if (!string.IsNullOrEmpty(studentDtos.UserAttachmentPath))
                        {
                            studentDtos.UserAttachmentPath = ServerRootPath + studentDtos.UserAttachmentPath.Replace('\\', '/');
                        }
                    }
                }
                return Ok(studentDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { error = "Not Found", message = "Student with provided Name not found" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Add([FromForm] StudentCrudDTO std)
        {
            try
            {
                if (std == null)
                    return BadRequest("Invalid student data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = mapper.Map<Students>(std);
                data.Users.Role = "student";

                foreach (var nameOfCourse in std.CourseName)

                {
                    var course = courseRep.GetByName(nameOfCourse);

                    if (course != null)
                    {
                        Console.WriteLine($"Found course: {course.Name}");

                        foreach (var instId in std.InstructorIDs)
                        {
                            var inst = instRep.GetById(instId);

                            if (inst != null)
                            {
                                Console.WriteLine($"Found instructor: {inst.userID}");

                                var instructorCourse = db.InstructorCourse
                                    .FirstOrDefault(ic => ic.Course_ID == course.Id && ic.inst_ID == inst.userID);

                                if (instructorCourse != null)
                                {
                                    Console.WriteLine($"Found instructor course: {instructorCourse.Id}");

                                    var group = new Group
                                    {
                                        Name = "any",
                                        Chat = "",
                                        Std_ID = data.userID,
                                        InstructorCourse = instructorCourse,
                                        InstCos_ID = instructorCourse.Id 
                                    };

                                    data.Group.Add(group);
                                }
                                else
                                {
                                    return BadRequest($"Instructor course not found for course: {nameOfCourse} and instructor: {inst.userID}");
                                }
                            }
                            else
                            {
                                return BadRequest($"Instructor with ID {instId} not found for course: {nameOfCourse}");
                            }
                        }
                    }
                    else
                    {
                        return BadRequest($"Invalid course name: {nameOfCourse}");
                    }
                }

                Random rnd = new Random();
                var path = $"Images\\Students\\Student{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Second}_{rnd.Next(9000)}";
                var attachmentPath = await uploadFile.UploadFileServices(std.ImageFile, path);
                data.Users.UserAttachmentPath = attachmentPath;

                stdRep.Add(data);

                return CreatedAtAction(nameof(GetStudentById), new { id = data.userID }, new { Message = "Student added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }

        [HttpPost("addCourse/{id:int}")]
        //[Route("{id:int}")]
        public async Task<IActionResult> AddCourseToStudent(int id, [FromBody] StudentWithCourseDTO stdCourse)
        {
            try
            {
                var student = stdRep.GetById(id);
                if (student == null)
                    return BadRequest($"Student with ID {id} not found.");

                foreach (var nameOfCourse in stdCourse.CourseName)
                {
                    var course = courseRep.GetByName(nameOfCourse);

                    if (course != null)
                    {
                        Console.WriteLine($"Found course: {course.Name}");

                        
                        if (student.Group.Any(g => g.InstructorCourse.Course_ID == course.Id))
                        {
                            return BadRequest($"Student already has the course: {nameOfCourse}");
                        }

                        foreach (var instId in stdCourse.InstructorIDs)
                        {
                            var inst = instRep.GetById(instId);

                            if (inst != null)
                            {
                                Console.WriteLine($"Found instructor: {inst.userID}");

                                var instructorCourse = db.InstructorCourse
                                    .FirstOrDefault(ic => ic.Course_ID == course.Id && ic.inst_ID == inst.userID);

                                if (instructorCourse != null)
                                {
                                    Console.WriteLine($"Found instructor course: {instructorCourse.Id}");

                                    var group = new Group
                                    {
                                        Name = "any",
                                        Chat = "",
                                        Std_ID = student.userID,
                                        InstructorCourse = instructorCourse,
                                        InstCos_ID = instructorCourse.Id
                                    };

                                    student.Group.Add(group);
                                }
                                else
                                {
                                    return BadRequest($"Instructor course not found for course: {nameOfCourse} and instructor: {inst.userID}");
                                }
                            }
                            else
                            {
                                return BadRequest($"Instructor with ID {instId} not found for course: {nameOfCourse}");
                            }
                        }
                    }
                    else
                    {
                        return BadRequest($"Invalid course name: {nameOfCourse}");
                    }
                }

                stdRep.Update(student);
                return CreatedAtAction(nameof(GetStudentById), new { Id = id }, new { Message = $"Course(s) added to student successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }




        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromForm] StudentCrudDTO std)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingStudent = stdRep.GetById(id);

                if (existingStudent == null)
                    return NotFound("Student not found.");

                mapper.Map(std, existingStudent);
                existingStudent.Users.Role = "student";

                existingStudent.Group.Clear();

                foreach (var nameOfCourse in std.CourseName)
                {
                    var course = courseRep.GetByName(nameOfCourse);

                    if (course != null)
                    {
                        Console.WriteLine($"Found course: {course.Name}");

                        foreach (var instId in std.InstructorIDs)
                        {
                            var inst = instRep.GetById(instId);

                            if (inst != null)
                            {
                                Console.WriteLine($"Found instructor: {inst.userID}");

                                var instructorCourse = db.InstructorCourse
                                    .FirstOrDefault(ic => ic.Course_ID == course.Id && ic.inst_ID == inst.userID);

                                if (instructorCourse != null)
                                {
                                    Console.WriteLine($"Found instructor course: {instructorCourse.Id}");

                                    var group = new Group
                                    {
                                        Name = "any",
                                        Chat = "",
                                        Std_ID = existingStudent.userID,
                                        InstructorCourse = instructorCourse,
                                        InstCos_ID = instructorCourse.Id
                                    };

                                    existingStudent.Group.Add(group);
                                }
                                else
                                {
                                    return BadRequest($"Instructor course not found for course: {nameOfCourse} and instructor: {inst.userID}");
                                }
                            }
                            else
                            {
                                return BadRequest($"Instructor with ID {instId} not found for course: {nameOfCourse}");
                            }
                        }
                    }
                    else
                    {
                        return BadRequest($"Invalid course name: {nameOfCourse}");
                    }
                }

                if (std.ImageFile != null)
                {
                    Random rnd = new Random();
                    var path = $"Images\\Students\\Student{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Second}_{rnd.Next(9000)}";
                    var attachmentPath = await uploadFile.UploadFileServices(std.ImageFile, path);
                    existingStudent.Users.UserAttachmentPath = attachmentPath;
                }

                existingStudent.userID = id;
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
