using Balatro.Enums;
using Balatro.Util;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI;
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
using WinRT.Interop;

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
        RunInfoWindow? runInfoWindow;
        OptionsWindow? optionsWindow;
        IntPtr mainHwnd;
        private const int GWLP_HWNDPARENT = -8;
        public SelectionPage()
        {
            InitializeComponent();

            NavigationCacheMode = NavigationCacheMode.Required;

            SelectSmallBlindCommand = new RelayCommand(SelectBlind, CanSelectSmallBlind);
            SelectBigBlindCommand = new RelayCommand(SelectBlind, CanSelectBigBlind);
            SelectBossBlindCommand = new RelayCommand(SelectBlind, CanSelectBossBlind);

            Game.AssignBlindCommands(SelectSmallBlindCommand, SelectBigBlindCommand, SelectBossBlindCommand);
        }

        private string XDashY(int X, int Y) => Helper.XDashY(X, Y);

        private void SelectBlind()
        {
            Game.NextRound();
            App.MainFrame.Navigate(typeof(Balatro_Page));
        }

        private bool CanSelectSmallBlind() => Game.Round % 4 == 1;

        private bool CanSelectBigBlind() => Game.Round % 4 == 2;

        private bool CanSelectBossBlind() => Game.Round % 4 == 3;

        private void Show_RunInfo(object sender, RoutedEventArgs e)
        {
            var windowId = XamlRoot.ContentIslandEnvironment.AppWindowId;
            mainHwnd = Win32Interop.GetWindowFromWindowId(windowId);

            runInfoWindow = new RunInfoWindow(mainHwnd);
            runInfoWindow.Closed += RunInfo_Closed;

            var runInfoHwnd = WindowNative.GetWindowHandle(runInfoWindow);

            NativeMethods.SetWindowLongPtr(runInfoHwnd, GWLP_HWNDPARENT, mainHwnd);

            runInfoWindow.Activate();
        }

        private void Show_Options(object sender, RoutedEventArgs e)
        {
            var windowId = XamlRoot.ContentIslandEnvironment.AppWindowId;
            mainHwnd = Win32Interop.GetWindowFromWindowId(windowId);

            optionsWindow = new OptionsWindow(mainHwnd);
            optionsWindow.Closed += Options_Closed;

            var runInfoHwnd = WindowNative.GetWindowHandle(runInfoWindow);

            NativeMethods.SetWindowLongPtr(runInfoHwnd, GWLP_HWNDPARENT, mainHwnd);

            optionsWindow.Activate();
        }

        private void RunInfo_Closed(object sender, WindowEventArgs args)
        {
            runInfoWindow = null;
        }

        private void Options_Closed(object sender, WindowEventArgs args)
        {
            runInfoWindow = null;
        }
    }
}
