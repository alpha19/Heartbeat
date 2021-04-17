using LabSystems.UI.Navigators;

namespace LabSystems.UI
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel(ModelNavigator navigator) 
        {
            Navigator = navigator;
        }

        public IModelNavigator Navigator { get; set;}
    }
}
