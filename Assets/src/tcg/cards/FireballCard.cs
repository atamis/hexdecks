﻿using UnityEngine;
using System.Collections.Generic;
using game.world;
using game.math;
using System.Linq;

namespace game.tcg.cards {
	class FireballCard : TCGCard {

		public override string GetName ()
		{
			return "Fireball";
		}

		public override List<Hex> ValidTargets (WorldMap w, Hex h) {
			List<Hex> targets = new List<Hex>(HexLoc.hex_directions.Length);

						var origin = w.hero.h;

						foreach (KeyValuePair<HexLoc, Hex> kv in w.map) {
								var loc = kv.Key;
								var hex = kv.Value;

								if (loc.Distance(h.loc) > 2 || loc.Distance(h.loc) == 0) continue;

								if (!hex.Passable()) continue;

								if (hex.unit == null) continue;

								var mid = origin.loc + (hex.loc - origin.loc).Normalize();
								//UnityEngine.Debug.Log((hex.loc - origin.loc) + ", " + (hex.loc - origin.loc).Normalize() + ", " + mid);

								if (w.map.ContainsKey(mid) && w.map[mid].tileType == TileType.Wall) continue;

								targets.Add(hex);
						}

						return targets;
			/*
            var targets = h.Neighbors().Where((x) => x.unit != null);
            return targets.ToList(); */
        }

		public override void OnPlay (WorldMap wm, Hex h) {
            if (h.unit != null) {
                h.unit.ApplyDamage(3, wm.hero);
            }
		}

        public override string getDescription()
        {
            return "Deal 3 damage to an enemy up to 2 hexes away.";
        }
    }
}
