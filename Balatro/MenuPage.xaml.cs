using Balatro.Models;
using Balatro.Models.Decks;
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
using Windows.System;
using WinRT.Interop;

namespace Balatro;

/// <summary>
/// The menu page
/// </summary>
public sealed partial class MenuPage : Page
{
    private Frame MainFrame => App.MainFrame;
    private Dictionary<IDeck, string> Decks { get; } = [];
    private IDeck? SelectedDeck { get; set; } = null;
    RelayCommand PlayCommand { get; }
    Select_Deck_Window? deckWindow;
    private const int GWLP_HWNDPARENT = -8;
    IntPtr mainHwnd;
    public MenuPage()
    {
        InitializeComponent();
        NavigationCacheMode = NavigationCacheMode.Required;
        List<IDeck> Instances = Helper.GenerateUnlocked<IDeck>("Balatro.Models.Decks");
        foreach(IDeck Deck in Instances)
        {
            Decks.Add(Deck, Deck.Name);
        }
        PlayCommand = new RelayCommand(Play, CanPlay);
    }

    private void Play()
    {
        Player Player = App.CurrentGame.Player;
        Player.Deck = SelectedDeck;
        Player.Deck!.AddEffect(Player);
        App.CurrentGame.DealCards();
        MainFrame.Navigate(typeof(SelectionPage));
    }

    private bool CanPlay() => SelectedDeck != null;

    private void Select_Deck(object sender, RoutedEventArgs e)
    {
        var windowId = XamlRoot.ContentIslandEnvironment.AppWindowId;
        mainHwnd = Win32Interop.GetWindowFromWindowId(windowId);

        deckWindow = new Select_Deck_Window(mainHwnd, Decks);
        deckWindow.Closed += DeckWindow_Closed;

        var deckHwnd = WindowNative.GetWindowHandle(deckWindow);

        NativeMethods.SetWindowLongPtr(deckHwnd, GWLP_HWNDPARENT, mainHwnd);

        deckWindow.Activate();
    }

    private void DeckWindow_Closed(object sender, WindowEventArgs args)
    {
        SelectedDeck = deckWindow!.SelectedDeck;
        PlayCommand.NotifyCanExecuteChanged();
        deckWindow = null;
    }

    private void Play_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (e.NewValue is bool isEnabled && !isEnabled)
        {
            var style = (Style)Application.Current.Resources["InactiveButtonContainer"];
            PlayButtonBorder.Style = style;
        }
        else
        {
            var style = (Style)Application.Current.Resources["ChipsContainer"];
            PlayButtonBorder.Style = style;
        }

    }
}
