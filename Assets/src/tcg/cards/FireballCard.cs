using UnityEngine;
using System.Collections.Generic;
using game.world;
using System.Linq;

namespace game.tcg.cards {
	class FireballCard : TCGCard {

		public override string GetName ()
		{
			return "Fireball";
		}
		
		public override List<Hex> ValidTargets (WorldMap wm, Hex h) {
            var targets = h.Neighbors().Where((x) => x.unit != null);
            return targets.ToList();
        }

		public override void OnPlay (WorldMap wm, Hex h) {
            if (h.unit != null) {
                h.unit.ApplyDamage(3);
            }
		}

        public override string getDescription()
        {
            return "Deal 3 damage to an adjacent enemy.";
        }
    }
}

