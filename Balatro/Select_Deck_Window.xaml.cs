using Balatro.Models.Decks;
using Balatro.Util;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

namespace Balatro
{
    /// <summary>
    /// Pop-up window where the player selects their deck before the game could start
    /// </summary>
    public sealed partial class Select_Deck_Window : Window, INotifyPropertyChanged
    {
        private IntPtr hwnd;
        private IntPtr parentHwnd;
        private NativeMethods.WndProcDelegate? newWndProc;
        private IntPtr oldWndProc;
        public RelayCommand SelectDeckCommand { get; }

        public event PropertyChangedEventHandler? PropertyChanged;

        public Dictionary<IDeck, string> Decks { get; set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Decks)));
                }
            }
        }
        public IDeck SelectedDeck { get; private set; }
        public IDeck PreviewDeck { get; 
            set 
            { 
                if (field != value) 
                { 
                    field = value; 
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(PreviewDeck))); 
                } 
            } 
        }
        public Visibility ShowDetails { get; set 
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ShowDetails)));
                }
            } 
        } = Visibility.Collapsed;
        public Select_Deck_Window(IntPtr parentHwnd, Dictionary<IDeck, string> Decks)
        {
            InitializeComponent();
            RootGrid.DataContext = this;
            SelectDeckCommand = new RelayCommand(SelectDeck, CanSelectDeck);

            this.Decks = Decks;

            this.parentHwnd = parentHwnd;
            hwnd = WindowNative.GetWindowHandle(this);
            newWndProc = new NativeMethods.WndProcDelegate(WndProc);
            oldWndProc = NativeMethods.SetWindowLongPtr(hwnd, NativeMethods.GWLP_WNDPROC, newWndProc);

            var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
            var appWindow = AppWindow.GetFromWindowId(windowId);
            appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
            appWindow.SetPresenter(OverlappedPresenter.CreateForContextMenu());

            var dpi = NativeMethods.GetDpiForWindow((nint)appWindow.Id.Value);
            var height = NativeMethods.DipToPhysical(1100, dpi);
            var width = NativeMethods.DipToPhysical(1100, dpi);

            appWindow.MoveAndResize(new Windows.Graphics.RectInt32
            {
                Height = height,
                Width = width,
                X = NativeMethods.GetSystemMetrics(NativeMethods.SM_CXSCREEN) / 2 - width / 2,
                Y = NativeMethods.GetSystemMetrics(NativeMethods.SM_CYSCREEN) / 2 - height / 2,
            });

            Activated += Activated_Event;
        }
        private IntPtr WndProc(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam)
        {
            if (msg == NativeMethods.WM_WINDOWPOSCHANGING && parentHwnd != IntPtr.Zero)
            {
                var pos = Marshal.PtrToStructure<NativeMethods.WINDOWPOS>(lParam);

                bool gotRect = NativeMethods.GetWindowRect(parentHwnd, out var parentRect);

                if (gotRect && parentRect.Right > parentRect.Left && parentRect.Bottom > parentRect.Top)
                {
                    int width = pos.cx;
                    int height = pos.cy;

                    int minX = parentRect.Left;
                    int maxX = Math.Max(minX, parentRect.Right - width);
                    int minY = parentRect.Top;
                    int maxY = Math.Max(minY, parentRect.Bottom - height);

                    pos.x = Math.Clamp(pos.x, minX, maxX);
                    pos.y = Math.Clamp(pos.y, minY, maxY);

                    Marshal.StructureToPtr(pos, lParam, true);
                }
            }

            return NativeMethods.CallWindowProc(oldWndProc, hWnd, msg, wParam, lParam);
        }
        private void Activated_Event(object sender, WindowActivatedEventArgs e)
        {
            if (e.WindowActivationState == WindowActivationState.Deactivated)
            {
                Close();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Display_Deck(object sender, ItemClickEventArgs e)
        {
            PreviewDeck = (IDeck)e.ClickedItem;
            SelectDeckCommand.NotifyCanExecuteChanged();
            ShowDetails = Visibility.Visible;
        }

        private void SelectDeck()
        {
            SelectedDeck = PreviewDeck;
            Close();
        }

        private bool CanSelectDeck() => PreviewDeck != null;
    }
}
