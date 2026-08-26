using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Models
{
    public enum Enhancement
    {
        [Description("Enhanced card gives an additional +30 Chips when scored")]
        BONUS_CHIPS,
        [Description("Enhanced card gives +4 Mult when scored")]
        BONUS_MULT,
        [Description("Enhanced card is considered to be every suit simultaneously")]
        WILD_CARD,
        [Description("Enhanced card gives X2 Mult when scored\r\n1 in 4 chance to destroy card after all scoring is finished")]
        GLASS_CARD,
        [Description("Enhanced card gives X1.5 Mult while held in hand")]
        STEEL_CARD,
        [Description("Enhanced card's value is set to +50 Chips\r\nEnhanced card has no rank or suit\r\nEnhanced card always scores")]
        STONE_CARD,
        [Description("Enhanced card gives $3 if held in hand at end of round")]
        GOLD_CARD,
        [Description("Enhanced card has a 1 in 5 chance to give +20 Mult\r\nEnhanced card has a 1 in 15 chance to give $20\r\n(Both effects are rolled separately, and both can trigger on the same turn) ")]
        LUCKY_CARD
    }
}
