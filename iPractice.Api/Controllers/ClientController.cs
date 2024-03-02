using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using iPractice.Api.Models;
using iPractice.Application.Commands;
using iPractice.Application.Interfaces;
using iPractice.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace iPractice.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly ICommandHandler<CreateAppointmentCommand> _createAppointmentCommandHandler;
        private readonly IQueryHandler<GetAvailablePsychologistsQuery, IEnumerable<Domain.Entities.Psychologist>> _getAvailableTimeSlotsQueryHandler;

        private readonly ILogger<ClientController> _logger;
        
        public ClientController(
            ICommandHandler<CreateAppointmentCommand> createAppointmentCommandHandler,
            IQueryHandler<GetAvailablePsychologistsQuery, IEnumerable<Domain.Entities.Psychologist>> getAvailableTimeSlotsQueryHandler, 
            ILogger<ClientController> logger)
        {
            _createAppointmentCommandHandler = createAppointmentCommandHandler;
            _getAvailableTimeSlotsQueryHandler = getAvailableTimeSlotsQueryHandler;
            _logger = logger;
        }
        
        /// <summary>
        /// The client can see when his psychologists are available.
        /// Get available slots from his two psychologists.
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <returns>All time slots for the selected client</returns>
        [HttpGet("{clientId}/timeslots")]
        [ProducesResponseType(typeof(IEnumerable<TimeSlot>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IEnumerable<TimeSlot>>> GetAvailableTimeSlots(long clientId)
        {
            try
            {
                var getAvailableSlotsQuery = new GetAvailablePsychologistsQuery(clientId);
                
                var psychologistAvailabilities = await _getAvailableTimeSlotsQueryHandler.HandleAsync(getAvailableSlotsQuery);

                // TODO: move to factory
                var timeSlots = TimeSlot.FromEntity(psychologistAvailabilities);

                return Ok(timeSlots);
            }
            //@TODO: map to custom error response
            catch (NullReferenceException e) 
            {
                _logger.LogError(e, e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Unhandled exception occurred while processing the request.");
                return StatusCode((int)HttpStatusCode.InternalServerError, "Error occurred while processing the request.");
            }
        }

        /// <summary>
        /// Create an appointment for a given availability slot
        /// </summary>
        /// <param name="clientId">The client ID</param>
        /// <param name="appointment">Identifies the psychologist and availability slot</param>
        /// <returns>Ok if appointment was made</returns>
        [HttpPost("{clientId}/appointment")]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult> CreateAppointment(long clientId, [FromBody] Appointment appointment)
        {
            try
            {
                var createAppointmentCommand = new CreateAppointmentCommand(clientId, appointment.TimeSlotId);
                
                await _createAppointmentCommandHandler.HandleAsync(createAppointmentCommand);

                // TODO: move to factory
                // var timeSlots = TimeSlot.FromEntity(psychologistAvailabilities);

                return Ok();
            }
            //@TODO: map to custom error response
            catch (NullReferenceException e) 
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
