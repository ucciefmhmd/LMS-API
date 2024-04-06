using AutoMapper;
using LMS.BL.DTO;
using LMS.BL.Interface;
using LMS.BL.Repository;
using LMS.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Policy = "AdminAndSubAdminPolicy")]
    public class InstructorController : ControllerBase
    {
        private readonly IInstructorRep instRep;
        private readonly IUploadFile uploadFile;
        private readonly IMapper mapper;
        private readonly ICourseRep courseRep;
        private readonly IUserRep userRep;

        public string ServerRootPath { get { return $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; } }

        public InstructorController(IInstructorRep instRep, IUploadFile uploadFile, IMapper mapper , ICourseRep courseRep, IUserRep userRep)
        {
            this.instRep = instRep;
            this.uploadFile = uploadFile;
            this.mapper = mapper;
            this.courseRep = courseRep;
            this.userRep = userRep;
        }

        [HttpGet]
        public ActionResult<IEnumerable<InstructorsWithCourseNameDTO>> GetAll()
        {
            var instructors = instRep.GetAllData();
            var instDtos = mapper.Map<IEnumerable<InstructorsWithCourseNameDTO>>(instructors);
            foreach (var item in instDtos)
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
            return Ok(instDtos);
        }

        [HttpGet("{id:int}")]
        public ActionResult<InstructorsWithCourseNameDTO> GetId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { error = "Invalid ID", message = "ID must be a positive integer." });
                

                var instructor = instRep.GetById(id);
                if (instructor == null || instructor.userID != id)
                    return NotFound(new { error = "Instructor not found", message = $"Instructor with ID {id} is not found." });
                

                var instDtos = mapper.Map<InstructorsWithCourseNameDTO>(instructor);
                if (instDtos.UserAttachmentPath != null)
                {
                    if (instDtos.UserAttachmentPath.StartsWith("\\"))
                    {
                        if (!string.IsNullOrEmpty(instDtos.UserAttachmentPath))
                        {
                            instDtos.UserAttachmentPath = ServerRootPath + instDtos.UserAttachmentPath.Replace('\\', '/');
                        }
                    }
                }
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

                var instDtos = mapper.Map<InstructorsWithCourseNameDTO>(instructor);
                return Ok(instDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(404, new { error = "Not Found", message = "Instructor with provided Name not found" });
            }
        }


        [HttpPost("addCourse")]
        public async Task<IActionResult> AddCourseToInstructor([FromBody] InstructorWithCourseDTO instCourse)
        {
            try
            {
                var inst = instRep.GetById(instCourse.InstructorId);
                if (inst == null)
                    return BadRequest($"Instructor with ID {instCourse.InstructorId} not found.");

                foreach (var idOfCourse in instCourse.CourseIDs)
                {
                    var course = courseRep.GetById(idOfCourse);

                    if (course != null)
                    {
                        var instructorCourse = new InstructorCourse
                        {
                            inst_ID = inst.userID,
                            Course_ID = course.Id
                        };

                        inst.InstructorCourse.Add(instructorCourse);
                    }
                    else
                        return BadRequest("Invalid course id: " + idOfCourse);

                }


                instRep.Update(inst);
                return CreatedAtAction(nameof(GetId), new { id = instCourse.InstructorId }, new { Message = $"Course added to Instructor successfully." });
            }

            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }


        [HttpGet("names/{id:int}")]
        public ActionResult<IEnumerable<InstructorDataDTO>> GetInstructorsByCourse(int id)
        {
            try
            {
                var instructors = instRep.GetInstructorNamesByCourse(id);

                if (instructors == null || !instructors.Any())
                    return NotFound("No instructors found for the provided course ID.");

                var instDtos = mapper.Map<IEnumerable<InstructorDataDTO>>(instructors);

                return Ok(instDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }




        [HttpPost]
        public async Task<IActionResult> Add([FromForm] InstructorsWithCourseNameDTO inst)
        {
            try
            {
                if (inst == null)
                    return BadRequest("Invalid instructor data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = mapper.Map<Instructors>(inst);
                data.Users.Role = "instructor";
                

                foreach (var idOfCourse in inst.CourseIDs)
                {
                    var course = courseRep.GetById(idOfCourse);

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
                        return BadRequest("Invalid course id: " + idOfCourse);
                    
                }

                Random rnd = new Random();
                var path = $"Images\\Instructors\\Instructor{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Second}_{rnd.Next(9000)}";
                var attachmentPath = await uploadFile.UploadFileServices(inst.ImageFile, path);
                data.Users.UserAttachmentPath = attachmentPath;


                instRep.Add(data);

                return CreatedAtAction(nameof(GetId), new { id = data.userID }, new { Message = "Instructor added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] InstructorsWithCourseNameDTO inst)
        {
            try
            {
                //if (inst is null || id != inst.Id)
                //    return BadRequest("Invalid instructor data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingInstructor = instRep.GetById(id);

                if (existingInstructor == null)
                    return NotFound("Instructor not found.");

                mapper.Map(inst, existingInstructor);
                existingInstructor.Users.Role = "instructor";

                
                //existingInstructor.InstructorCourse.Clear();

                foreach (var courseID in inst.CourseIDs)
                {
                    var course = courseRep.GetById(courseID);

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
                        return BadRequest("Invalid course Id: " + courseID);
                    
                }


                if (inst.ImageFile != null)
                {
                    Random rnd = new Random();
                    var path = $"Images\\Instructors\\Instructor{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Second}_{rnd.Next(9000)}";
                    var attachmentPath = await uploadFile.UploadFileServices(inst.ImageFile, path);
                    existingInstructor.Users.UserAttachmentPath = attachmentPath;
                }

                existingInstructor.userID = id;
                instRep.Update(existingInstructor);

                return Ok(new { Message = "Instructor updated successfully." });
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
                var existingInstructor = instRep.GetById(id);
                var UserInstructor = userRep.GetById(id);

                if (existingInstructor is null && UserInstructor is null)
                    return NotFound("Instructor not found.");

                instRep.Delete(existingInstructor);
                userRep.Delete(UserInstructor);

                return Ok(new { Message = "Instructor deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }



    }

}
