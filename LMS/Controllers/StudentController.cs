using LMS.BL.Interface;
using LMS.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRep stdRep;

        public StudentController(IStudentRep stdRep)
        {
            this.stdRep = stdRep;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Students>> GetAllStudents()
        {
            var students = stdRep.GetAllData();
            return Ok(students);
        }

        [HttpGet]
        [Route("{id:int}")]
        public ActionResult<Students> GetStudentById(int id)
        {
            var students = stdRep.GetById(id);
            return students == null ? NotFound() : Ok(students);
        }

        [HttpGet]
        [Route("{name:alpha}")]
        public ActionResult<Students> GetStudentByName(string name)
        {
            var students = stdRep.GetByName(name);
            return students == null ? NotFound() : Ok(students);
        }
    }
}
