using UnityEngine;
using System.Collections.Generic;
using game.math;

namespace game.world {
	[System.Serializable]
	public struct WorldData {
		public List<HexData> hData;
	}

	class WorldMap {
		public Dictionary<HexLoc, Hex> map;
		private GameObject hFolder;
		private Layout l;

		public HeroUnit hero;
		public int turns { get; set; }

		public WorldMap() {
			
		}
	}
}

