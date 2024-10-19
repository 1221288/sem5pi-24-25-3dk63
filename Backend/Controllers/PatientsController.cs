using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;
using DDDSample1.Patients;

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
        public async Task<ActionResult<PatientDTO>> GetById(Guid id)
        {
            var patient = await _service.GetByIdAsync(new MedicalRecordNumber(id));

            if (patient == null)
            {
                return NotFound();
            }

            return patient;
        }

        [HttpPost("register-patient")]
        public async Task RegisterPatient(RegisterPatientDTO dto)
        {
            await _service.RegisterPatientAsync(dto);
        }
    }
}