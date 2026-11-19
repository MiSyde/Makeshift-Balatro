using Balatro.Enums;
using Microsoft.UI.Xaml;
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
    public class Red_Deck : IDeck
    {
        public List<Card> Cards { get; }
        public Visibility ButtonVisibility { get; set; }
        public int Price { get; set; }
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
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/11.png")), 10, true, SuitType.Spades, 11, FaceCard.Jack));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/12.png")), 10, true, SuitType.Spades, 12, FaceCard.Queen));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/13.png")), 10, true, SuitType.Spades, 13, FaceCard.King));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/14.png")), 10, true, SuitType.Spades, 14, FaceCard.Ace));

            for (int i = 2; i < 11; ++i)
            {
                Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/" + i + ".png")), i, false, SuitType.Hearts, i));
            }
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/11.png")), 10, true, SuitType.Hearts, 11, FaceCard.Jack));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/12.png")), 10, true, SuitType.Hearts, 12, FaceCard.Queen));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/13.png")), 10, true, SuitType.Hearts, 13, FaceCard.King));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/14.png")), 10, true, SuitType.Hearts, 14, FaceCard.Ace));

            for (int i = 2; i < 11; ++i)
            {
                Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/" + i + ".png")), i, false, SuitType.Clubs, i));
            }
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/11.png")), 10, true, SuitType.Clubs, 11, FaceCard.Jack));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/12.png")), 10, true, SuitType.Clubs, 12, FaceCard.Queen));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/13.png")), 10, true, SuitType.Clubs, 13, FaceCard.King));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/14.png")), 10, true, SuitType.Clubs, 14, FaceCard.Ace));

            for (int i = 2; i < 11; ++i)
            {
                Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/" + i + ".png")), i, false, SuitType.Diamonds, i));
            }
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/11.png")), 10, true, SuitType.Diamonds, 11, FaceCard.Jack));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/12.png")), 10, true, SuitType.Diamonds, 12, FaceCard.Queen));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/13.png")), 10, true, SuitType.Diamonds, 13, FaceCard.King));
            Cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/14.png")), 10, true, SuitType.Diamonds, 14, FaceCard.Ace));

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
