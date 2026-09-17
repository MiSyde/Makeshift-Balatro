using Balatro.Enums;
using Balatro.Models.Seals;
using Balatro.Util;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Balatro.Models
{
    public class Card : IEffect
    {
        public int Value { get; }
        public Visibility ButtonVisibility { get; set
            {
                if (field != value)
                {
                    field = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ButtonVisibility)));
                }
            }
        } = Visibility.Collapsed;
        public bool IsFaceCard { get; }
        public FaceCard? FaceCardType { get; }
        public SuitType SuitType { get; }
        public BitmapImage Image { get; }
        public Guid Id { get; }
        public IEffect? Seal { get; set; }
        public Modifier Modifier { get; set; }
        public List<Enhancement> AdditionalEnhancements { get; }
        public Enhancement BaseEnhancement { get; set; }
        public int RankOrder { get; }

        public string Name { get; } = string.Empty;

        public string Description { get; }


        public Card(BitmapImage Image, int Value, bool IsFaceCard, SuitType SType, int RankOrder, FaceCard? FCType = null)
        {
            this.RankOrder = RankOrder;
            this.Value = Value;
            this.IsFaceCard = IsFaceCard;
            FaceCardType = FCType;
            SuitType = SType;
            this.Image = Image;
            Id = new Guid();
            Modifier = Modifier.BASE;
            AdditionalEnhancements = new List<Enhancement>();
            if (IsFaceCard) Description = Helper.GetDescription(FCType!) + " of " + Helper.GetDescription(SType);
            else Description = Value.ToString() + " of " + Helper.GetDescription(SType);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void Trigger_Seal(Player Player) 
        {
            
        }

        private void Trigger_Modifier(Player Player)
        {

        }

        private void TriggerEnhancements(Player Player)
        {

        }

        public void AddEffect(Player Player)
        {
            Trigger_Modifier(Player);
            Trigger_Seal(Player);
        }
    }
}
