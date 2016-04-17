using UnityEngine;
using System.Collections.Generic;
using game.world;
using game.world.units;

namespace game.tcg.cards {
    class FireballCard : Card {
        public override bool CanPlay(WorldMap w, Hex h) {
            if (h.unit != null) {
                return true;
            }
            return false;
        }

        public override void OnPlay(WorldMap w, Hex h) {
            if (h.unit != null) {
                h.unit.health -= 3;
            }
        }

        public override List<Hex> ValidTargets(WorldMap w, Hex h) {
            List<Hex> tmp = new List<Hex> ();
			foreach (Hex h2 in h.Neighbors()) {
				if (h2.Passable ()) {
					tmp.Add (h2);
				}
			}
            return tmp;
        }

		public override List<Hex> PreCast (Hex h) {
			List<Hex> tmp = new List<Hex> ();
			tmp.Add(GameManager.world.map[h.loc.Neighbor (0)]); 
			tmp.Add(GameManager.world.map[h.loc.Neighbor (1)]);
			tmp.Add(GameManager.world.map[h.loc.Diagonal (0)]);
			return tmp;
		}

        public Sprite GetSprite() {
            return Resources.Load<Sprite> ("Sprites/Circle");
        }
    }
}
