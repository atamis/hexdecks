using UnityEngine;
using System.Collections.Generic;
using game.world;
using game.math;
using System;

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

                if (!h2.Passable()) {
                    // Can't jump into walls
                    continue;
                }

                if (h1.tileType == TileType.Wall) {
                    // Can't jump over walls
                    continue;
                }

                if (h2.unit != null && !h1.Passable() && h2.unit.health != 1) {
                    // If we can't immediately kill the enemy, and we would land in water
                    continue;
                }

                targets.Add(h2);

			}

			return targets;
		}

		public override string GetName () {
			return "Jump Attack";
		}
			
		public override void OnPlay (WorldMap w, Hex h) {
			if (h.unit != null) {
				h.unit.ApplyDamage(1, w.hero);
			}

			if (h.unit != null) {
				var origin = w.hero.h;
				var dest = origin.loc + (h.loc - origin.loc).Normalize();

                //UnityEngine.Debug.Log(dest + " normalized: " + dest.Normalize());

                if (!w.map.ContainsKey(dest)) {
					throw new System.Exception("Attempted to jump over a nonexistant tile");
				}

                var dest_h = w.map[dest];

                if (!dest_h.Passable()) {
                    throw new Exception("Would have to land an impassable tile");
                }

				w.hero.h = w.map[dest];
			} else {
				w.hero.h = h;
			}
		}

        public override string getDescription()
        {
            return "Attack an enemy two hexes away in a straight line, or jump over a water hex.";
        }
    }
}