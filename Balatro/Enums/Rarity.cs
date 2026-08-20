using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Enums
{
    public enum Rarity
    {
        [Description("Common")]
        COMMON,
        [Description("Uncommon")]
        UNCOMMON,
        [Description("Rare")]
        RARE,
        [Description("Legendary")]
        LEGENDARY
    }
}
