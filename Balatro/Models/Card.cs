using Balatro.Enums;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;

namespace Balatro.Models
{
    public class Card
    {
        private int value;
        private bool isFaceCard;
        private FaceCard? faceCardType;
        private SuitType suitType;
        public int Value { get => value; }
        public bool IsFaceCard { get => isFaceCard; }
        public FaceCard? FaceCardType { get => faceCardType; }
        public SuitType SuitType { get => suitType; }
        public BitmapImage Image { get; }
        public Guid Id { get; }
        public Seal? Seal { get; set; }
        public Modifier Modifier { get; set; }
        public IList<Enhancement> Enhancements { get; }

        public Card(BitmapImage Image, int Value, bool IsFaceCard, SuitType SType, FaceCard? FCType = null)
        {
            value = Value;
            isFaceCard = IsFaceCard;
            faceCardType = FCType;
            suitType = SType;
            this.Image = Image;
            Id = new Guid();
            Modifier = Modifier.BASE;
            Enhancements = new List<Enhancement>();
        }
    }
}
