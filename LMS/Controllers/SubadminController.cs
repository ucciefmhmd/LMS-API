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
    public class SubadminController : ControllerBase
    {
        private readonly IUserRep subadminRep;
        private readonly IMapper mapper;

        public SubadminController(IUserRep subadminRep , IMapper mapper)
        {
            this.subadminRep = subadminRep;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SubAdminDTO>> GetAll()
        {
            var AllData = subadminRep.GetAllData();
            var subadmins = AllData.Where(a => a.Role == "subadmin").Select(a=>a);
            var sunadminDtos = mapper.Map<IEnumerable<SubAdminDTO>>(subadmins);
            return Ok(sunadminDtos);
        }

        [HttpGet("{id:int}")]
        public ActionResult<SubAdminDTO> GetSubadminById(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { error = "Invalid ID", message = "ID must be a positive integer." });


                var subadmin = subadminRep.GetById(id);
                if(subadmin.Role == "subadmin")
                {
                    if (subadmin == null)
                        return NotFound(new { error = "Subadmin not found", message = $"Subadmin with ID {id} is not found." });
                    

                    var subadminDtos = mapper.Map<SubAdminDTO>(subadmin);
                    return Ok(subadminDtos);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", message = "An error occurred while processing the request." });
            }
        }

        [HttpGet("{name:alpha}")]
        public ActionResult<SubAdminDTO> GetSubadminByName(string name)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(name))
                    return BadRequest(new { error = "Invalid Name", message = "Name cannot be empty or null." });


                var subadmin = subadminRep.GetByName(name);
                if (subadmin.Role == "subadmin")
                {
                    if (subadmin == null)
                        return NotFound(new { error = "Subadmin not found", message = $"Subadmin with Name {name} is not found." });


                    var subadminDtos = mapper.Map<SubAdminDTO>(subadmin);
                    return Ok(subadminDtos);
                }
                return NotFound();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", message = "An error occurred while processing the request." });
            }
        }


        [HttpPost]
        public IActionResult Add([FromBody] SubAdminDTO subadmin)
        {
            try
            {
                if (subadmin == null)
                    return BadRequest("Invalid Subadmin data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = mapper.Map<Users>(subadmin);
                data.Role = "subadmin";

                subadminRep.Add(data);

                return CreatedAtAction(nameof(GetSubadminById), new { id = data.Id }, new { Message = "Subadmin added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }


        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] SubAdminDTO subadmin)
        {
            try
            {
                //if (subadmin is null || id != subadmin.Id)
                //    return BadRequest("Invalid Subadmin data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingSubadmin = subadminRep.GetById(id);

                if (existingSubadmin == null)
                    return NotFound("Subadmin not found.");

                mapper.Map(subadmin, existingSubadmin);
                existingSubadmin.Role = "subadmin";

                existingSubadmin.Id = id;
                subadminRep.Update(existingSubadmin);

                return Ok(new { Message = "Subadmin updated successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteSubadmin(int id)
        {
            try
            {
                var existingSubadmin = subadminRep.GetById(id);

                if (existingSubadmin is null)
                    return NotFound("Subadmin not found.");

                subadminRep.Delete(existingSubadmin);

                return Ok(new { Message = "Subadmin deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }


    }
}
