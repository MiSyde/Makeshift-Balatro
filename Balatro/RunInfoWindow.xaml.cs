using Balatro.Util;
using Balatro.Enums;
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
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

namespace Balatro
{
    /// <summary>
    /// A pop-up window displaying info about the current run.
    /// </summary>
    public sealed partial class RunInfoWindow : Window, INotifyPropertyChanged
    {
        private IntPtr hwnd;
        private IntPtr parentHwnd;
        private NativeMethods.WndProcDelegate? newWndProc;
        private IntPtr oldWndProc;
        public BalatroGame Game => App.CurrentGame;
        public Visibility PokerHandsVisibility
        {
            get;
            set
            {
                if (value != PokerHandsVisibility)
                {
                    field = value;
                    OnPropertyChanged();
                }  
            }
        }
        public Visibility BlindsVisibility
        {
            get;
            set
            {
                if (value != BlindsVisibility)
                {
                    field = value;
                    OnPropertyChanged();
                }
            }
        }
        public Visibility VouchersVisibility
        {
            get;
            set
            {
                if (value != VouchersVisibility)
                {
                    field = value;
                    OnPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public RunInfoWindow(IntPtr parentHwnd)
        {
            InitializeComponent();

            BaseGrid.DataContext = this;

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
            if(e.WindowActivationState == WindowActivationState.Deactivated)
            {
                Close();
            }
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PokerHands_Click(object sender, RoutedEventArgs e)
        {
            PokerHandsVisibility = Visibility.Visible;
            BlindsVisibility = Visibility.Collapsed;
            VouchersVisibility = Visibility.Collapsed;
        }

        private void Blinds_Click(object sender, RoutedEventArgs e)
        {
            PokerHandsVisibility = Visibility.Collapsed;
            BlindsVisibility = Visibility.Visible;
            VouchersVisibility = Visibility.Collapsed;
        }

        private void Vouchers_Click(object sender, RoutedEventArgs e)
        {
            PokerHandsVisibility = Visibility.Collapsed;
            BlindsVisibility = Visibility.Collapsed;
            VouchersVisibility = Visibility.Visible;
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
