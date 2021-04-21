using LabSystems.Domain.Context;
using LabSystems.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace LabSystems.Domain.Services
{
    public interface IDiskDriveService : IDataService<DiskDrive>
    {
        Task<DiskDrive> Get(string serialNumber);
    }
    public class DiskDriveService : GenericDataService<DiskDrive>, IDiskDriveService
    {
        public DiskDriveService(LabSystemsContextFactory contextFactory) : base(contextFactory)
        {
        }
        public async Task<DiskDrive> Get(string serialNumber)
        {
            using (LabSystemsContext context = _contextFactory.CreateDbContext())
            {
                DiskDrive entity = await context.Set<DiskDrive>().FirstOrDefaultAsync((e) => e.SerialNumber == serialNumber);
                return entity;
            }
        }
    }
}
