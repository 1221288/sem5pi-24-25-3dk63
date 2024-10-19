using Google.Apis.Auth;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DDDSample1.Domain.Patients;
using DDDSample1.Domain.Users;
using DDDSample1.Patients;
using DDDSample1.Domain;

namespace DDDSample1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegistrationsController : ControllerBase
    {
        private readonly EmailService _emailService;
        private readonly RegistrationService _registrationService;

        public RegistrationsController(EmailService emailService, RegistrationService registrationService)
        {
            _emailService = emailService;
            _registrationService = registrationService;
        }

        [HttpPost("self-register")]
        public async Task<IActionResult> SelfRegister(SelfRegisterPatientDTO dto)
        {
            try
            {
                await _registrationService.SelfRegisterAsync(dto);
                return Ok("Registration initiated. Please check your email for confirmation.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string token)
        {
            try
            {
                await _registrationService.ConfirmEmailAsync(token);
                return Ok("Email confirmed successfully. Your registration is now complete.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Email confirmation failed: {ex.Message}");
            }
        }
    }
}