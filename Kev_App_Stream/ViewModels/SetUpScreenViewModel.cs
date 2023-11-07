using Avalonia.Controls;
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

        int ruleAddAmount = 1;
        public int RuleAddAmount { get => ruleAddAmount; set => this.RaiseAndSetIfChanged(ref ruleAddAmount, value); }

        public SetUpScreenViewModel()
        {
            PlayerList = AppManager.Instance.PlayerNameList;
            RuleList = AppManager.Instance.RuleList;

            DropPlayerCommand = ReactiveCommand.Create<bool>(DropPlayers);
            DropRulesCommand = ReactiveCommand.Create<bool>(DropRules);
            SaveListCommand = ReactiveCommand.Create<string>(SaveList);
            LoadListCommand = ReactiveCommand.Create<string>(LoadList);
        }

        string newPlayerName;
        string newRule;
        public string NewPlayerName { get => newPlayerName; set => this.RaiseAndSetIfChanged(ref newPlayerName, value); }
        public string NewRule { get => newRule; set => this.RaiseAndSetIfChanged(ref newRule, value); }

        string selectedPlayer;
        string selectedRule;
        public string SelectedPlayer { get => selectedPlayer; set => this.RaiseAndSetIfChanged(ref selectedPlayer, value); }
        public string SelectedRule { get => selectedRule; set => this.RaiseAndSetIfChanged(ref selectedRule, value); }

        public void AddPlayer()
        {
            if (!String.IsNullOrEmpty(NewPlayerName))
            {
                addItem(NewPlayerName, PlayerList, AppManager.Instance.PlayerNameList);
                NewPlayerName = "";
            }

        }
        public void AddRule()
        {
            if (!String.IsNullOrEmpty(NewRule))
            {
                for (int i = 0; i < RuleAddAmount; i++)
                {
                    addItem(NewRule, RuleList, AppManager.Instance.RuleList, true);
                }
            }
        }

        private void addItem(string newItem, List<string> setUpList, List<string> appManagerList, bool isRule = false)
        {
            if (setUpList.Count != 0)
            {
                foreach (var item in setUpList)
                {
                    if (!isRule)
                        if (item == newItem) return;
                }
            }

            appManagerList.Add(newItem);
            SetContentArea.Navigate(this);
        }

        public ReactiveCommand<bool, Unit> DropPlayerCommand { get; }
        public void DropPlayers(bool allPlayers)
        {
            if (allPlayers) { AppManager.Instance.PlayerNameList.Clear(); }
            else if (!string.IsNullOrEmpty(SelectedPlayer))
            {
                foreach (var item in AppManager.Instance.PlayerNameList)
                {
                    if (item == SelectedPlayer) { AppManager.Instance.PlayerNameList.Remove(item); break; }
                }
            }

            SetContentArea.Navigate(this);
        }
        public ReactiveCommand<bool, Unit> DropRulesCommand { get; }
        public void DropRules(bool allRules)
        {
            if (allRules) { AppManager.Instance.RuleList.Clear(); }
            else if (!string.IsNullOrEmpty(SelectedRule))
            {
                foreach (var item in AppManager.Instance.RuleList)
                {
                    if (item == SelectedRule) { AppManager.Instance.RuleList.Remove(item); break; }
                }
            }

            SetContentArea.Navigate(this);
        }

        public void Start()
        {
            if (PlayerList.Count == 0 || RuleList.Count == 0) return;

            SetContentArea.Navigate(new AppMainScreenViewModel());
        }

        public ReactiveCommand<string, Unit> SaveListCommand { get; }
        public void SaveList(string playerOrRule)
        {
            //if (PlayerList.Count == 0 || RuleList.Count == 0) return;
            switch (playerOrRule)
            {
                case "player": if (PlayerList.Count == 0) return; break;
                case "rule": if (RuleList.Count == 0) return; break;
                default: return;
            }

            playerOrRule = playerOrRule.ToLower();
            if (playerOrRule == "player") { FileSystem.SaveList("PlayerList", AppManager.Instance.PlayerNameList); /*Save Player List*/ }
            else if (playerOrRule == "rule") { FileSystem.SaveList("RuleList", AppManager.Instance.RuleList); /* Save Rule List */ }
        }

        public ReactiveCommand<string, Unit> LoadListCommand { get; }
        public void LoadList(string playerOrRule)
        {
            playerOrRule = playerOrRule.ToLower();
            if (playerOrRule == "player") { FileSystem.LoadList("PlayerList", AppManager.Instance.PlayerNameList); /*Save Player List*/ }
            else if (playerOrRule == "rule") { FileSystem.LoadList("RuleList", AppManager.Instance.RuleList); /* Save Rule List */ }

        }
    }
}
