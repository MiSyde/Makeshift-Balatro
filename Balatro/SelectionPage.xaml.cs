using Balatro.Enums;
using Balatro.Models;
using Balatro.Models.Jokers;
using Balatro.Models.Tags;
using Balatro.Util;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
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
        private RelayCommand<ITag> SkipSmallBlindCommand;
        private RelayCommand<ITag> SkipBigBlindCommand;
        RunInfoWindow? runInfoWindow;
        OptionsWindow? optionsWindow;
        IntPtr mainHwnd;
        private const int GWLP_HWNDPARENT = -8;
        ITag[] CurrentTags { get; }
        List<ITag> Tags { get; }
        public SelectionPage()
        {
            InitializeComponent();
            SizeChanged += SelectionPage_SizeChanged;

            NavigationCacheMode = NavigationCacheMode.Required;

            SelectSmallBlindCommand = new RelayCommand(SelectBlind, CanSelectSmallBlind);
            SelectBigBlindCommand = new RelayCommand(SelectBlind, CanSelectBigBlind);
            SelectBossBlindCommand = new RelayCommand(SelectBlind, CanSelectBossBlind);

            SkipSmallBlindCommand = new RelayCommand<ITag>(Tag => SkipBlind(Tag!), Tag => CanSelectSmallBlind());
            SkipBigBlindCommand = new RelayCommand<ITag>(Tag => SkipBlind(Tag!), Tag => CanSelectBigBlind());

            CurrentTags = new ITag[2];
            Tags = Helper.GenerateClassesInNamespace<ITag>("Balatro.Models.Tags");

            Game.AssignBlindCommands(SelectSmallBlindCommand, SelectBigBlindCommand, SelectBossBlindCommand);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            if (Game.Round % 4 == 1)
            {
                CurrentTags[0] = Tags[Random.Shared.Next(0, Tags.Count - 1)];
                CurrentTags[1] = Tags[Random.Shared.Next(0, Tags.Count - 1)];
            }
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

        private void SkipBlind(ITag tag)
        {
            ++Game.Round;
            Game.Player.TotalSavedDiscardsCount += Game.Player.Discards;

            SkipBigBlindCommand.NotifyCanExecuteChanged();
            SkipSmallBlindCommand.NotifyCanExecuteChanged();
            SelectBigBlindCommand.NotifyCanExecuteChanged();
            SelectBossBlindCommand.NotifyCanExecuteChanged();
            SelectSmallBlindCommand.NotifyCanExecuteChanged();

            switch(tag)
            {
                case Negative:
                case Rare:
                case Uncommon:
                    tag.ApplyEffect(App.Shop);
                    break;
            }
            Game.Player.Tags.Add(tag.Image);
        }
    }
}
