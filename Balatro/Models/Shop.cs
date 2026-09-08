using Balatro.Models.Jokers;
using Balatro.Models.Jokers.Common;
using Balatro.Models.Packs;
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
using Windows.Foundation;

namespace Balatro.Models
{
    public class Shop
    {
        BalatroGame Game => App.CurrentGame;
        public readonly RelayCommand RerollCommand;
        public int RerollPrice { get; set; } = 5;
        public ObservableCollection<IEffect> CurrentShop { get; }
        public ObservableCollection<IVoucher> VoucherShop { get; }
        public ObservableCollection<Card> CardShop { get; }
        public ObservableCollection<ConsumablePack<IEffect>> ConsumablePackShop { get; }
        public ObservableCollection<ConsumablePack<IJoker>> JokerPackShop { get; }
        public ObservableCollection<ConsumablePack<Card>> CardPackShop { get; }
        public List<IEffect> Tarots;
        public List<IEffect> Planets;
        public List<IEffect> Spectrals;
        public List<IJoker> CommonJokers;
        public List<IJoker> UncommonJokers;
        public List<IJoker> RareJokers;
        public List<IVoucher> Vouchers;
        public List<Card> Cards;
        public Random Random;
        public int ShopSize { get; set; } = 2;
        public double PriceModifier { get; set; } = 1;
        public int ChanceModifier { get; set; } = 1;
        public int TarotWeight { get; set; }
        public int PlanetWeight { get; set; }
        public int JokerWeight { get; set; }
        public int CardWeight { get; set; }
        public int NormalStACWeight => 4; // Standard, Arcana, Celestial
        public double NormalBuffoonWeight => 5.2;
        public double NormalSpectralWeight => 5.8;
        public double JumboStACWeight => 7.8;
        public double JumboBuffoonWeight => 8.4;
        public double JumboSpectralWeight => 8.7;
        public double MegaStACWeight => 9.2;
        public double MegaBuffoonWeight => 9.35;
        public double MegaSpectralWeight => 9.42;
        public int VoucherShopSize { get; internal set; }

        public Shop()
        {
            RerollCommand = new RelayCommand(RerollShop, CanReroll);

            CurrentShop = new ObservableCollection<IEffect>();
            CardShop = new ObservableCollection<Card>();
            VoucherShop = new ObservableCollection<IVoucher>();
            ConsumablePackShop = new ObservableCollection<ConsumablePack<IEffect>>();
            JokerPackShop = new ObservableCollection<ConsumablePack<IJoker>>();
            CardPackShop = new ObservableCollection<ConsumablePack<Card>>();

            Random = new Random();
            Tarots = new List<IEffect>();
            Planets = new List<IEffect>();
            Spectrals = new List<IEffect>();
            Vouchers = new List<IVoucher>();
            Cards = new List<Card>();

            FillLists();
        }

        private void FillLists()
        {
            CommonJokers = Helper.GenerateUnlocked<IJoker>("Balatro.Models.Jokers.Common");
            UncommonJokers = Helper.GenerateUnlocked<IJoker>("Balatro.Models.Jokers.Uncommon");
            RareJokers = Helper.GenerateUnlocked<IJoker>("Balatro.Models.Jokers.Rare");
            Vouchers = Helper.GenerateUnlocked<IVoucher>("Balatro.Models.Vouchers");
            //Planets = Helper.GenerateUnlocked<IEffect>("Balatro.Models.Planets");
            //Tarots = Helper.GenerateUnlocked<IEffect>("Balatro.Models.Tarots");
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

        public IJoker GetJoker(List<IJoker> Jokers) => Jokers[Random.Next(0, Jokers.Count - 1)];

        private Card GetCard() => Cards[Random.Next(0, Cards.Count - 1)];
        public IVoucher GetVoucher() => Vouchers[Random.Next(0, Vouchers.Count - 1)];
        public IEffect GetPlanet() => Planets[Random.Next(0, Planets.Count - 1)];
        public IEffect GetTarot() => Tarots[Random.Next(0, Tarots.Count - 1)];
        public IEffect GetSpectral() => Spectrals[Random.Next(0, Spectrals.Count - 1)];
        
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

        internal void FillPackShop()
        {
            ConsumablePackShop.Clear();
            JokerPackShop.Clear();
            CardPackShop.Clear();

            do
            {
                double pVal = Random.Shared.Next(0, 2242) / 100;

                switch (pVal)
                {
                    case var _ when pVal <= NormalStACWeight:
                        break;
                    case var _ when pVal > NormalStACWeight && pVal <= NormalBuffoonWeight:
                        Uri Uri = null;
                        switch(Random.Shared.Next(0, 1))
                        {
                            case 0:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Buffoon_Normal_1.png");
                                break;
                            case 1:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Buffoon_Normal_2.png");
                                break;
                        }
                        List<IJoker> list;
                        int lVal = Random.Next(1, 100);
                        switch (lVal)
                        {
                            case <= 70:
                                list = CommonJokers;
                                break;
                            case > 70 and <= 95:
                                list = UncommonJokers;
                                break;
                            default:
                                list = RareJokers;
                                break;
                        }
                        JokerPackShop.Add(new ConsumablePack<IJoker>(Uri!, 2, () => GetJoker(list), 1, "Normal Buffoon Pack",
                            "Choose 1 of up to 2 Joker cards", 4));
                        break;
                    case var _ when pVal > NormalBuffoonWeight && pVal <= NormalSpectralWeight:

                        break;
                    case var _ when pVal > NormalSpectralWeight && pVal <= JumboStACWeight:
                        break;
                    case var _ when pVal > JumboStACWeight && pVal <= JumboBuffoonWeight:
                        break;
                    case var _ when pVal > JumboBuffoonWeight && pVal <= JumboSpectralWeight:
                        break;
                    case var _ when pVal > JumboSpectralWeight && pVal <= MegaStACWeight:
                        break;
                    case var _ when pVal > MegaStACWeight && pVal <= MegaBuffoonWeight:
                        break;
                    case var _ when pVal > MegaBuffoonWeight && pVal <= MegaSpectralWeight:
                        break;
                }
            } while (ConsumablePackShop.Count + JokerPackShop.Count + CardPackShop.Count != 2);
        }
        internal void FillVoucherShop()
        {
            throw new NotImplementedException();
        }
    }
}
