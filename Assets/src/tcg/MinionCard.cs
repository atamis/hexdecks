using UnityEngine;
using System.Collections;
using game.world;
using game.world.math;
using game.world.units;

namespace game.tcg {

	class MinionCard : Card {
		
		public MinionCard() : base() {

		}

		public override bool ValidTarget(Hex h) {
			if (h.unit != null) {
				Debug.Log ("There's already a unit there!");
				return false;
			}
			/*
			else if (p.loc.Distance(h.loc) > 2) { // if the card is outside the player's radius ... 
				Debug.Log ("You can't cast a spell that far away!");
				return false;
			}
			*/
			return true;
		}

		public override void OnPlay(HexLoc hl) {
			GameManager.map.addUnit (new MinionUnit ("", 5), hl);
		}

		/*
		public override void Destroy() {

		}
		*/

	}
}