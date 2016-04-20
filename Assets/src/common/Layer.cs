using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game {
	// GUI is its own magic unity layer.

	class Layer {
		public readonly static int Board = 0;
		public readonly static int Miasma = -1;
		public readonly static int EnemyUnits = -2;
		public readonly static int HeroUnit = -3;
		public readonly static int UnitsFX = -4;
		public readonly static int BuildingFX = -5;
		public readonly static int PseudoUI = -6;
	}

	class LayerV {
		public readonly static Vector3 Board = new Vector3(0, 0, Layer.Board);
		public readonly static Vector3 Miasma = new Vector3(0, 0, Layer.Miasma);
		public readonly static Vector3 EnemyUnits = new Vector3(0, 0, Layer.EnemyUnits);
		public readonly static Vector3 HeroUnit = new Vector3(0, 0, Layer.HeroUnit);
		public readonly static Vector3 UnitsFX = new Vector3(0, 0, Layer.UnitsFX);
		public readonly static Vector3 BuildingFX = new Vector3(0, 0, Layer.BuildingFX);
		public readonly static Vector3 PseudoUI = new Vector3(0, 0, Layer.PseudoUI);
	}
}
