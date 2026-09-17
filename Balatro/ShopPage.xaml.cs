using Balatro.Models;
using Balatro.Models.Jokers;
using Balatro.Models.Tarots;
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
    Shop Shop => App.Shop;
    RunInfoWindow? runInfoWindow;
    OptionsWindow? optionsWindow;
    IntPtr mainHwnd;
    private const int GWLP_HWNDPARENT = -8;
    IEffect? currentShopItem;
    RelayCommand<IEffect> BuyFromItemShopCommand;
    RelayCommand<IVoucher> BuyVoucherCommand;
    RelayCommand<ConsumablePack> BuyPackCommand;

    public ShopPage()
    {
        InitializeComponent();

        NavigationCacheMode = NavigationCacheMode.Required;

        BuyFromItemShopCommand = new RelayCommand<IEffect>((Item) => Buy(Item!), (Item) => CanBuy(Item!));
        BuyVoucherCommand = new RelayCommand<IVoucher>((Voucher) => Buy(Voucher!), (Voucher) => CanBuy(Voucher!));
        BuyPackCommand = new RelayCommand<ConsumablePack>((Pack) => Buy(Pack!), (Pack) => CanBuy(Pack!));

        SizeChanged += ShopPage_SizeChanged;
    }

    private void Buy(IEffect Item)
    {
        switch(Item)
        {
            case IPassiveJoker:
                Game.Player.Jokers.Add((IPassiveJoker) Item);
                break;
            case IJoker:
                Game.Player.Jokers.Add((IJoker)Item);
                break;
            case Card:
                Game.Player.Deck.Cards.Add((Card)Item);
                break;
            default:
                Game.Player.Consumables.Add(Item);
                break;
        }
        Game.Player.Money -= Item.Price;
    }

    private void Buy(IVoucher Voucher)
    {
        Game.Player.Vouchers.Add(Voucher);
        Game.Player.Money -= Voucher.Price;
    }

    private void Buy(ConsumablePack Pack)
    {
        Game.Player.Money -= Pack.Price;
        App.MainFrame.Navigate(typeof(PackAction_Page), Pack);
    }

    private bool CanBuy(IEffect Item) => Item.Price <= Game.Player.Money;
    private bool CanBuy(ConsumablePack Pack) => Pack.Price <= Game.Player.Money;
    private bool CanBuy(IVoucher Voucher) => Voucher.Price <= Game.Player.Money;
    
    protected override void OnNavigatedFrom(NavigationEventArgs e)
    {
        base.OnNavigatedFrom(e);

        if (Game.Round % 4 == 0) Game.Player.Tags.Clear();
    }

    protected override void OnNavigatedTo(NavigationEventArgs e)
    {
        base.OnNavigatedTo(e);
        Shop.CurrentShop.Clear();

        Shop.RerollPrice = 5;

        foreach (IVoucher v in Game.Player.Vouchers)
        {
            v.ApplyEffect(Shop);
        }
        
        Shop.FillUpShop();

        Shop.FillPackShop();

        Shop.FillVoucherShop();
    }

    private void ShopPage_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        SolidColorBrush opaque = new SolidColorBrush(Windows.UI.Color.FromArgb(50, 35, 35, 35));
        SolidColorBrush gray = new SolidColorBrush(Windows.UI.Color.FromArgb(255,59, 81, 85));

        JokersGridView.Background = opaque;
        ConsumablesGridView.Background = opaque;

        PackShopGridView.Background = gray;
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

    private string GetAnteString(int Ante) => "ANTE " + Ante.ToString() + " VOUCHER";

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

    private void UnselectOnLosingFocus(UIElement sender, LosingFocusEventArgs args)
    {
        if (sender is GridView gv)
        {
            var newFocus = args.NewFocusedElement;

            if (newFocus == null || !InsideGridView(newFocus, gv))
            {
                if (currentShopItem != null)
                {
                    currentShopItem.ButtonVisibility = Visibility.Collapsed;
                    currentShopItem = null;
                }
                else if (gv.SelectedItem is ConsumablePack Pack)
                    Pack.ButtonVisibility = Visibility.Collapsed;

                gv.SelectedIndex = -1;
            }
        }
    }

    private bool InsideGridView(DependencyObject Child, DependencyObject Parent)
    {
        while(Child != null)
        {
            if(Child == Parent)
            {
                return true;
            }
            Child = VisualTreeHelper.GetParent(Child);
        }
        return false;
    }

    private void ShowBuyButtonItemsGridView(object sender, ItemClickEventArgs e)
    {
        if (e.ClickedItem is IEffect effect)
        {
            if (currentShopItem == effect)
            {
                effect.ButtonVisibility = Visibility.Collapsed;
                currentShopItem = null;
            }
            else
            {
                currentShopItem?.ButtonVisibility = Visibility.Collapsed;
                
                BuyFromItemShopCommand.NotifyCanExecuteChanged();
                Button? b = (Button) BuyableItemsGridView.FindName("BuyItemButton");
                effect.ButtonVisibility = Visibility.Visible;
                currentShopItem = effect;
            }
        }
    }
}
