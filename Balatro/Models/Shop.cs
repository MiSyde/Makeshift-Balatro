using Balatro.Models.Jokers;
using Balatro.Models.Vouchers;
using Balatro.Util;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Balatro.Models
{
    public class Shop
    {
        public readonly RelayCommand RerollCommand;
        public int RerollPrice { get; set; } = 5;
        public ObservableCollection<IEffect> CurrentShop;
        public List<IJoker> CommonJokers;
        public List<IJoker> UncommonJokers;
        public List<IJoker> RareJokers;
        public Random Random;
        public int ShopSize { get; set; } = 2;
        public double PriceModifier { get; set; } = 1;
        public int ChanceModifier { get; set; } = 1;

        public Shop()
        {
            RerollCommand = new RelayCommand(RerollShop, CanReroll);
            CurrentShop = new ObservableCollection<IEffect>();
            Random = new Random();

            FillLists();
        }

        private void FillLists()
        {
            CommonJokers = Helper.GenerateUnlocked<IJoker>("Balatro.Models.Jokers.Common");
            UncommonJokers = Helper.GenerateUnlocked<IJoker>("Balatro.Models.Jokers.Uncommon");
            RareJokers = Helper.GenerateUnlocked<IJoker>("Balatro.Models.Jokers.Rare");
        }

        private IJoker ModifyModifier(IJoker joker)
        {
            double rVal = Random.Next(1, 100);

            int polyModifier;
            if (ChanceModifier == 2) polyModifier = 3;
            else if (ChanceModifier == 4) polyModifier = 7;
            else polyModifier = 1;

            double basePolyChance = 99.7 - polyModifier * 0.3;
            double baseHoloChance = basePolyChance - ChanceModifier * 1.4;
            double baseFoilChance = baseHoloChance - ChanceModifier * 2;

            if (rVal > baseFoilChance && rVal <= baseHoloChance) { joker.Modifier = Enums.Modifier.FOIL; } 
            else if(rVal > baseHoloChance && rVal <= basePolyChance) { joker.Modifier = Enums.Modifier.HOLOGRAPHIC; }
            else if(rVal > basePolyChance && rVal <= 99.7) { joker.Modifier = Enums.Modifier.POLYCHROME; }
            else if(rVal > 99.7 && rVal <= 100) { joker.Modifier = Enums.Modifier.NEGATIVE; }
            else { joker.Modifier = Enums.Modifier.BASE; }

            return joker;
        }

        private bool CanReroll() => App.CurrentGame.Player.Money >= RerollPrice;

        private void RerollShop()
        {
            ++RerollPrice;
            CurrentShop.Clear();
            FillUpShop();
        }

        public void FillUpShop()
        {
            do
            {
                int rVal = Random.Next(1, 100);
                IJoker joker;
                switch (rVal)
                {
                    case <= 70:
                        joker = CommonJokers[Random.Next(0, CommonJokers.Count - 1)];
                        CurrentShop.Add(ModifyModifier(joker));
                        break;
                    case > 70 and <= 95:
                        joker = UncommonJokers[Random.Next(0, UncommonJokers.Count - 1)];
                        CurrentShop.Add(ModifyModifier(joker));
                        break;
                    default:
                        joker = RareJokers[Random.Next(0, RareJokers.Count - 1)];
                        CurrentShop.Add(joker);
                        break;
                }
            } while (CurrentShop.Count != ShopSize);
        }
    }
}
