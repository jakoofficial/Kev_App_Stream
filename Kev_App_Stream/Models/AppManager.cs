using Kev_App_Stream.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kev_App_Stream.Models
{
    public class AppManager : ViewModelBase
    {
        public static AppManager Instance;
        public static void SetManager(AppManager am)
        {
            Instance = am;
        }

        ObservableCollection<string> history = new ObservableCollection<string>();
        public ObservableCollection<string> History { get => history; set => this.RaiseAndSetIfChanged(ref history, value); }

        List<string> playerNameList = new List<string>()
        {
        };
        List<string> ruleList = new List<string>()
        {
        };
        public List<string> PlayerNameList { get => playerNameList; set => this.RaiseAndSetIfChanged(ref playerNameList, value); }
        public List<string> RuleList { get => ruleList; set => this.RaiseAndSetIfChanged(ref ruleList, value); }
    }
}
