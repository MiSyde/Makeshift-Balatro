using Balatro.Enums;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace Balatro.Models
{
    public class Card
    {
        private int value;
        private bool isFaceCard;
        private FaceCard? faceCardType;
        private SuitType suitType;
        public int Value { get { return value; } }
        public bool IsFaceCard { get { return isFaceCard; } }
        public FaceCard? FaceCardType { get { return faceCardType; } }
        public SuitType SuitType { get { return suitType; } }
        public BitmapImage Image { get; }
        public Guid Id { get; }

        public Card(BitmapImage Image, int Value, bool IsFaceCard, SuitType SType, FaceCard? FCType = null)
        {
            value = Value;
            isFaceCard = IsFaceCard;
            faceCardType = FCType;
            suitType = SType;
            this.Image = Image;
            Id = new Guid();
        }
    }
}
