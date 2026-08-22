using Balatro.Enums;
using Balatro.Models;
using Balatro.Util;
using Microsoft.UI;
using Microsoft.UI.Windowing;
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
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics;
using Windows.Graphics.Imaging;
using WinRT.Interop;

namespace Balatro
{
    public sealed partial class Balatro_Page : Page
    {
        BalatroGame game => App.CurrentGame;
        RunInfoWindow? runInfoWindow;
        OptionsWindow? optionsWindow;
        IntPtr mainHwnd;
        private const int GWLP_HWNDPARENT = -8;

        public Balatro_Page()
        {
            InitializeComponent();

            SizeChanged += Balatro_Page_SizeChanged;
            

            NavigationCacheMode = NavigationCacheMode.Required;
        }

        private async void Balatro_Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetBorderDimensions();

            if (game.Round % 4 == 1)
            {
                ((ImageBrush)InnerNameGridBorder.Background).ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Borders/SmallBlindBorder.png"));
            }
            else if(game.Round % 4 == 2)
            {
                ((ImageBrush)InnerNameGridBorder.Background).ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Borders/BigBlindBorder.png"));
            }
            else if (game.Round % 4 == 3)
            {
                ((ImageBrush)InnerNameGridBorder.Background).ImageSource = new BitmapImage(new Uri("ms-appx:///Assets/Borders/" + game.BossBlind.GetDescription() + "BlindBorder.png"));
            }
        }

        public string ConvertEnumDictToIntString(ObservableDictionary<Hand, int> dict, Hand hand)
        {
            return dict[hand].ToString();
        }

        public string ConvertRoundToBlind(int round)
        {
            return game.Blinds[round % 4];
        }

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

        private void Card_Selected(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem is Card clickedCard)
            {
                game.CardPressed(clickedCard);
            }
        }

        private void SetBorderDimensions()
        {
            CurrentSelectionInfoBorder.Height = this.ActualHeight * 0.1944;

            ChipsBorder.Height = this.ActualHeight * 0.082;

            MultiplierBorder.Height = this.ActualHeight * 0.082;

            ScoreBorder.Height = this.ActualHeight * 0.07;

            PlayCardsButtonBorder.Height = this.ActualHeight * 0.12;
            PlayCardsButtonBorder.Width = this.ActualWidth * 0.11;

            PlayCards.Height = PlayCardsButtonBorder.Height;
            PlayCards.Width = PlayCardsButtonBorder.Width;

            DiscardButtonBorder.Height = this.ActualHeight * 0.12;
            DiscardButtonBorder.Width = this.ActualWidth * 0.11;
            DiscardCards.Height = DiscardButtonBorder.Height;
            DiscardCards.Width = DiscardButtonBorder.Width;
        }

        private void DiscardCards_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue is bool isEnabled && !isEnabled)
            {
                var style = (Style)Application.Current.Resources["InactiveButtonContainer"];
                DiscardButtonBorder.Style = style;
            } 
            else
            {
                var style = (Style)Application.Current.Resources["MultiplierContainer"];
                DiscardButtonBorder.Style = style;
            }
            
        }

        private void PlayHand_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (e.NewValue is bool isEnabled && !isEnabled)
            {
                var style = (Style)Application.Current.Resources["InactiveButtonContainer"];
                PlayCardsButtonBorder.Style = style;
            }
            else
            {
                var style = (Style)Application.Current.Resources["ChipsContainer"];
                PlayCardsButtonBorder.Style = style;
            }
        }
    }
}
