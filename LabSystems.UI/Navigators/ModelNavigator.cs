using IntelMAS.UI.Helpers;
using LabSystems.Domain.Context;
using LabSystems.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace LabSystems.UI.Navigators
{
    public interface IModelNavigator : INotifyPropertyChanged
    {
        string SelectedModelType { get; set; }
    }

    public class ModelNavigator : NotifyBase, IModelNavigator
    {
        public ModelNavigator(LabSystemsContextFactory contextFactory)
        {
            // Not great, I know. Think of a cleaner way...
            string fullType = contextFactory.CreateDbContext().LabSystems.EntityType.Name;
            SelectedModelType = fullType.Substring(fullType.LastIndexOf('.') + 1);
        }

        private string selectedModeType;
        public string SelectedModelType
    {
            get => selectedModeType;
            set => SetProperty(ref selectedModeType, value);
        }
    }
}
