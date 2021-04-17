using Prism.Ioc;
using Prism.Unity;
using System.Windows;
using Unity;
using LabSystems.Domain.Context;
using LabSystems.UI;
using LabSystems.UI.Navigators;

namespace LabSystems.POC
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    partial class App : PrismApplication
    {
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            var unityContainer = containerRegistry.GetContainer();

            // register as a singleton so DeviceList will only be created once
            unityContainer.RegisterType<LabSystemsContextFactory, LabSystemsContextFactory>(TypeLifetime.Singleton);
            unityContainer.RegisterType<IModelNavigator, ModelNavigator>(TypeLifetime.Singleton);
        }

        protected override Window CreateShell() => Container.Resolve<MainWindow>();
    }
}

