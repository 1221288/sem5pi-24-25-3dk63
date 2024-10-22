using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;
using DDDSample1.Patients;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace DDDSample1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {

        private readonly ILogger<PatientsController> _logger;
        private readonly PatientService _service;

        public PatientsController(ILogger<PatientsController> logger, PatientService service)
        {
            _logger = logger;
            _service = service;
        }

        // GET: api/Patients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientDTO>> GetById(MedicalRecordNumber id)
        {
            var patient = await _service.GetByIdAsync(id);

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        [HttpPost("register-patient")]
        public async Task<ActionResult<PatientDTO>> RegisterPatient(RegisterPatientDTO dto)
        {
            var patient = await _service.RegisterPatientAsync(dto);
            if (patient == null)
            {
                return Problem("Error: patient is null");
            }
            return CreatedAtAction(nameof(GetById), new { id = patient.Id }, patient);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdatePatientProfile(PatientUpdateDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _service.UpdatePatientProfileAsync(updateDto);

            if (result)
            {
                return Ok("Patient updated successfully");
            }

            return BadRequest("Patient not updated!");
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeletePatientProfile(string id)
        {
            var adminEmail = User.FindFirstValue(ClaimTypes.Email);
            _logger.LogInformation($"Tentando deletar o paciente com ID: {id}");

            var medicalRecordNumber = new MedicalRecordNumber(id);
            _logger.LogInformation($"Tentando deletar o paciente com medical record number ID: {medicalRecordNumber}");

            var result = await _service.DeletePatientAsync(medicalRecordNumber, adminEmail);

            if (result == null)
            {
                return NotFound("Patient not found.");
            }

            return NoContent();
        }



    }
}