using UnityEngine;
using System.Collections.Generic;
using game.world;
using game.math;

namespace game.tcg.cards {
	class JumpAttackCard : TCGCard {

		public override List<Hex> ValidTargets (WorldMap w, Hex h) {
			List<Hex> targets = new List<Hex>(HexLoc.hex_directions.Length);

			foreach (HexLoc dir in HexLoc.hex_directions) {
				var loc1 = h.loc + dir;
				var loc2 = loc1 + dir;

				if (!w.map.ContainsKey(loc1) || !w.map.ContainsKey(loc2)) {
					continue;
				}
				var h1 = w.map[loc1];
				var h2 = w.map[loc2];

				if (h1.Passable() && h2.unit != null) {
					targets.Add(h2);
				}

				if (h1.tileType == TileType.Water && h2.unit == null) {
					targets.Add(h2);
				}

			}

			return targets;
		}

		public override string GetName () {
			return "Jump Attack";
		}
			
		public override void OnPlay (WorldMap w, Hex h) {
			if (h.unit != null) {
				h.unit.ApplyDamage(1);
			}

			if (h.unit != null) {
				var origin = w.hero.h;
				var dest = origin.loc - h.loc;
				if (!w.map.ContainsKey(dest)) {
					throw new System.Exception("Attempted to jump over a nonexistant tile");
				}

				w.hero.h = w.map[dest];
			} else {
				w.hero.h = h;
			}
		}
	}
}