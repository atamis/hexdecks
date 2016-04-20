using UnityEngine;
using game.world;
using System;

namespace game.tcg.cards {
	class JumpAttackCard : TCGCard {
		public override void OnPlay ()
		{
			throw new NotImplementedException ();
		}

		public override System.Collections.Generic.List<Hex> ValidTargets ()
		{
			throw new NotImplementedException ();
		}

		public override string GetTitle ()
		{
			throw new NotImplementedException ();
		}

		/*
		public override void OnPlay (WorldMap w, Hex h) {
			if (h.unit != null) {
				h.unit.ApplyDamage(1);
			}

			if (h.unit != null) {
				var origin = w.hero.h;
				var dest = origin.loc - h.loc;
				if (!w.map.ContainsKey(dest)) {
					throw new Exception("Attempted to jump over a nonexistant tile");
				}

				w.hero.h = w.map[dest];
			} else {
				w.hero.h = h;
			}
		}
		*/
	}
}