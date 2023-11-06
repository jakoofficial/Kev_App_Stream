using Kev_App_Stream.Models;

namespace Kev_App_Stream.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel() 
        {
            AppManager.SetManager(new AppManager());
            SetContentArea.Navigate(new SetUpScreenViewModel());
        }
    }
}