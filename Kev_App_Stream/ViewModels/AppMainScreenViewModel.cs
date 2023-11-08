using Kev_App_Stream.Models;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kev_App_Stream.ViewModels
{
    public class AppMainScreenViewModel : ViewModelBase
    {
        public List<string> PlayersList { get; set; } = AppManager.Instance.PlayerNameList;
        public List<string> RuleList { get; set; }

        ObservableCollection<string> history = new ObservableCollection<string>();
        public ObservableCollection<string> History { get => history; set => this.RaiseAndSetIfChanged(ref history, value); }

        string playerName;
        string rule;
        public string PlayerName { get => playerName; set => this.RaiseAndSetIfChanged(ref playerName, value); }
        public string Rule { get => rule; set => this.RaiseAndSetIfChanged(ref rule, value); }

        public AppMainScreenViewModel()
        {
            History.Clear();
            History = AppManager.Instance.History;

            RuleList = RuleAmountCorrection();
        }

        public List<string> RuleAmountCorrection()
        {
            List<string> newRuleList = new List<string>();
            List<string> tempRuleList = new List<string>();

            foreach (var item in AppManager.Instance.RuleList)
            {
                int amount = AppManager.Instance.RuleList.Where(x => x.Equals(item)).Count();
                tempRuleList.Add(item + " x" + amount);
            }

            for (int i = 0; i < tempRuleList.Count; i++)
            {
                string rule = tempRuleList[i];
                if(!newRuleList.Contains(rule)) newRuleList.Add(rule);
            }

            return newRuleList;
        }

        Random rng = new Random();
        public void Roll()
        {
            int rName = rng.Next(PlayersList.Count);
            int rRule = rng.Next(AppManager.Instance.RuleList.Count);

            PlayerName = PlayersList[rName];
            Rule = AppManager.Instance.RuleList[rRule];

            string newHistoryAddition = String.Format("{2} | {0}\n{1}", PlayerName, Rule, DateTime.Now.ToString("HH:mm:ss"));
            History.Insert(0, newHistoryAddition);
        }

        public void ClearHistory()
        {
            if (History.Count == 0 || History == null) return;
            AppManager.Instance.History.Clear();
            History.Clear();
        }

        public void GoSetUp()
        {
            AppManager.Instance.History = History;
            SetContentArea.Navigate(new SetUpScreenViewModel());
        }
    }
}
