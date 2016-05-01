using UnityEngine;
using System.Collections.Generic;
using game.world;

namespace game.tcg.cards {
	class FlashHealCard : TCGCard {
		public override string GetName () {
			return "Flash Heal";
		}

		public override List<Hex> ValidTargets (WorldMap wm, Hex h) {
			return justHeroValid(wm, h);
		}

		public override void OnPlay (WorldMap wm, Hex h) {
			if (h.unit != null) {
                h.unit.ApplyHealing(1, wm.hero);
            }
		}

        public override string getDescription()
        {
            return "Gain 1 health.";
        }
    }
}
