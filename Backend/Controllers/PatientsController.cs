using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;
using DDDSample1.Patients;
using Microsoft.AspNetCore.Authorization;

namespace DDDSample1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PatientsController : ControllerBase
    {
        private readonly PatientService _service;

        public PatientsController(PatientService service)
        {
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
        [Authorize (Roles = "Admin")]
        public async Task<ActionResult<PatientDTO>> RegisterPatient(RegisterPatientDTO dto)
        {
            var patient = await _service.RegisterPatientAsync(dto);
            if (patient.Value == null)
            {
                return Problem("Error: patient is null");
            }
            return CreatedAtAction(nameof(GetById), new { id = patient.Value.Id }, patient);
        }
    }
}