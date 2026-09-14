using Balatro.Enums;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balatro.Models.Decks
{
    public class Red_Deck : IDeck, INotifyPropertyChanged
    {
        public List<Card> Cards { get; }
        public int CurrentSize { get; set; }
        public int MaxSize { get; set; }
        public string Name => "Red Deck";
        public string Description => "Gives +1 discard";

        public BitmapImage Image { get; }

        public Red_Deck()
        {
            CurrentSize = 52;
            MaxSize = 52;
            Cards = new List<Card>();
            for (int i = 2; i < 11; ++i)
            {
                Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/" + i + ".png")), i, false, SuitType.Spades, i));
            }
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/Jack.png")), 10, true, SuitType.Spades, 11, FaceCard.Jack));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/Queen.png")), 10, true, SuitType.Spades, 12, FaceCard.Queen));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/King.png")), 10, true, SuitType.Spades, 13, FaceCard.King));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/Ace.png")), 10, true, SuitType.Spades, 14, FaceCard.Ace));

            for (int i = 2; i < 11; ++i)
            {
                Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/" + i + ".png")), i, false, SuitType.Hearts, i));
            }
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/Jack.png")), 10, true, SuitType.Hearts, 11, FaceCard.Jack));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/Queen.png")), 10, true, SuitType.Hearts, 12, FaceCard.Queen));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/King.png")), 10, true, SuitType.Hearts, 13, FaceCard.King));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/Ace.png")), 10, true, SuitType.Hearts, 14, FaceCard.Ace));

            for (int i = 2; i < 11; ++i)
            {
                Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/" + i + ".png")), i, false, SuitType.Clubs, i));
            }
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/Jack.png")), 10, true, SuitType.Clubs, 11, FaceCard.Jack));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/Queen.png")), 10, true, SuitType.Clubs, 12, FaceCard.Queen));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/King.png")), 10, true, SuitType.Clubs, 13, FaceCard.King));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/Ace.png")), 10, true, SuitType.Clubs, 14, FaceCard.Ace));

            for (int i = 2; i < 11; ++i)
            {
                Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/" + i + ".png")), i, false, SuitType.Diamonds, i));
            }
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/Jack.png")), 10, true, SuitType.Diamonds, 11, FaceCard.Jack));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/Queen.png")), 10, true, SuitType.Diamonds, 12, FaceCard.Queen));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/King.png")), 10, true, SuitType.Diamonds, 13, FaceCard.King));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/Ace.png")), 10, true, SuitType.Diamonds, 14, FaceCard.Ace));

            Image = new BitmapImage(new Uri("ms-appx:///Assets/DeckImages/Red_Deck.png"));
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void Remove(Card c)
        {
            --CurrentSize;
            Cards.Remove(c);
        }

        public void AddEffect(Player Player)
        {
            ++Player.Discards;
        }
    }

}
