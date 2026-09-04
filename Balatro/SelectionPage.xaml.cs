using Balatro.Enums;
using Balatro.Util;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Balatro
{
    /// <summary>
    /// The page where the player selects blinds and is shows between blinds
    /// </summary>
    public sealed partial class SelectionPage : Page
    {
        BalatroGame Game => App.CurrentGame;
        private RelayCommand SelectSmallBlindCommand;
        private RelayCommand SelectBigBlindCommand;
        private RelayCommand SelectBossBlindCommand;
        public SelectionPage()
        {
            InitializeComponent();

            SelectSmallBlindCommand = new RelayCommand(SelectBlind, CanSelectSmallBlind);
            SelectBigBlindCommand = new RelayCommand(SelectBlind, CanSelectBigBlind);
            SelectBossBlindCommand = new RelayCommand(SelectBlind, CanSelectBossBlind);

            Game.AssignBlindCommands(SelectSmallBlindCommand, SelectBigBlindCommand, SelectBossBlindCommand);
        }

        private string XDashY(int X, int Y) => Helper.XDashY(X, Y);

        private void SelectBlind() => Game.NextRound();

        private bool CanSelectSmallBlind() => Game.Round % 4 == 1;

        private bool CanSelectBigBlind() => Game.Round % 4 == 2;

        private bool CanSelectBossBlind() => Game.Round % 4 == 3;

        private string GetBossDescription(BossBlind BossBlind) => Helper.GetBossDescription(BossBlind);
        private string GetBossName(BossBlind BossBlind) => Helper.GetDescription(BossBlind);
        private string GetBossImage(BossBlind BossBlind) => Helper.GetBossDescription(BossBlind);
    }
}
