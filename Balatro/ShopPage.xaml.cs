using Balatro.Models;
using Balatro.Models.Jokers;
using Balatro.Models.Vouchers;
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
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

namespace Balatro;

/// <summary>
/// The shop that shows up between Antes
/// </summary>
public sealed partial class ShopPage : Page
{
    BalatroGame Game => App.CurrentGame;
    Shop Shop { get; }
    RunInfoWindow? runInfoWindow;
    OptionsWindow? optionsWindow;
    IntPtr mainHwnd;
    private const int GWLP_HWNDPARENT = -8;
    public ShopPage()
    {
        InitializeComponent();

        Shop = new Shop();

        NavigationCacheMode = NavigationCacheMode.Required;

        SizeChanged += ShopPage_SizeChanged;
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        Shop.CurrentShop.Clear();

        Shop.VoucherEffects();

        Shop.RerollPrice = 5;

        foreach (IVoucher v in Game.Player.Vouchers)
        {
            v.ApplyEffect(Shop);
        }

        Shop.FillUpShop();
    }

    private void ShopPage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        SolidColorBrush opaque = new SolidColorBrush(Windows.UI.Color.FromArgb(50, 35, 35, 35));
        SolidColorBrush gray = new SolidColorBrush(Windows.UI.Color.FromArgb(255,59, 81, 85));

        JokersGridView.Background = opaque;
        ConsumablesGridView.Background = opaque;

        PacksGridView.Background = gray;
        BuyableItemsGridView.Background = gray;
    }

    private void NextRound_Click(object sender, RoutedEventArgs e) => App.MainFrame.Navigate(typeof(SelectionPage));

    private void RerollButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if(e.NewValue is bool isEnabled && !isEnabled)
        {
            var style = (Style)Application.Current.Resources["InactiveButtonContainer"];
            RerollBorder.Style = style;
        } 
        else 
        {
            var style = (Style)Application.Current.Resources["RerollContainer"];
            RerollBorder.Style = style;
        }

    }
    private string XDashY(int x, int y) => Helper.XDashY(x, y);

    private string GetAnteString() => "ANTE " + Game.Ante.ToString() + " VOUCHER";

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
