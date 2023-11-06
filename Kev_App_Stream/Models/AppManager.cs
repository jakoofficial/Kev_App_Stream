using Kev_App_Stream.ViewModels;
using ReactiveUI;
using System;
using System.Collections.Generic;
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

        List<string> playerNameList = new List<string>()
        {
            "Koko"
        };
        List<string> ruleList = new List<string>()
        {
            "Slurp"
        };
        public List<string> PlayerNameList { get => playerNameList; set => this.RaiseAndSetIfChanged(ref playerNameList, value); }
        public List<string> RuleList { get => ruleList; set => this.RaiseAndSetIfChanged(ref ruleList, value); }
    }
}
