using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace LabSystems.Domain.Models
{
    public class DiskDrive : DomainObject
    {
        public string ModelNumber{ get; set; }
        public string SerialNumber { get; set; }
        public string Firmware { get; set; }
        public DateTime? Timestamp { get; set; }
        public string DriverVersion { get; set; }
        public string DriverProviderName { get; set; }
        public Guid LabSystemId { get; set; }
    }
}
