using Balatro.Util;
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
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using WinRT.Interop;

namespace Balatro;

/// <summary>
/// A pop-up window that displays the Options menu.
/// </summary>
public sealed partial class OptionsWindow : Window
{
    private IntPtr hwnd;
    private IntPtr parentHwnd;
    private NativeMethods.WndProcDelegate? newWndProc;
    private IntPtr oldWndProc;
    private BalatroGame game => App.CurrentGame;
    public OptionsWindow(IntPtr parentHwnd)
    {
        InitializeComponent();

        this.parentHwnd = parentHwnd;
        hwnd = WindowNative.GetWindowHandle(this);
        newWndProc = new NativeMethods.WndProcDelegate(WndProc);
        oldWndProc = NativeMethods.SetWindowLongPtr(hwnd, NativeMethods.GWLP_WNDPROC, newWndProc);

        var windowId = Microsoft.UI.Win32Interop.GetWindowIdFromWindow(hwnd);
        var appWindow = Microsoft.UI.Windowing.AppWindow.GetFromWindowId(windowId);
        appWindow.TitleBar.ExtendsContentIntoTitleBar = true;
        appWindow.SetPresenter(Microsoft.UI.Windowing.OverlappedPresenter.CreateForContextMenu());

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
}
