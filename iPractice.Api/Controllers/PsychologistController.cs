using System;
using System.Net;
using System.Threading.Tasks;
using iPractice.Api.Models;
using iPractice.Application.Commands;
using iPractice.Application.Interfaces;
using iPractice.Domain.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iPractice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PsychologistController : ControllerBase
    {
        private readonly ILogger<PsychologistController> _logger;
        private readonly ICommandHandler<CreateTimeSlotsCommand> _createTimeSlotsCommandHandler;
        private readonly ICommandHandler<UpdateAvailabilityCommand> _updateAvailabilityCommandHandler;

        public PsychologistController(
            ICommandHandler<CreateTimeSlotsCommand> createTimeSlotsCommandHandler,
            ICommandHandler<UpdateAvailabilityCommand> updateAvailabilityCommandHandler,
            ILogger<PsychologistController> logger)
        {
            _createTimeSlotsCommandHandler = createTimeSlotsCommandHandler;
            _updateAvailabilityCommandHandler = updateAvailabilityCommandHandler;
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
                var createTimeSlotsCommand = new CreateTimeSlotsCommand(psychologistId, availability.From, availability.To);
            
                await _createTimeSlotsCommandHandler.HandleAsync(createTimeSlotsCommand);
            
                return StatusCode((int)HttpStatusCode.Created, true);
            }
            catch (DomainException e) 
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled exception occurred while processing the request.");
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
            try
            {
                var updateAvailabilityCommand = new UpdateAvailabilityCommand(psychologistId, availabilityId, availability.From, availability.To);
                
                await _updateAvailabilityCommandHandler.HandleAsync(updateAvailabilityCommand);

                return StatusCode((int)HttpStatusCode.NoContent, true);
            }
            catch (DomainException e) 
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled exception occurred while processing the request.");
                return StatusCode((int)HttpStatusCode.InternalServerError, e);
            }
        }
    }
}
