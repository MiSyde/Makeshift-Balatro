using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace Balatro;

/// <summary>
/// The menu page
/// </summary>
public sealed partial class MenuPage : Page
{
    private Frame MainFrame => App.MainFrame;
    public MenuPage()
    {
        InitializeComponent();
    }

    private void Play_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(typeof(Balatro_Page));
    }
}
