using UnityEngine;
using System.Collections.Generic;
using game.world;
using game.math;
using System;

namespace game.tcg.cards {
	class JumpAttackCard : TCGCard {

		public override List<Hex> ValidTargets (WorldMap w, Hex h) {
			List<Hex> targets = new List<Hex>(HexLoc.hex_directions.Length);

            var origin = w.hero.h;

            foreach (KeyValuePair<HexLoc, Hex> kv in w.map) {
                var loc = kv.Key;
                var hex = kv.Value;

                if (loc.Distance(h.loc) > 2 || loc.Distance(h.loc) == 0) continue;

                if (!hex.Passable()) continue;

                if (hex.unit != null) continue;

                var mid = origin.loc + (hex.loc - origin.loc).Normalize();
                //UnityEngine.Debug.Log((hex.loc - origin.loc) + ", " + (hex.loc - origin.loc).Normalize() + ", " + mid);

                if (w.map.ContainsKey(mid) && w.map[mid].tileType == TileType.Wall) continue;

                targets.Add(hex);
            }

			return targets;
		}

		public override string GetName () {
			return "Jump";
		}

		public override void OnPlay (WorldMap w, Hex h) {
            if (h.unit != null) {
                throw new Exception("Can't jump on top of unit");
            }

            if (!h.Passable()) {
                throw new Exception("Can't land there");
            }

            w.hero.h = h;

            Combo();
        }

        public override string getDescription()
        {
            return "Jump to an unoccupied hex and gain an extra action.";
        }
    }
}
