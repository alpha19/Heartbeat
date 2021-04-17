using LabSystems.Domain.Models;
using System.Collections.Generic;
using System.Management;
using System.IO;

namespace LabSystems.Heartbeat.Tasks
{
    public class UpdateDiskDriveInfo
    {
        private readonly ManagementClass _driveClass;
        public UpdateDiskDriveInfo()
        {
            // Will need to ensure this is corrs-platform. Quick and dirty for now..
            _driveClass = new ManagementClass("Win32_DiskDrive");
        }

        public ICollection<DiskDrive> Update()
        {        
            ICollection<DiskDrive> disks = new List<DiskDrive>();

            ManagementObjectCollection drives = _driveClass.GetInstances();

            string firmware = "FirmwareRevision";
            string model = "Model";
            string serial = "SerialNumber";
            string PNPDeviceId = "PNPDeviceID";

            foreach (ManagementObject drive in drives)
            {
                DiskDrive driveModel = new DiskDrive();

                foreach (PropertyData property in drive.Properties)
                {
                    if (property.Name == firmware)
                    {
                        driveModel.Firmware = property.Value.ToString();
                        continue;
                    }
                    if (property.Name == model)
                    {
                        driveModel.ModelNumber = property.Value.ToString();
                        continue;
                    }
                    if (property.Name == serial)
                    {
                        driveModel.SerialNumber = property.Value.ToString();
                        continue;
                    }
                    if(property.Name == PNPDeviceId)
                    {
                        AddDriverInfo(driveModel, property.Value.ToString());
                        continue;
                    }

                }

                disks.Add(driveModel);
            }

            return disks;
        }

        private void AddDriverInfo(DiskDrive driveModel, string pnpString)
        {
            // Not great :( ......... Takes 11 seconds to run.....
            string DriverProviderName = "DriverProviderName";
            string DriverVersion = "DriverVersion";
            string DeviceId = "DeviceID";

            SelectQuery query = new SelectQuery($"SELECT {DeviceId},{DriverProviderName},{DriverVersion} FROM Win32_PnPSignedDriver WHERE DeviceClass = 'DISKDRIVE'");
            ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);

            foreach (ManagementObject mo in searcher.Get())
            {
                bool matched = false;
                foreach (PropertyData property in mo.Properties)
                {
                    if(property.Name == DeviceId && property.Value.ToString() == pnpString)
                    {
                        matched = true;
                        break;
                    }
                }

                if (matched == true)
                {
                    foreach (PropertyData property in mo.Properties)
                    {
                        if (property.Name == DriverProviderName)
                        {
                            driveModel.DriverProviderName = property.Value.ToString();
                            continue;
                        }
                        if (property.Name == DriverVersion)
                        {
                            driveModel.DriverVersion = property.Value.ToString();
                            continue;
                        }
                    }
                }
            }
        }
    }
}
