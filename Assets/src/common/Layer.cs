using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game {
	// GUI is its own magic unity layer.

	class Layer {
		public readonly static int Hex = 0;
		public readonly static int HeroUnit = -1;
		public readonly static int EnemyUnit = -2;
		public readonly static int UnitsFX = -3;
		public readonly static int Card = -4;
		public readonly static int HUD = -5;
		public readonly static int HUDFX = -6;
		public readonly static int Tooltip = -6;
	}

	class LayerV {
		public readonly static Vector3 Hex = new Vector3(0, 0, Layer.Hex);
		public readonly static Vector3 HeroUnit = new Vector3(0, 0, Layer.HeroUnit);
		public readonly static Vector3 EnemyUnit = new Vector3(0, 0, Layer.EnemyUnit);
		public readonly static Vector3 UnitFX = new Vector3(0, 0, Layer.UnitsFX);
		public readonly static Vector3 Card = new Vector3(0, 0, Layer.Card);
		public readonly static Vector3 HUD = new Vector3(0, 0, Layer.HUD);
		public readonly static Vector3 HUDFX = new Vector3(0, 0, Layer.HUDFX);
		public readonly static Vector3 Tooltip = new Vector3(0, 0, Layer.Tooltip);
	}
}
