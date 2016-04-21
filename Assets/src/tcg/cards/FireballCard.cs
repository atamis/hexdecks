using UnityEngine;
using System.Collections.Generic;
using game.world;

namespace game.tcg.cards {
	class FireballCard : TCGCard {

		public override string GetName ()
		{
			return "Fireball";
		}
		
		public override List<Hex> ValidTargets (WorldMap wm, Hex h) {
			List<Hex> tmp = new List<Hex> ();
			foreach (Hex h2 in h.Neighbors()) {
				if (h2.Passable ()) {
					tmp.Add (h2);
				}
			}
			return tmp;
		}

		public override void OnPlay (WorldMap wm, Hex h) {
			foreach (Hex h2 in h.Neighbors()) {
				if (h.unit != null) {
					h.unit.ApplyDamage (1);
				}
			}
		}
	}
}

