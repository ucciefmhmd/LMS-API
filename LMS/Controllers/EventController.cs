using AutoMapper;
using LMS.BL.DTO;
using LMS.BL.Interface;
using LMS.BL.Repository;
using LMS.DAL.Entity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMS.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRep eventRep;
        private readonly IMapper mapper;
        private readonly ICourseRep courseRep;

        public EventController(IEventRep eventRep, IMapper mapper, ICourseRep courseRep)
        {
            this.eventRep = eventRep;
            this.mapper = mapper;
            this.courseRep = courseRep;
        }
        [HttpGet]
        public ActionResult<IEnumerable<EventDto>> GetAllEvents()
        {
            var events = eventRep.GetAllData();
            var eventDtos = mapper.Map<IEnumerable<EventDto>>(events);
            return Ok(eventDtos);
        }

        [HttpGet("{id:int}")]
        public ActionResult<EventDto> GetId(int id)
        {
            try
            {
                if (id <= 0)
                    return BadRequest(new { error = "Invalid ID", message = "ID must be a positive integer." });


                var eventData = eventRep.GetById(id);
                if (eventData == null)
                    return NotFound(new { error = "Event not found", message = $"Event with ID {id} is not found." });


                var eventDtos = mapper.Map<EventDto>(eventData);
                return Ok(eventDtos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Internal Server Error", message = "An error occurred while processing the request." });
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] EventDto eve)
        {
            try
            {
                if (eve == null)
                    return BadRequest("Invalid event data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var data = mapper.Map<Events>(eve);

                foreach (var nameOfCourse in eve.CoursesName)
                {
                    var course = courseRep.GetByName(nameOfCourse);

                    if (course != null)
                    {
                        var eventCourse = new EventsCourses
                        {
                            Event_ID = data.Id,
                            Course_ID = course.Id
                        };

                        data.EventsCourses.Add(eventCourse);
                    }
                    else
                        return BadRequest("Invalid course name: " + nameOfCourse);

                }

                eventRep.Add(data);

                return CreatedAtAction(nameof(GetId), new { id = data.Id }, new { Message = "Event added successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request: " + ex.Message);
            }
        }



        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] EventDto eve)
        {
            try
            {
                //if (eve is null || id != eve.Id)
                //    return BadRequest("Invalid event data.");

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var existingEvent = eventRep.GetById(id);

                if (existingEvent == null)
                    return NotFound("Event not found.");

                mapper.Map(eve, existingEvent);

                foreach (var nameOfCourse in eve.CoursesName)
                {
                    var course = courseRep.GetByName(nameOfCourse);

                    if (course != null)
                    {
                        var eventCourse = new EventsCourses
                        {
                            Event_ID = eve.Id,
                            Course_ID = course.Id
                        };

                        existingEvent.EventsCourses.Add(eventCourse);
                    }
                    else
                        return BadRequest("Invalid course name: " + nameOfCourse);

                }
                
                existingEvent.Id = id;
                eventRep.Update(existingEvent);

                return Ok(new { Message = "Event updated successfully." });
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
                var existingEvent = eventRep.GetById(id);

                if (existingEvent is null)
                    return NotFound("Event not found.");

                eventRep.Delete(existingEvent);

                return Ok(new { Message = "Event deleted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while processing your request.");
            }
        }



    }
}
