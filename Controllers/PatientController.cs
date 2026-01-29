using Microsoft.AspNetCore.Mvc;
using PatientAppointmentAPI.Core.DTOs.Patients;
using PatientAppointmentAPI.Core.Interfaces;

namespace PatientAppointmentAPI.Controllers
{
    [ApiController]
    [Route("api/patients")]
    [Produces("application/json")]
    public sealed class PatientsController : ControllerBase
    {
        private readonly IPatientService _service;

        public PatientsController(IPatientService service)
            => _service = service;

        // GET api/patients/{id}
        [HttpGet("{id:guid}")]
        [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientDto>> GetById(Guid id, CancellationToken ct)
        {
            var dto = await _service.GetPatientByIdAsync(id, ct);
            return Ok(dto);
        }

        // GET api/patients/{id}/appointments
        [HttpGet("{id:guid}/appointments")]
        [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<PatientDto>> GetByIdWithAppointments(Guid id, CancellationToken ct)
        {
            var dto = await _service.GetPatientByIdwithApptsAsync(id, ct);
            return Ok(dto);
        }

        // POST api/patients
        [HttpPost]
        [ProducesResponseType(typeof(PatientDto), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<PatientDto>> Create(
            [FromBody] CreatePatientRequest request,
            CancellationToken ct)
        {
            var created = await _service.CreatePatientAsync(request, ct);

            return CreatedAtAction(
                nameof(GetById),
                new { id = created.Id },
                created);
        }

        // PUT api/patients/{id}
        [HttpPut("{id:guid}")]
        [ProducesResponseType(typeof(PatientDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<PatientDto>> Update(
            Guid id,
            [FromBody] UpdatePatientRequest request,
            CancellationToken ct)
        {
            // Route id is the source of truth
            request.Id = id;

            var updated = await _service.UpdatePatientAsync(request, ct);
            return Ok(updated);
        }

        // DELETE api/patients/{id}
        [HttpDelete("{id:guid}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        {
            await _service.DeletePatientAsync(id, ct);
            return NoContent();
        }
    }
}
