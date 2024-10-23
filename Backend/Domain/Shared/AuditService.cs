using System;
using DDDSample1.Domain;
using DDDSample1.Domain.Staff; // Ensure this is the correct namespace for the Staff class
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

        public void LogDeactivateOperationType(OperationType operationType, string adminEmail)
        {
            string logMessage = $"OperationType {operationType.Id.Value} was deactivated by Admin ({adminEmail}) on {DateTime.UtcNow}";

            _logger.Information(logMessage);
        }

        public void LogCreateOperationType(OperationType operationType, string adminEmail)
        {
            string logMessage = $"OperationType {operationType.Id.Value} was created by Admin ({adminEmail}) on {DateTime.UtcNow}";

            _logger.Information(logMessage);
        }

        public void LogDeactivateStaff(DDDSample1.Domain.Staff.Staff staff, string adminEmail)
        {
            string logMessage = $"Staff {staff.Id.Value} was deactivated by Admin ({adminEmail}) on {DateTime.UtcNow}";

            _logger.Information(logMessage);
        }
        
    }
}
