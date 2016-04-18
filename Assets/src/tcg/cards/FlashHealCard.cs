using UnityEngine;
using System.Collections.Generic;
using game.world;
using System;

namespace game.tcg.cards {
	class FlashHealCard : Card {

		public override List<Hex> ValidTargets(WorldMap w, Hex h) {
			List<Hex> tmp = new List<Hex> ();
			tmp.Add (GameManager.world.hero.h);
			return tmp;
		}

		public override void OnPlay (WorldMap w, Hex h) {
			GameManager.ctm.AddText (h.transform.position, "+2");
			h.unit.health += 2;
			Destroy (this);
		}

		public override bool CanPlay (WorldMap w, Hex h) {
			return false;
		}

		public override List<Hex> PreCast (Hex h, int dir) {
			return null;
		}

        public override string GetName() {
            return "Heal";
        }

        public override string GetCardText() {
            return "Heal yourself for 2";
        }
    }
}
