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
            return justHeroValid(wm, h);
        }

        public override void OnPlay(WorldMap wm, Hex h) {
            GameManager.p.turns = 2;
        }

        public override string getDescription()
        {
            return "Gain 2 more actions this turn.";
        }
    }
}
