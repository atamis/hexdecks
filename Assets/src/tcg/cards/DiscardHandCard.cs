using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;

namespace game.tcg.cards {
    class DiscardHandCard : TCGCard {
        public override string GetName() {
            return "Discard Hand";
        }

        public override void OnPlay(WorldMap wm, Hex h) {
            GameManager.p.DiscardHand();
            GameManager.p.DrawCards(5);
        }

        public override List<Hex> ValidTargets(WorldMap wm, Hex h) {
            return justHeroValid(wm, h);
        }
    }
}
