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
	class WorldMap {
		GameObject hFolder; GameObject uFolder;
		public Dictionary<HexLoc, Hex> map;
		public Layout l;
        public HeroUnit hero;
        public GameManager gm;

		// TODO
		// Hex Shaped Map

		public WorldMap(Layout l, GameManager gm) {
			this.l = l;
            this.gm = gm;

			hFolder = new GameObject ("Hex Map");
			map = new Dictionary<HexLoc, Hex> ();

		}

		public Hex addHex(HexLoc hl) {
			Hex h = new GameObject ("Hex" + hl.ToString()).AddComponent<Hex> ();
			h.init (this, hl);

			h.transform.parent = hFolder.transform;

			map.Add (hl, h);

            return h;
		}

		public void deleteHex(HexLoc hl) {
			// TODO
		}

		public void deleteUnit() {
			// TODO
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
