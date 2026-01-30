using Microsoft.AspNetCore.Mvc;
using PatientAppointmentAPI.Core.DTOs.Appointments;
using PatientAppointmentAPI.Core.Interfaces;

namespace PatientAppointmentAPI.Controllers
{
    [ApiController]
    [Route("api/appointments")]
    [Produces("application/json")]
    public sealed class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _service;

        public AppointmentsController(IAppointmentService service) => _service = service;

        // GET api/appointments/{id}
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AppointmentDto>> GetById(Guid id, CancellationToken ct)
        {
            var dto = await _service.GetByIdAsync(id, ct);
            return Ok(dto);
        }

        // GET api/appointments/patient/{patientId}
        [HttpGet("patient/{patientId:guid}")]
        [ProducesResponseType(typeof(List<AppointmentDto>), StatusCodes.Status200OK)]
        public async Task<ActionResult<List<AppointmentDto>>> GetByPatient(Guid patientId, CancellationToken ct)
        {
            var dtos = await _service.GetByPatientIdAsync(patientId, ct);
            return Ok(dtos);
        }

        // GET api/appointments/search
        [HttpGet("search")]
        [ProducesResponseType(typeof(List<AppointmentDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<List<AppointmentDto>>> Search([FromBody] GetAppointmentsRequest query, CancellationToken ct)
        {
            var dtos = await _service.SearchAsync(query, ct);
            return Ok(dtos);
        }

        // POST api/appointments
        [HttpPost]
        [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppointmentDto>> Create([FromBody] CreateAppointmentRequest request, CancellationToken ct)
        {
            var created = await _service.CreateAsync(request, ct);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        // PUT api/appointments/{id}
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(AppointmentDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<AppointmentDto>> Update(Guid id, [FromBody] UpdateAppointmentRequest request, CancellationToken ct)
        {
            // Prefer route id as source of truth
            request.Id = id;

            var updated = await _service.UpdateAsync(request, ct);
            return Ok(updated);
        }

        // POST api/appointments/{id}/cancel
        [HttpPost("{id:guid}/cancel")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
        {
            await _service.CancelAsync(id, ct);
            return NoContent();
        }

        // POST api/appointments/{id}/markattended
        [HttpPost("{id:guid}/markattended")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarkAttended(Guid id, CancellationToken ct)
        {
            await _service.MarkAsAttended(id, ct);
            return NoContent();
        }

        // POST api/appointments/{id}/markmissed
        [HttpPost("{id:guid}/markmissed")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MarkMissedIfOverdue(Guid id, CancellationToken ct)
        {
            await _service.MarkMissedIfOverdue(id, ct);
            return NoContent();
        }
    }
}
