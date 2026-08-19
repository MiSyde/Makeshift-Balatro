using Balatro.Models;
using Cards.Balatro;
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

namespace Balatro
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Balatro_Page : Page
    {
        BalatroGame game;
        public Balatro_Page()
        {
            game = new BalatroGame();
            InitializeComponent();
        }

        public string ConvertEnumDictToIntString(ObservableDictionary<Hand, int> dict, Hand hand)
        {
            return dict[hand].ToString();
        }

        public string ConvertRoundToBlind(int round)
        {
            return game.Blinds[round % 3];
        }
    }
}
