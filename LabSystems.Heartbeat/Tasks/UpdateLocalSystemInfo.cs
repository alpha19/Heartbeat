using LabSystems.Domain.Context;
using LabSystems.Domain.Extensions;
using LabSystems.Domain.Models;
using LabSystems.Domain.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace LabSystems.Heartbeat.Tasks
{
    public interface IUpdateSystemInfo
    {
        public Task<bool> Update(SystemCategories.Category category);

    }
    public class UpdateSystemInfo : IUpdateSystemInfo
    {
        public UpdateSystemInfo()
        {
        }

        public async Task<bool> Update(SystemCategories.Category category)
        {
            ILabSystemService service = new LabSystemService(new LabSystemsContextFactory());

            string hostName = GetHostName();

            LabSystem existingEntity = await service.Get(hostName);

            LabSystem entity = new LabSystem()
            {
                Ipaddress = GetIPAddress(),
                HostName = hostName,
                Osversion = GetOSVersion(),
                Timestamp = GetTimestamp(),
                Category = category,
            };

            if (existingEntity != null)
            {
                entity.Id = existingEntity.Id;

                if (category == SystemCategories.Category.Unknown)
                {
                    entity.Category = existingEntity.Category;
                }
            }

            entity.DiskDrives?.Clear();
            entity.DiskDrives = await (new UpdateDiskDriveInfo()).Update();

            Task<LabSystem> task = service.CreateOrUpdate(hostName, entity);

            task.Wait();
            return task.IsCompleted;
        }

        private string GetHostName()
        {
            return System.Environment.MachineName;
        }

        private string GetIPAddress()
        {
            IPHostEntry host = Dns.GetHostEntry(GetHostName());
            string IPAddress = "";

            Array.ForEach(host.AddressList, item =>
            {
                if (item.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    IPAddress = Convert.ToString(item);
                }
            });

            return IPAddress;
        }

        private string GetOSVersion()
        {
            return Environment.OSVersion.ToString();
        }

        private DateTime GetTimestamp()
        {
            return DateTime.Now;
        }
    }
}
