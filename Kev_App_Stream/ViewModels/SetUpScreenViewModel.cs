using Kev_App_Stream.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Kev_App_Stream.ViewModels
{

    public class SetUpScreenViewModel : ViewModelBase
    {
        List<string> playerList;
        public List<string> PlayerList { get => playerList; set => this.RaiseAndSetIfChanged(ref playerList, value); }

        List<string> ruleList;
        public List<string> RuleList { get => ruleList; set => this.RaiseAndSetIfChanged(ref ruleList, value); }

        public SetUpScreenViewModel()
        {
            PlayerList = AppManager.Instance.PlayerNameList;
            RuleList = AppManager.Instance.RuleList;
        }

        string newPlayerName;
        string newRule;
        public string NewPlayerName { get => newPlayerName; set => this.RaiseAndSetIfChanged(ref newPlayerName, value); }
        public string NewRule { get => newRule; set => this.RaiseAndSetIfChanged(ref newRule, value); }


        public void AddPlayer()
        {
            if(!String.IsNullOrEmpty(NewPlayerName))
                addItem(NewPlayerName, PlayerList, AppManager.Instance.PlayerNameList);
        }
        public void AddRule()
        {
            if(!String.IsNullOrEmpty(NewRule))
                addItem(NewRule, RuleList, AppManager.Instance.RuleList);
        }

        private void addItem(string newItem, List<string> setUpList, List<string> appManagerList)
        {
            if (setUpList.Count != 0)
            {
                foreach (var item in setUpList)
                {
                    if (item == newItem) return;
                }
            }

            appManagerList.Add(newItem);
            SetContentArea.Navigate(this);
        }
    }
}
