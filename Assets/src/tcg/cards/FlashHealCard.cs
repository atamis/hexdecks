using UnityEngine;
using System.Collections.Generic;
using game.world;

namespace game.tcg.cards {
	class FlashHealCard : TCGCard {
		public override string GetName () {
			return "Flash Heal";
		}

		public override List<Hex> ValidTargets (WorldMap wm, Hex h) {
			List<Hex> tmp = new List<Hex> ();
			tmp.Add (wm.hero.h);
			return tmp;
		}

		public override void OnPlay (WorldMap wm, Hex h) {
			GameManager.ntm.AddText (h.transform.position, "+2");
			h.unit.health += 2;
		}
	}
}
