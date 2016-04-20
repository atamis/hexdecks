using UnityEngine;
using System.Collections.Generic;
using game.math;
using game.world.units;

namespace game.world {
	[System.Serializable]
	public struct HexData {
		public int pos;
		public UnitData uData;
	}

	public enum TileType {
		Normal, Water, Wall
	}

	class Hex : MonoBehaviour {
		private HexModel model;
		public TileType tileType;

		private bool _updated;
			
		public HexLoc loc { get; set; }
		private Unit _unit;

		public void init() {

		}

		internal bool Passable() {
			switch(tileType) {
			case TileType.Normal:
				return true;
			case TileType.Wall:
			case TileType.Water:
			default:
				return false;
			}
		}

		private class HexModel : MonoBehaviour {
			public SpriteRenderer sr;
			private Hex h;

			public void init(Hex h) {
				this.h = h;
				transform.localPosition = new Vector3 (0, 0, Layer.Board);
				transform.localScale = new Vector3 (1.9f, 1.9f, 0);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load <Sprite>("Sprites/Tiles/T_Ground");
			}
		}
	}
}

