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
        public bool Update(SystemCategories.Category category);

    }
    public class UpdateSystemInfo : IUpdateSystemInfo
    {
        public UpdateSystemInfo()
        {
        }

        public bool Update(SystemCategories.Category category)
        {
            string hostName = GetHostName();

            LabSystem entity = new LabSystem()
            {
                Ipaddress = GetIPAddress(),
                HostName = hostName,
                Osversion = GetOSVersion(),
                Timestamp = GetTimestamp(),
                Category = category,
            };

            entity.DiskDrives?.Clear();
            entity.DiskDrives = (new UpdateDiskDriveInfo()).Update();

            ILabSystemService service = new LabSystemService(new LabSystemsContextFactory());

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
