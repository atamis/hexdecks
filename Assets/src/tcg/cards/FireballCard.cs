using UnityEngine;
using System.Collections.Generic;
using game.world;
using System;

namespace game.tcg.cards {
	class FireballCard : Card {
		public override bool CanPlay(WorldMap w, Hex h) {
			if (h.unit != null) {
				return true;
			}
			return false;
		}

		public override void OnPlay(WorldMap w, Hex h) {
			foreach (Hex h2 in h.Neighbors()) {
				if (h.unit != null) {
					h.unit.ApplyDamage (1);
				}
			}
		}

		public override List<Hex> ValidTargets(WorldMap wm, Hex h) {
			List<Hex> tmp = new List<Hex> ();
			foreach (Hex h2 in h.Neighbors()) {
				if (h2.Passable ()) {
					tmp.Add (h2);
				}
			}
			return tmp;
		}

        public override string GetName() {
            return "Fireball";
        }

        public override string GetCardText() {
            return "Deal 3 damage to an enemy";
        }
    }
}
