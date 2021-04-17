using LabSystems.Domain.Extensions;
using System;
using System.Collections.Generic;

namespace LabSystems.Domain.Models
{
    public class LabSystem : DomainObject
    {
        public string HostName { get; set; }
        public string Ipaddress { get; set; }
        public string Osversion { get; set; }
        public DateTime? Timestamp { get; set; }
        public SystemCategories.Category Category { get; set; }
        public ICollection<DiskDrive> DiskDrives { get; set; } = new List<DiskDrive>();
    }
}
