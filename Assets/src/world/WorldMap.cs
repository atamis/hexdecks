/*
 * Andrew Amis, Nick Care, Robert Tomcik (2016)
 * The WorldMap class
 *
 */
using UnityEngine;
using System.Collections.Generic;
using game.world.units;
using game.world.math;

namespace game.world {
	[System.Serializable]
	struct WorldData {
		public List<HexData> hdata;
	}

	class WorldMap {
		public Dictionary<HexLoc, Hex> map;
		public Layout l;

		public WorldMap(Layout l) {
			this.l = l;

			map = new Dictionary<HexLoc, Hex> ();
			for (int i = 0; i < 20; i++) {
				for (int j = 0; j < 20; j++) {
					addHex (new HexLoc (i, j));
				}
			}
		}

		public void addHex(HexLoc hl) {
			Hex h = new Hex (hl);
			map.Add (hl, h);
		}
	}
}
