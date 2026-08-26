using Balatro.Models.Achievement;
using Microsoft.UI;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Animation;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

namespace Balatro
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        private Frame MainFrame => App.MainFrame;
        public MainWindow()
        {
            InitializeComponent();

            var appWindow = AppWindow;
            appWindow.SetPresenter(AppWindowPresenterKind.FullScreen);

            Closed += SaveProgress;

            MainFrame.Navigate(typeof(MenuPage), null, new ContinuumNavigationTransitionInfo());
        }

        private void SaveProgress(object sender, WindowEventArgs args)
        {
            App.AchievementManager.SaveProgress();
        }
    }
}
