using AutoMapper;
using LMS.BL.DTO;
using LMS.BL.Interface;
using LMS.BL.Repository;
using LMS.DAL.Entity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    //[Authorize(Policy = "AdminPolicy")]
    public class SubadminController : ControllerBase
    {
        private readonly IUserRep subadminRep;
        private readonly IUploadFile uploadFile;
        private readonly IMapper mapper;

        public string ServerRootPath { get { return $"{Request.Scheme}://{Request.Host}{Request.PathBase}"; } }

        public SubadminController(IUserRep subadminRep, IUploadFile uploadFile, IMapper mapper)
        {
            this.subadminRep = subadminRep;
            this.uploadFile = uploadFile;
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<SubAdminDTO>> GetAll()
        {
            var AllData = subadminRep.GetAllData();
            var subadmins = AllData.Where(a => a.Role == "subadmin").Select(a=>a);
            var sunadminDtos = mapper.Map<IEnumerable<SubAdminDTO>>(subadmins);
            foreach (var item in sunadminDtos)
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

                    if (subadminDtos.UserAttachmentPath != null)
                    {
                        if (subadminDtos.UserAttachmentPath.StartsWith("\\"))
                        {
                            if (!string.IsNullOrEmpty(subadminDtos.UserAttachmentPath))
                            {
                                subadminDtos.UserAttachmentPath = ServerRootPath + subadminDtos.UserAttachmentPath.Replace('\\', '/');
                            }
                        }
                    }
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
        public async Task<IActionResult> Add([FromForm] SubAdminDTO subadmin)
        {
            try
            {
                if (subadmin == null)
                    return BadRequest("Invalid Subadmin data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = mapper.Map<Users>(subadmin);
                data.Role = "subadmin";

                Random rnd = new Random();
                var path = $"Images\\Subadmins\\Subadmin{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Second}_{rnd.Next(9000)}";
                var attachmentPath = await uploadFile.UploadFileServices(subadmin.ImageFile, path);
                data.UserAttachmentPath = attachmentPath;

                subadminRep.Add(data);

                return CreatedAtAction(nameof(GetSubadminById), new { id = data.Id }, new { Message = "Subadmin added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] SubAdminDTO subadmin)
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

                if (subadmin.ImageFile != null)
                {
                    Random rnd = new Random();
                    var path = $"Images\\Subadmins\\Ssubadmin{DateTime.Now.Year}_{DateTime.Now.Month}_{DateTime.Now.Day}_{DateTime.Now.Second}_{rnd.Next(9000)}";
                    var attachmentPath = await uploadFile.UploadFileServices(subadmin.ImageFile, path);
                    existingSubadmin.UserAttachmentPath = attachmentPath;
                }

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
