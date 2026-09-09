using Balatro.Models.Jokers;
using Balatro.Models.Jokers.Common;
using Balatro.Models.Packs;
using Balatro.Models.Vouchers;
using Balatro.Util;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections;
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
        public ObservableCollection<ConsumablePack<IEffect>> PackShop { get; }
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
        public double NormalBuffoonWeight => 13.2;
        public double NormalSpectralWeight => 13.8;
        public double JumboStACWeight => 15.8;
        public double JumboBuffoonWeight => 19.8; 
        public double JumboSpectralWeight => 20.1;
        public double MegaStACWeight => 20.6;
        public double MegaBuffoonWeight => 21.75;
        public double MegaSpectralWeight => 21.82;
        public int VoucherShopSize { get; internal set; }

        public Shop()
        {
            RerollCommand = new RelayCommand(RerollShop, CanReroll);

            CurrentShop = new ObservableCollection<IEffect>();
            VoucherShop = new ObservableCollection<IVoucher>();
            PackShop = new ObservableCollection<ConsumablePack<IEffect>>();

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
            Vouchers = Helper.GenerateClassesInNamespace<IVoucher>("Balatro.Models.Vouchers");
            Planets = Helper.GenerateClassesInNamespace<IEffect>("Balatro.Models.Planets");
            Tarots = Helper.GenerateClassesInNamespace<IEffect>("Balatro.Models.Tarots");
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
                    CurrentShop.Add(GetCard());
                }
                
            } while (CurrentShop.Count != ShopSize);
        }

        public IJoker GetJoker(List<IJoker> Jokers) => Jokers[Random.Next(0, Jokers.Count - 1)];

        private Card GetCard() => Cards[Random.Next(0, Cards.Count - 1)];
        public IVoucher GetVoucher() => Vouchers[Random.Next(0, Vouchers.Count - 1)];
        public IEffect GetPlanet() => Planets[Random.Next(0, Planets.Count - 1)];
        public IEffect GetTarot() => Tarots[Random.Next(0, Tarots.Count - 1)];
        public IEffect GetSpectral() => Spectrals[Random.Next(0, Spectrals.Count - 1)];

        internal void FillPackShop()
        {
            PackShop.Clear();

            do
            {
                double pVal = Random.Shared.Next(0, 2242) / 100;
                int varValue = Random.Shared.Next(1, 4);
                Uri? Uri = null;
                switch (pVal)
                {
                    case var _ when pVal <= NormalStACWeight:
                        switch(varValue)
                        {
                            case 1:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Standard_Normal_1.png");
                                break;
                            case 2:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Standard_Normal_2.png");
                                break;
                            case 3:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Standard_Normal_3.png");
                                break;
                            case 4:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Standard_Normal_4.png");
                                break;
                        }
                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 3, GetCard, 1, "Normal Standard Pack",
                            "Choose 1 of up to 3 Playing cards to add to your deck", 4));
                        break;
                    case var _ when pVal <= NormalStACWeight * 2:
                        switch (varValue)
                        {
                            case 1:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Arcana_Normal_1.png");
                                break;
                            case 2:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Arcana_Normal_2.png");
                                break;
                            case 3:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Arcana_Normal_3.png");
                                break;
                            case 4:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Arcana_Normal_4.png");
                                break;
                        }
                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 3, GetTarot, 1, "Normal Arcana Pack",
                            "Choose 1 of up to 3 Tarot cards to be used immediately", 4));
                        break;
                    case var _ when pVal <= NormalStACWeight * 3:
                        switch (varValue)
                        {
                            case 1:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Celestial_Normal_1.png");
                                break;
                            case 2:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Celestial_Normal_2.png");
                                break;
                            case 3:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Celestial_Normal_3.png");
                                break;
                            case 4:
                                Uri = new Uri("ms-appx:///Assets/PackImages/Celestial_Normal_4.png");
                                break;
                        }
                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 3, GetPlanet, 1, "Normal Celestial Pack",
                            "Choose 1 of up to 3 Planet cards to be used immediately", 4));
                        break;
                    case var _ when pVal <= NormalBuffoonWeight:
                        if(varValue == 1) Uri = new Uri("ms-appx:///Assets/PackImages/Buffoon_Normal_1.png");
                        else Uri = new Uri("ms-appx:///Assets/PackImages/Buffoon_Normal_2.png");

                        PackShop.Add(new ConsumablePack<IEffect>(Uri, 2, () => GetJoker(GetJokerList()), 1, "Normal Buffoon Pack",
                            "Choose 1 of up to 2 Joker cards", 4));
                        break;
                    case var _ when pVal <= NormalSpectralWeight:
                        if(varValue == 0) Uri = new Uri("ms-appx:///Assets/PackImages/Spectral_Normal_1.png");
                        else Uri = new Uri("ms-appx:///Assets/PackImages/Spectral_Normal_2.png");

                        PackShop.Add(new ConsumablePack<IEffect>(Uri, 2, GetSpectral, 1, "Normal Spectral Pack",
                            "Choose 1 of up to 2 Spectral cards", 4));
                        break;
                    case var _ when pVal <= JumboStACWeight:
                        if (pVal == 1) Uri = new Uri("ms-appx:///Assets/PackImages/Arcana_Jumbo_1.png");
                        else Uri = new Uri("ms-appx:///Assets/PackImages/Arcana_Jumbo_2.png");

                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 5, GetTarot, 1, "Jumbo Arcana Pack",
                            "Choose 1 of up to 5 Tarot cards to be used immediately", 6));
                        break;
                    case var _ when pVal <= JumboStACWeight + 2:
                        if (pVal == 1) Uri = new Uri("ms-appx:///Assets/PackImages/Standard_Jumbo_1.png");
                        else Uri = new Uri("ms-appx:///Assets/PackImages/Standard_Jumbo_2.png");

                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 5, GetCard, 1, "Jumbo Standard Pack",
                            "Choose 1 of up to 5 Playing cards to add to your deck", 6));
                        break;
                    case var _ when pVal <= JumboStACWeight + 4:
                        if (pVal == 1) Uri = new Uri("ms-appx:///Assets/PackImages/Celestial_Jumbo_1.png");
                        else Uri = new Uri("ms-appx:///Assets/PackImages/Celestial_Jumbo_2.png");

                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 5, GetPlanet, 1, "Jumbo Celestial Pack",
                            "Choose 1 of up to 5 Planet cards to be used immediately", 6));
                        break;
                    case var _ when pVal <= JumboBuffoonWeight:
                        Uri = new Uri("ms-appx:///Assets/PackImages/Buffoon_Jumbo.png");
                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 4, () => GetJoker(GetJokerList()), 1, "Jumbo Buffoon Pack",
                            "Choose 1 of up to 4 Joker cards", 6));
                        break;
                    case var _ when pVal <= JumboSpectralWeight:
                        Uri = new Uri("ms-appx:///Assets/PackImages/Spectral_Jumbo.png");
                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 4, GetSpectral, 1, "Jumbo Spectral Pack",
                            "Choose 1 of up to 4 Spectral cards to be used immediately", 6));
                        break;
                    case var _ when pVal <= MegaStACWeight:
                        if (pVal == 1) Uri = new Uri("ms-appx:///Assets/PackImages/Celestial_Mega_1.png");
                        else Uri = new Uri("ms-appx:///Assets/PackImages/Celestial_Mega_2.png");

                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 5, GetPlanet, 2, "Mega Celestial Pack",
                            "Choose 2 of up to 5 Planet cards to be used immediately", 8));
                        break;
                    case var _ when pVal <= MegaStACWeight + 0.5:
                        if (pVal == 1) Uri = new Uri("ms-appx:///Assets/PackImages/Arcana_Mega_1.png");
                        else Uri = new Uri("ms-appx:///Assets/PackImages/Arcana_Mega_2.png");

                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 5, GetTarot, 2, "Mega Arcana Pack",
                            "Choose 2 of up to 5 Tarot cards to be used immediately", 8));
                        break;
                    case var _ when pVal <= MegaStACWeight + 1:
                        if (pVal == 1) Uri = new Uri("ms-appx:///Assets/PackImages/Standard_Mega_1.png");
                        else Uri = new Uri("ms-appx:///Assets/PackImages/Standard_Mega_2.png");

                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 5, GetCard, 2, "Mega Standard Pack",
                            "Choose 2 of up to 5 Playing cards to add to your deck", 8));
                        break;
                    case var _ when pVal <= MegaBuffoonWeight:
                        Uri = new Uri("ms-appx:///Assets/PackImages/Buffoon_Mega.png");
                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 4, () => GetJoker(GetJokerList()), 2, "Mega Spectral Pack",
                            "Choose 2 of up to 4 Joker cards", 8));
                        break;
                    case var _ when pVal <= MegaSpectralWeight:
                        Uri = new Uri("ms-appx:///Assets/PackImages/Spectral_Mega.png");
                        PackShop.Add(new ConsumablePack<IEffect>(Uri!, 4, GetSpectral, 2, "Mega Spectral Pack",
                            "Choose 2 of up to 4 Spectral cards to be used immediately", 8));
                        break;
                }
            } while (PackShop.Count != 2);
        }
        internal void FillVoucherShop()
        {
            do
            {
                VoucherShop.Add(Vouchers[Random.Next(0, Vouchers.Count - 1)]);
            } while (VoucherShop.Count != VoucherShopSize);
        }

        private List<IJoker> GetJokerList()
        {
            int lVal = Random.Next(1, 100);
            switch (lVal)
            {
                case <= 70:
                    return CommonJokers;
                case <= 95:
                    return UncommonJokers;
                default:
                    return RareJokers;
            }
        }
    }
}
