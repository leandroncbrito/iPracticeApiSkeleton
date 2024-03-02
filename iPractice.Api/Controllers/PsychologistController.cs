using System;
using System.Net;
using System.Threading.Tasks;
using iPractice.Api.Models;
using iPractice.Application.Commands;
using iPractice.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iPractice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PsychologistController : ControllerBase
    {
        private readonly ILogger<PsychologistController> _logger;
        private readonly ICommandHandler<CreateAvailabilityCommand> _createAvailabilityCommandHandler;

        public PsychologistController(ICommandHandler<CreateAvailabilityCommand> createAvailabilityCommandHandler, ILogger<PsychologistController> logger)
        {
            _createAvailabilityCommandHandler = createAvailabilityCommandHandler;
            _logger = logger;
        }

        [HttpGet]
        public string Get()
        {
            return "Success!";
        }

        /// <summary>
        /// Add a block of time during which the psychologist is available during normal business hours
        /// </summary>
        /// <param name="psychologistId"></param>
        /// <param name="availability">Availability</param>
        /// <returns>Ok if the availability was created</returns>
        [HttpPost("{psychologistId}/availability")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateAvailability([FromRoute] long psychologistId, [FromBody] Availability availability)
        {
            try
            {
                var createAvailabilityCommand = new CreateAvailabilityCommand
                {
                    PsychologistId = psychologistId,
                    From = availability.From,
                    To = availability.To
                };
            
                await _createAvailabilityCommandHandler.HandleAsync(createAvailabilityCommand);
            
                var uri = new Uri($"{Request.Scheme}://{Request.Host}/api/psychologists/{psychologistId}");
            
                return Created(uri, true);
            }
            //@TODO: map to custom error response
            catch (NullReferenceException e) 
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create availability");
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }

        /// <summary>
        /// Update availability of a psychologist
        /// </summary>
        /// <param name="psychologistId">The psychologist's ID</param>
        /// <param name="availabilityId">The ID of the availability block</param>
        /// <returns>List of availability slots</returns>
        [HttpPut("{psychologistId}/availability/{availabilityId}")]
        [ProducesResponseType(typeof(Availability), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Availability>> UpdateAvailability([FromRoute] long psychologistId, [FromRoute] long availabilityId, [FromBody] Availability availability)
        {
            throw new NotImplementedException();
        }
    }
}
