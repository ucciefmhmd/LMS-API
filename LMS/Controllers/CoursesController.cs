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
        private readonly IUploadFile uploadFile;
        private readonly IMapper mapper;
        public string ServerRootPath { get { return $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; } }

        public CoursesController(ICourseRep courseRep, IUploadFile uploadFile , IMapper mapper)
        {
            this.courseRep = courseRep;
            this.uploadFile = uploadFile;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CoursesWithNumberOfExamDTO>> GetAll()
        {
            var courses = courseRep.GetAllData();
            var courseDTO = mapper.Map<IEnumerable<CoursesWithNumberOfExamDTO>>(courses);
            foreach (var item in courseDTO)
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
                if (courseDtos.UserAttachmentPath != null)
                {
                    if (courseDtos.UserAttachmentPath.StartsWith("\\"))
                    {
                        if (!string.IsNullOrEmpty(courseDtos.UserAttachmentPath))
                        {
                            courseDtos.UserAttachmentPath = ServerRootPath + courseDtos.UserAttachmentPath.Replace('\\', '/');
                        }
                    }
                }
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
        public async Task<ActionResult> CreateCourse([FromForm] CoursesDTO course)
        {
            try
            { 
                if (course is null)
                    return BadRequest("Invalid Course Data");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var model = mapper.Map<Courses>(course);

                Random rnd = new Random();
                var path = $"Images\\Courses\\Course{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Second}_{rnd.Next(9000)}";
                var attachmentPath = await uploadFile.UploadFileServices(course.ImageFile, path);
                model.UserAttachmentPath = attachmentPath;


                courseRep.Add(model);

                return CreatedAtAction(nameof(GetId), new { id = model.Id }, new { Message = "Course added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }

        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> updateCourse(int id,[FromForm] CoursesDTO courseDto)
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

                if (courseDto.ImageFile != null)
                {
                    Random rnd = new Random();
                    var path = $"Images\\Students\\Student{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Second}_{rnd.Next(9000)}";
                    var attachmentPath = await uploadFile.UploadFileServices(courseDto.ImageFile, path);
                    existingCourse.UserAttachmentPath = attachmentPath;
                }

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
