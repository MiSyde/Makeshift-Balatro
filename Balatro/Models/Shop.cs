using Balatro.Models.Jokers;
using Balatro.Models.Vouchers;
using Balatro.Util;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Reflection.PortableExecutable;
using System.Text;

namespace Balatro.Models
{
    public class Shop
    {
        BalatroGame Game => App.CurrentGame;
        public readonly RelayCommand RerollCommand;
        public int RerollPrice { get; set; } = 5;
        public ObservableCollection<IEffect> CurrentShop;
        public ObservableCollection<Card> CardShop;
        public List<IEffect> Tarots;
        public List<IEffect> Planets;
        public List<IJoker> CommonJokers;
        public List<IJoker> UncommonJokers;
        public List<IJoker> RareJokers;
        public Random Random;
        public int ShopSize { get; set; } = 2;
        public double PriceModifier { get; set; } = 1;
        public int ChanceModifier { get; set; } = 1;
        public int TarotWeight { get; set; }
        public int PlanetWeight { get; set; }
        public int JokerWeight { get; set; }
        public int CardWeight { get; set; }

        public Shop()
        {
            RerollCommand = new RelayCommand(RerollShop, CanReroll);
            CurrentShop = new ObservableCollection<IEffect>();
            CardShop = new ObservableCollection<Card>();
            Random = new Random();
            Tarots = new List<IEffect>();
            Planets = new List<IEffect>();

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

            if (rVal > baseFoilChance && rVal <= baseHoloChance) { joker.Modifier = Enums.Modifier.FOIL; joker.Price += 2; } 
            else if(rVal > baseHoloChance && rVal <= basePolyChance) { joker.Modifier = Enums.Modifier.HOLOGRAPHIC; joker.Price += 3; }
            else if(rVal > basePolyChance && rVal <= 99.7) { joker.Modifier = Enums.Modifier.POLYCHROME; joker.Price += 5; }
            else if(rVal > 99.7 && rVal <= 100) { joker.Modifier = Enums.Modifier.NEGATIVE; joker.Price += 5; }
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
                int wValue = Random.Next(0, CardWeight + JokerWeight + PlanetWeight + TarotWeight);
                if(wValue <= JokerWeight)
                {
                    int jVal = Random.Next(1, 100);
                    switch (jVal)
                    {
                        case <= 70:
                            CurrentShop.Add(ModifyModifier(GetJoker(CommonJokers)));
                            break;
                        case > 70 and <= 95:
                            CurrentShop.Add(ModifyModifier(GetJoker(UncommonJokers)));
                            break;
                        default:
                            CurrentShop.Add(GetJoker(RareJokers));
                            break;
                    }
                } 
                else if(JokerWeight + PlanetWeight <= wValue && wValue > JokerWeight)
                {
                    CurrentShop.Add(Planets[Random.Next(0, Planets.Count - 1)]);
                } 
                else if(JokerWeight + PlanetWeight > wValue && wValue <= JokerWeight + PlanetWeight + TarotWeight)
                {
                    CurrentShop.Add(Tarots[Random.Next(0, Tarots.Count - 1)]);
                } 
                else
                {
                    CardShop.Add(GetCard());
                }
                
            } while (CurrentShop.Count + CardShop.Count != ShopSize);
        }

        public IJoker GetJoker(List<IJoker> Jokers) {
            IJoker Joker;

            do
            {
                Joker = Jokers[Random.Next(0, Jokers.Count - 1)];
            } while (Joker.MinAnte > Game.Ante);

            return Joker;
        }

        private Card GetCard()
        {
            return null;
        }

        public void VoucherEffects()
        {
            foreach(IVoucher v in Game.Player.Vouchers)
            {
                switch(v)
                {
                    case Hone:
                        break;
                }
            }
        }

    }
}
