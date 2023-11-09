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
        ObservableCollection<string> playersList;
        public ObservableCollection<string> PlayersList { get => playersList; set => this.RaiseAndSetIfChanged(ref playersList, value); }
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

            PlayersList = AddPlayers();

            RuleList = RuleAmountCorrection();
        }

        public ObservableCollection<string> AddPlayers()
        {
            ObservableCollection<string> list = new();

            if (AppManager.Instance.PlayerPickedAmount.Count > 0)
            {
                list = AppManager.Instance.PlayerPickedAmount;
            }

            if (list.Count != AppManager.Instance.PlayerNameList.Count)
            {
                AppManager.Instance.PlayerPickedAmount.Clear();
                list.Clear();
                foreach (var item in AppManager.Instance.PlayerNameList)
                {
                    list.Add("x0 : "+ item);
                }
            }

            return list;
        }

        public List<string> RuleAmountCorrection()
        {
            List<string> newRuleList = new List<string>();
            List<string> tempRuleList = new List<string>();

            foreach (var item in AppManager.Instance.RuleList)
            {
                int amount = AppManager.Instance.RuleList.Where(x => x.Equals(item)).Count();
                tempRuleList.Add("x" + amount + " : "+item);
            }

            for (int i = 0; i < tempRuleList.Count; i++)
            {
                string rule = tempRuleList[i];
                if (!newRuleList.Contains(rule)) newRuleList.Add(rule);
            }

            return newRuleList;
        }

        Random rng = new Random();
        public void Roll()
        {
            int rName = rng.Next(AppManager.Instance.PlayerNameList.Count);
            int rRule = rng.Next(AppManager.Instance.RuleList.Count);

            PlayerName = AppManager.Instance.PlayerNameList[rName];
            Rule = AppManager.Instance.RuleList[rRule];

            var playerCalled = PlayersList[rName].Split(':');

            ObservableCollection<string> names = new();

            foreach (var item in PlayersList)
            {
                names.Add(item);
            }

            PlayersList.Clear();

            for (int i = 0; i < AppManager.Instance.PlayerNameList.Count; i++)
            {
                if (AppManager.Instance.PlayerNameList[i] == playerCalled[1].Trim())
                {
                    int amount = Convert.ToInt32(playerCalled[0].Split('x')[1]);
                    PlayersList.Add($"x{++amount} : {PlayerName}");
                }
                else { PlayersList.Add(names[i]); }
            }

            string newHistoryAddition = String.Format("{2} | {0}\n{1}", PlayerName, Rule, DateTime.Now.ToString("HH:mm:ss"));
            History.Insert(0, newHistoryAddition);
        }

        public void ClearHistory()
        {
            if (History.Count == 0 || History == null) return;
            AppManager.Instance.History.Clear();
            History.Clear();
        }

        public void ClearAll()
        {
            ClearHistory();
            AppManager.Instance.PlayerPickedAmount.Clear();
            PlayersList.Clear();
            PlayersList = AddPlayers();
        }

        public void GoSetUp()
        {
            AppManager.Instance.History = History;
            AppManager.Instance.PlayerPickedAmount = PlayersList;
            SetContentArea.Navigate(new SetUpScreenViewModel());
        }
    }
}
