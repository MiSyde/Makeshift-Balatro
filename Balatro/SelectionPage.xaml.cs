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
            SizeChanged += SelectionPage_SizeChanged;

            NavigationCacheMode = NavigationCacheMode.Required;

            SelectSmallBlindCommand = new RelayCommand(SelectBlind, CanSelectSmallBlind);
            SelectBigBlindCommand = new RelayCommand(SelectBlind, CanSelectBigBlind);
            SelectBossBlindCommand = new RelayCommand(SelectBlind, CanSelectBossBlind);

            Game.AssignBlindCommands(SelectSmallBlindCommand, SelectBigBlindCommand, SelectBossBlindCommand);
        }

        private void SelectionPage_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetDimensions();
        }
        private void SetDimensions()
        {
            double SmallBlindWidthMult = 0, BigBlindWidthMult = 0, BossBlindWidthMult = 0, SmallBlindHeightMult = 0, BigBlindHeightMult = 0, BossBlindHeightMult = 0;

            GetBlindDimMults(ref SmallBlindWidthMult, ref BigBlindWidthMult, ref BossBlindWidthMult, ref SmallBlindHeightMult, ref BigBlindHeightMult, ref BossBlindHeightMult);

            CurrentSelectionInfoBorder.Height = this.ActualHeight * 0.1944;

            ChipsBorder.Height = this.ActualHeight * 0.082;

            MultiplierBorder.Height = this.ActualHeight * 0.082;

            ScoreBorder.Height = this.ActualHeight * 0.07;

            SmallBlindBorder.Height = this.ActualHeight * SmallBlindHeightMult;
            SmallBlindBorder.Width = this.ActualWidth * SmallBlindWidthMult;
            SmallBlindBorder.Margin = new Thickness(this.ActualWidth * 0.02, 0, 0, 0);

            BigBlindBorder.Height = this.ActualWidth * BigBlindHeightMult;
            BigBlindBorder.Width = this.ActualHeight * BigBlindWidthMult;
            BigBlindBorder.Margin = new Thickness(this.ActualWidth * 0.02, 0, this.ActualWidth * 0.02, 0);

            BossBlindBorder.Height = this.ActualHeight * BossBlindHeightMult;
            BossBlindBorder.Width = this.ActualWidth * BossBlindWidthMult; 
            BossBlindBorder.Margin = new Thickness(0, 0, this.ActualWidth * 0.02, 0);

            DeckAndTagGrid.Width = this.ActualWidth * 0.1665;
        }

        private void GetBlindDimMults(ref double SmallBlindWidthMult, ref double BigBlindWidthMult, ref double BossBlindWidthMult, 
            ref double SmallBlindHeightMult, ref double BigBlindHeightMult, ref double BossBlindHeightMult)
        {
            switch (Game.Round % 4)
            {
                case 0:
                case 1:
                    SmallBlindWidthMult = 0.1665;
                    BigBlindWidthMult = 0.163;
                    BossBlindWidthMult = BigBlindWidthMult;

                    SmallBlindHeightMult = 0.7166;
                    BigBlindHeightMult = 0.6576;
                    BossBlindHeightMult = BigBlindHeightMult;
                    break;
                case 2:
                    SmallBlindWidthMult = 0.163;
                    BigBlindWidthMult = 0.1665;
                    BossBlindWidthMult = SmallBlindWidthMult;

                    SmallBlindHeightMult = 0.6576;
                    BigBlindHeightMult = 0.7166;
                    BossBlindHeightMult = SmallBlindHeightMult;
                    break;
                case 3:
                    SmallBlindWidthMult = 0.163;
                    BigBlindWidthMult = SmallBlindWidthMult;
                    BossBlindWidthMult = 0.1665;

                    SmallBlindHeightMult = 0.6576;
                    BigBlindHeightMult = SmallBlindHeightMult;
                    BossBlindHeightMult = 0.7166;
                    break;
            }
        }

        private string XDashY(int X, int Y) => Helper.XDashY(X, Y);

        private void SelectBlind()
        {
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

            var optionsHwnd = WindowNative.GetWindowHandle(optionsWindow);

            NativeMethods.SetWindowLongPtr(optionsHwnd, GWLP_HWNDPARENT, mainHwnd);

            optionsWindow.Activate();
        }

        private void RunInfo_Closed(object sender, WindowEventArgs args)
        {
            runInfoWindow = null;
        }

        private void Options_Closed(object sender, WindowEventArgs args)
        {
            optionsWindow = null;
        }
    }
}
