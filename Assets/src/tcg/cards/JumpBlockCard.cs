using UnityEngine;
using System.Collections.Generic;
using game.world;

namespace game.tcg.cards {
	class JumpBlockCard : Card {
		public override string GetName () {
			return "Jump Block";
		}

		public override string GetCardText () {
			return "Jump forward and become invicible until end of turn";
		}

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
        
	}
}

