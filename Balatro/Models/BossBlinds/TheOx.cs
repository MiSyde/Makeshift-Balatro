using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Balatro.Models.BossBlinds
{
    public class TheOx : IBossBlind
    {
        public string Name => "The Ox";
        public int MinAnte => 6;

        public string Description => "Playing the most played hand this run sets money to $0";

        public BitmapImage Image => new BitmapImage(new Uri("ms-appx:///Assets/BlindImages/The_Ox.png"));

        public bool MatadorCompatible => true;

        public double BaseThresholdMultiplier => 2;

        public int EarnedMoney => 5;

        public void AddEffect(BalatroGame Game)
        {
            Player Player = Game.Player;
            int Max = Player.HandTimes.Values.Max();
            Enums.Hand MostPlayedHand = Player.HandTimes.FirstOrDefault(x => x.Value == Max).Key;

            if (Player.HighestHand == MostPlayedHand) Player.Money = 0;
        }
    }
}
