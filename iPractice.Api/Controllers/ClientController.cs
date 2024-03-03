using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using iPractice.Api.Models;
using iPractice.Application.Commands;
using iPractice.Application.Interfaces;
using iPractice.Application.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace iPractice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ICommandHandler<MakeAppointmentCommand> _createAppointmentCommandHandler;
        private readonly IQueryHandler<GetAvailablePsychologistsQuery, IEnumerable<Domain.Entities.Psychologist>> _getAvailableTimeSlotsQueryHandler;

        public ClientController(
            ICommandHandler<MakeAppointmentCommand> createAppointmentCommandHandler,
            IQueryHandler<GetAvailablePsychologistsQuery, IEnumerable<Domain.Entities.Psychologist>> getAvailableTimeSlotsQueryHandler)
        {
            _createAppointmentCommandHandler = createAppointmentCommandHandler;
            _getAvailableTimeSlotsQueryHandler = getAvailableTimeSlotsQueryHandler;
        }
        
        /// <summary>
        /// The client can see when his psychologists are available.
        /// Get available slots from his two psychologists.
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <returns>All time slots for the selected client</returns>
        [HttpGet("{clientId}/timeslots")]
        [ProducesResponseType(typeof(IEnumerable<TimeSlot>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetAvailableTimeSlots(long clientId)
        {
            var getAvailableSlotsQuery = new GetAvailablePsychologistsQuery(clientId);
            
            var psychologists = await _getAvailableTimeSlotsQueryHandler.HandleAsync(getAvailableSlotsQuery);

            var timeSlots = psychologists.Select(Psychologist.FromEntity);
            
            return Ok(timeSlots);
        }

        /// <summary>
        /// Create an appointment for a given availability slot
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <param name="appointment">Identifies the availability slot</param>
        /// <returns>Ok if appointment was made</returns>
        [HttpPost("{clientId}/appointment")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateAppointment(long clientId, [FromBody] Appointment appointment)
        {
            var createAppointmentCommand = new MakeAppointmentCommand(clientId, appointment.TimeSlotId);
            
            await _createAppointmentCommandHandler.HandleAsync(createAppointmentCommand);

            return StatusCode(StatusCodes.Status201Created, true);
        }
    }
}
