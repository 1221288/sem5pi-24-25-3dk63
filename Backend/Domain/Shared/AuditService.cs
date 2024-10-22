using System;
using DDDSample1.Domain;
using Serilog;

namespace Backend.Domain.Shared
{
    public class AuditService
    {
        private readonly Serilog.ILogger _logger;


        public AuditService(Serilog.ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public void LogDeletionPatient(Patient patient, string adminEmail)
        {
            string logMessage = $"Patient {patient.Id} was deleted by Admin ({adminEmail}) on {DateTime.UtcNow}";

            _logger.Information(logMessage);
        }

        
    }
}
