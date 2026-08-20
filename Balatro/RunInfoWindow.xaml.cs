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
        private BalatroGame game => App.CurrentGame;
        private Visibility pokerHandsVisibility
        {
            get;
            set
            {
                if (value != pokerHandsVisibility)
                {
                    field = value;
                    OnPropertyChanged();
                }  
            }
        }
        private Visibility blindsVisibility
        {
            get;
            set
            {
                if (value != blindsVisibility)
                {
                    field = value;
                    OnPropertyChanged();
                }
            }
        }
        private Visibility vouchersVisibility
        {
            get;
            set
            {
                if (value != vouchersVisibility)
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
            if (msg == NativeMethods.WM_WINDOWPOSCHANGING)
            {
                var pos = Marshal.PtrToStructure<NativeMethods.WINDOWPOS>(lParam);

                NativeMethods.GetWindowRect(parentHwnd, out var parentRect);

                int width = pos.cx;
                int height = pos.cy;

                int clampedX = Math.Clamp(pos.x, parentRect.Left, parentRect.Right - width);
                int clampedY = Math.Clamp(pos.y, parentRect.Top, parentRect.Bottom - height);

                pos.x = clampedX;
                pos.y = clampedY;

                Marshal.StructureToPtr(pos, lParam, true);
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
            pokerHandsVisibility = Visibility.Visible;
            blindsVisibility = Visibility.Collapsed;
            vouchersVisibility = Visibility.Collapsed;
        }

        private void Blinds_Click(object sender, RoutedEventArgs e)
        {
            pokerHandsVisibility = Visibility.Collapsed;
            blindsVisibility = Visibility.Visible;
            vouchersVisibility = Visibility.Collapsed;
        }

        private void Vouchers_Click(object sender, RoutedEventArgs e)
        {
            pokerHandsVisibility = Visibility.Collapsed;
            blindsVisibility = Visibility.Collapsed;
            vouchersVisibility = Visibility.Visible;
        }

        private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}
