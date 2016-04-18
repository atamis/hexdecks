using UnityEngine;
using System.Collections.Generic;
using game.world;

namespace game.tcg.cards {
	class JumpBlockCard : Card {

		public override List<Hex> ValidTargets(WorldMap w, Hex h) {
			List<Hex> tmp = new List<Hex> ();
			foreach (Hex h2 in h.Neighbors()) {
				if (h2.Passable () && h2.unit == null) {
					tmp.Add (h2);
				}
			}
			return tmp;
		}

		public override void OnPlay (WorldMap w, Hex h) {
			w.hero.h = h;
			w.hero.invincible.duration = 1;
		}

		public override bool CanPlay (WorldMap w, Hex h) {
			return false;
		}

		public override List<Hex> PreCast (Hex h, int dir) {
			return null;
		}
	}
}

