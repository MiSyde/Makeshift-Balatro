using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Balatro.Enums
{
    internal enum Rarity
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
