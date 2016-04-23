using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;

namespace game.tcg.cards {
    class DoubleActionCard : TCGCard {
        public override string GetName() {
            return "Double Action";
        }

        public override List<Hex> ValidTargets(WorldMap wm, Hex h) {
            List<Hex> tmp = new List<Hex>();
            tmp.Add(GameManager.world.hero.h);
            return tmp;
        }

        public override void OnPlay(WorldMap wm, Hex h) {
            GameManager.p.turns = 2;
        }
    }
}
