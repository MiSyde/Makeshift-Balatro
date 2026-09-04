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
        private RelayCommand SelectSmallBindCommand;
        public SelectionPage()
        {
            InitializeComponent();
            SelectSmallBindCommand = new RelayCommand(SelectBind, CanSelectSmallBind);
        }

        private string XDashY(int X, int Y) => Helper.XDashY(X, Y);

        private void SelectBind() => Game.NextRound();

        private bool CanSelectSmallBind() => Game.Round % 4 == 1;
    }
}
