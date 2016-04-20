using UnityEngine;
using System.Collections;
using game.world;

namespace game.math {
	static class MathLib {
		public static Hex GetHexAtMouse() {
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			HexLoc l = GameManager.l.PixelHex(worldPos);
			if (GameManager.world.map.ContainsKey(l)) {
				Hex h = GameManager.world.map[l];
				return h;
			}
			return null;
		}
	}
}

