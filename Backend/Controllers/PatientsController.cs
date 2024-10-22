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
        //[Authorize(Roles="Admin")]
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
        //[Authorize(Roles="Admin")]
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

        [HttpPatch("update")]
        [Authorize(Roles = "Patient")]
        public async Task<IActionResult> UpdateOwnPatientProfile(UserProfileUpdateDTO updateDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;

            if (string.IsNullOrEmpty(userEmail))
            {
                return Unauthorized("Unable to find the patient information from Google IAM.");
            }

            var patient = await _service.GetPatientByUsername(new Username(userEmail));

            if (patient == null)
            {
                return Unauthorized("Patient not found.");
            }

            if (patient == null)
            {
                return Unauthorized("Patient information is incomplete.");
            }

            var userResult = await _service.GetUserByUserIdAsync(patient.UserId);

            updateDto.oldEmail = new Email(userResult.Email.ToString());

            PatientUpdateDTO patientUpdate = new PatientUpdateDTO
            {
                Id = patient.Id,
                Name = updateDto.Name,
                Email = updateDto.Email,
                personalEmail = updateDto.oldEmail,
                PhoneNumber = updateDto.PhoneNumber,
                emergencyContact = updateDto.emergencyContact,
                allergy = updateDto.allergy
            };

            var result = await _service.UpdatePatientProfileAsync(patientUpdate);

            if (result)
            {
                return Ok("Patient updated successfully");
            }

            return BadRequest("Patient not updated!");
        }

    }
}