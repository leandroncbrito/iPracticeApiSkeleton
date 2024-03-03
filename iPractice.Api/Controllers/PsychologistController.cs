using System.Threading.Tasks;
using iPractice.Api.Models;
using iPractice.Application.Commands;
using iPractice.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iPractice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PsychologistController : ControllerBase
    {
        private readonly ICommandHandler<CreateAvailabilityCommand> _createTimeSlotsCommandHandler;
        private readonly ICommandHandler<UpdateAvailabilityCommand> _updateAvailabilityCommandHandler;

        public PsychologistController(
            ICommandHandler<CreateAvailabilityCommand> createTimeSlotsCommandHandler,
            ICommandHandler<UpdateAvailabilityCommand> updateAvailabilityCommandHandler)
        {
            _createTimeSlotsCommandHandler = createTimeSlotsCommandHandler;
            _updateAvailabilityCommandHandler = updateAvailabilityCommandHandler;
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
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAvailability([FromRoute] long psychologistId, [FromBody] Availability availability)
        {
            var createTimeSlotsCommand = new CreateAvailabilityCommand(psychologistId, availability.From, availability.To);
        
            await _createTimeSlotsCommandHandler.HandleAsync(createTimeSlotsCommand);
        
            return StatusCode(StatusCodes.Status201Created, true);
        }

        /// <summary>
        /// Update availability of a psychologist
        /// </summary>
        /// <param name="psychologistId">The psychologist's ID</param>
        /// <param name="availabilityId">The ID of the availability block</param>
        /// <returns>List of availability slots</returns>
        [HttpPut("{psychologistId}/availability/{availabilityId}")]
        [ProducesResponseType(typeof(Availability), StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Availability>> UpdateAvailability([FromRoute] long psychologistId, [FromRoute] long availabilityId, [FromBody] Availability availability)
        {
            var updateAvailabilityCommand = new UpdateAvailabilityCommand(psychologistId, availabilityId, availability.From, availability.To);
            
            await _updateAvailabilityCommandHandler.HandleAsync(updateAvailabilityCommand);

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
