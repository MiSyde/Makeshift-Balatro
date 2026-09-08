using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.BossBlinds
{
    public class TheManacle : IBossBlind
    {
        public string Name => "The Manacle";

        public int MinAnte => 1;

        public string Description => "-1 Hand Size";

        public BitmapImage Image => new BitmapImage(new Uri("ms-appx:///Assets/BlindImages/The_Manacle.png"));

        public bool MatadorCompatible => false;

        public double BaseThresholdMultiplier => 2;

        public int EarnedMoney => 5;
        private bool FirstTrigger = true;

        public void AddEffect(BalatroGame Game)
        {
            if (FirstTrigger) --Game.Player.CurrentCardHoldingSize;
        }
    }
}
