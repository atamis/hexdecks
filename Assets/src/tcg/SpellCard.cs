using UnityEngine;
using System.Collections;
using game.world;
using game.world.math;

namespace game.tcg {
	
	class SpellCard : Card {
		public SpellCard() : base() {

		}

		public override void OnPlay(HexLoc hl) {
			
		}

		public override bool ValidTarget(Hex h) {
			if (h.unit == null) {
				return false;
			}
			return false;
		}
	}
}

