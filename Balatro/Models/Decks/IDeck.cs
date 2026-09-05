using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Decks
{
    public interface IDeck : IEffect
    {
        public List<Card> Cards { get; }
        public int CurrentSize { get; set; }
        public int MaxSize { get; set; }

        public abstract void Remove(Card c);
    }
}
