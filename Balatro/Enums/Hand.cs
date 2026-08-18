using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Cards.Balatro
{
    public enum Hand
    {
        [Description("High Card")]
        HIGH_CARD,
        [Description("Pair")]
        PAIR,
        [Description("Two Pair")]
        TWO_PAIR,
        [Description("Three of a Kind")]
        THREE_OF_A_KIND,
        [Description("Straight")]
        STRAIGHT,
        [Description("Flush")]
        FLUSH,
        [Description("Full House")]
        FULL_HOUSE,
        [Description("Four of a Kind")]
        FOUR_OF_A_KIND,
        [Description("Straight Flush")]
        STRAIGHT_FLUSH,
        [Description("Royal Flush")]
        ROYAL_FLUSH
    }
}
