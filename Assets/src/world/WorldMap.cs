/*
 * Andrew Amis, Nick Care, Robert Tomcik (2016)
 * The WorldMap class
 *
 */
using UnityEngine;
using System.Collections.Generic;
using game.world.units;
using game.world.math;
using System;

namespace game.world {
	[System.Serializable]
	struct WorldData {
		public List<HexData> hdata;
	}

	class WorldMap {
		public Dictionary<HexLoc, Hex> map;
		public Layout l;
        public HeroUnit hero;
		GameObject hFolder;

		public WorldMap(Layout l) {
			this.l = l;

			hFolder = new GameObject ("Hexes");

			map = new Dictionary<HexLoc, Hex> ();
		}

		public Hex addHex(HexLoc hl) {
			Hex h = new GameObject ("Hex " + hl.ToString ()).AddComponent<Hex> ();
			h.init (this, hl);

			h.transform.parent = hFolder.transform;

			map.Add(hl, h);
			return h;
		}

		internal void NewTurn() {
            foreach (KeyValuePair<HexLoc, Hex> kv in map) {
                kv.Value.Updated = false;
            }
            foreach (KeyValuePair<HexLoc, Hex> kv in map) {
                if (!kv.Value.Updated) {
                    kv.Value.Updated = true;
                    kv.Value.NewTurn();
                }
            }
        }
    }
}
