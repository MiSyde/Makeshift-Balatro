using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Models.Seals
{
    public enum Seal
    {
        [Description("Earn $3 when this card is played and scores")]
        GOLD_SEAL,
        [Description("Retrigger this card 1 time")]
        RED_SEAL,
        [Description("Creates the Planet card for final played poker hand of round if held in hand (Must have room)")]
        BLUE_SEAL,
        [Description("Creates a Tarot card when discarded (Must have room)")]
        PURPLE_SEAL
    }
}
