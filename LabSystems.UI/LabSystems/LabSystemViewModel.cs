using IntelMAS.UI.Helpers;
using LabSystems.Domain.Context;
using LabSystems.Domain.Models;
using LabSystems.Domain.Services;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace LabSystems.UI.LabSystems
{
    public interface ILabSystemCollection : ICollection<LabSystem>, INotifyCollectionChanged
    {
    }

    public class LabSystemCollection : NotifyBaseCollection<LabSystem>, ILabSystemCollection
    {
    }

    public class LabSystemViewModel : NotifyBase
    {
        private readonly LabSystemService _service;

        public LabSystemViewModel(LabSystemsContextFactory contextFactory)
        {
            _service = new LabSystemService(contextFactory);

            RefreshCommand = new AsyncCommand(async () =>
            {
                Systems.Clear();
                IsNotScanning = false;

                Task<IEnumerable<LabSystem>> task = _service.GetAll();

                await task;

                foreach (LabSystem system in task.Result)
                {
                    Systems.Add(system);
                }

                IsNotScanning = true;
            });
        }

        private bool isNotScanning = true;
        public bool IsNotScanning
        {
            get => isNotScanning;
            set => SetProperty(ref isNotScanning, value);
        }

        public ILabSystemCollection Systems { get; set; } = new LabSystemCollection();

        public IAsyncCommand RefreshCommand { get; }
    }
}
