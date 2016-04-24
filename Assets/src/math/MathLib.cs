using UnityEngine;
using System.Collections;
using game.world;

namespace game.math {
	static class MathLib {
		public static Hex GetHexAtMouse(WorldMap wm) {
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			HexLoc l = GameManager.l.PixelHex(worldPos);
			if (wm.map.ContainsKey(l)) {
				Hex h = wm.map[l];
				return h;
			}
			return null;
		}
	}
}

