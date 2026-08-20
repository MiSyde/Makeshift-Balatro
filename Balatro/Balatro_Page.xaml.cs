using Balatro.Models;
using Balatro.Util;
using Microsoft.UI;
using Balatro.Enums;
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
using Windows.Graphics;
using WinRT.Interop;
using Microsoft.UI.Windowing;

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
            NavigationCacheMode = NavigationCacheMode.Required;
        }

        public string ConvertEnumDictToIntString(ObservableDictionary<Hand, int> dict, Hand hand)
        {
            return dict[hand].ToString();
        }

        public string ConvertRoundToBlind(int round)
        {
            return game.Blinds[round % 3];
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
    }
}
