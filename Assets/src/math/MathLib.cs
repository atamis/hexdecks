using UnityEngine;
using System.Collections.Generic;
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


		public static int GetOrientation(HexLoc target, HexLoc origin) {
			for (int i = 0; i < 5; i++) {
				if (origin + HexLoc.hex_directions[i] == target) {
					return i;
				}
			}
			return -1;
		}
    }
}
