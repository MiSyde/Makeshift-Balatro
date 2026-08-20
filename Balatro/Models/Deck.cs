using Balatro.Enums;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Balatro.Models
{
    public class Deck
    {
        private List<Card> cards;
        private int currentSize;
        public List<Card> Cards { get { return cards; } }
        public int CurrentSize { get { return currentSize; }  }

        public Deck()
        {
            currentSize = 52;
            cards = new List<Card>();
            for (int i = 2; i < 11; ++i)
            {
                cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/" + i + ".png")), i, false, SuitType.Spades));
            }
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/Jack.png")), 10, true, SuitType.Spades, FaceCard.Jack));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/Queen.png")), 10, true, SuitType.Spades, FaceCard.Queen));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/King.png")), 10, true, SuitType.Spades, FaceCard.King));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Spades/Ace.png")), 10, true, SuitType.Spades, FaceCard.Ace));

            for (int i = 2; i < 11; ++i)
            {
                cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/" + i + ".png")), i, false, SuitType.Hearts));
            }
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/Jack.png")), 10, true, SuitType.Hearts, FaceCard.Jack));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/Queen.png")), 10, true, SuitType.Hearts, FaceCard.Queen));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/King.png")), 10, true, SuitType.Hearts, FaceCard.King));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Hearts/Ace.png")), 10, true, SuitType.Hearts, FaceCard.Ace));

            for (int i = 2; i < 11; ++i)
            {
                cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/" + i + ".png")), i, false, SuitType.Clubs));
            }
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/Jack.png")), 10, true, SuitType.Clubs, FaceCard.Jack));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/Queen.png")), 10, true, SuitType.Clubs, FaceCard.Queen));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/King.png")), 10, true, SuitType.Clubs, FaceCard.King));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Clubs/Ace.png")), 10, true, SuitType.Clubs, FaceCard.Ace));

            for (int i = 2; i < 11; ++i)
            {
                cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/" + i + ".png")), i, false, SuitType.Diamonds));
            }
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/Jack.png")), 10, true, SuitType.Diamonds, FaceCard.Jack));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/Queen.png")), 10, true, SuitType.Diamonds, FaceCard.Queen));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/King.png")), 10, true, SuitType.Diamonds, FaceCard.King));
            cards.Add(new Card(new BitmapImage(new Uri("ms-appx:///Assets/CardImages/Diamonds/Ace.png")), 10, true, SuitType.Diamonds, FaceCard.Ace));
        }
        public void Remove(Card c)
        {
            --currentSize;
            cards.Remove(c);
        }
    }

}
