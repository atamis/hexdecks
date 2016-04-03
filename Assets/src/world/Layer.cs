using UnityEngine;
using System.Collections;

namespace game.world {
	class Layer {
		public readonly static int Board = 0;
		public readonly static int Unit = -1;
	}

	class LayerV {
		public readonly static Vector3 Board = new Vector3(0, 0, Layer.Board);
		public readonly static Vector3 Unit = new Vector3(0, 0, Layer.Unit);
	}
}
