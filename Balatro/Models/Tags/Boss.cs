using Balatro.Models.BossBlinds;
using Microsoft.UI.Xaml.Media.Imaging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Balatro.Models.Tags
{
    public class Boss : ITag
    {
        public string Name => "Boss Tag";

        public string Description => "Rerolls the Boss Blind";

        public BitmapImage Image => new BitmapImage(new Uri("ms-appx:///Assets/TagImages/Boss_Tag.png"));

        public int MinAnte => 1;

        public void ApplyEffect(Player Player)
        {
            return;
        }

        public void ApplyEffect(Shop Shop) { return; }

        public void ApplyEffect(BalatroGame Game)
        {
            IBossBlind ReAdd = Game.BossBlind;

            Game.BossBlind = Game.RollBossBlind();

            Game.BossBlinds.Add(ReAdd);
        }
    }
}
