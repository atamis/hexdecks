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

		// TODO
		// Hex Shaped Map

		public WorldMap(Layout l) {
			this.l = l;

			hFolder = new GameObject ("Hex Map");
			map = new Dictionary<HexLoc, Hex> ();

			// create the map
			for (int i = 0; i < 20; i++) {
				for (int j = 0; j < 20; j++) {
					addHex (new HexLoc (i, j));
				}
			}
		}

		public void addHex(HexLoc hl) {
			Hex h = new GameObject ("Hex" + hl.ToString()).AddComponent<Hex> ();
			h.init (hl);

			h.transform.parent = hFolder.transform;

			map.Add (hl, h);
		}

		public void deleteHex(HexLoc hl) {
			// TODO
		}

		public void deleteUnit() {
			// TODO
		}

        internal void NewTurn() {
            foreach(KeyValuePair<HexLoc, Hex> kv in map) {
                kv.Value.NewTurn();
            }
        }
    }
}
