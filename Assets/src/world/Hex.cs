using UnityEngine;
using System.Collections;
using game.world.math;
using game.world.units;
using System;
using System.Collections.Generic;
/*
 * Andrew Amis, Nick Care, Robert Tomcik (2016)
 * The Hex Class
 *
 */

namespace game.world {
    [System.Serializable]
	public struct HexData {
		public UnitData u;
	}

    public enum TileType {
        Normal, Water, Wall
    }

	class Hex : MonoBehaviour {
        private HexModel model;
        public TileType tileType;
		private WorldMap w;

        public HexLoc loc { get; set; }
        public Unit unit { get; set; }

        internal WorldPathfinding.PathfindingInfo pathfind;
        private bool _updated;
        internal bool Updated {
            get {
                return _updated;
            }
            set {
                _updated = value;
                if (unit != null && value == false) {
                    unit.Updated = false;
                }
            }
        }

		public void init(WorldMap w, HexLoc loc) {
            this.loc = loc;
            this.w = w;
			this.transform.position = GameManager.l.HexPixel (loc);

            tileType = TileType.Normal;

            model = new GameObject ("Hex Model").AddComponent<HexModel> ();
			model.init (this);

			model.transform.parent = transform;
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

        internal void NewTurn() {
            if (unit != null && unit.Updated == false) {
                unit.Updated = true;
                unit.NewTurn();
            }
        }

        public List<Hex> Neighbors() {
            List<Hex> n = new List<Hex>();

            for (int i = 0; i < 6; i++) {
                HexLoc l = loc.Neighbor(i);
                if (w.map.ContainsKey(l)) {
                    n.Add(w.map[l]);
                }
            }

            return n;
        }

		public void Highlight(Color c) {
			this.model.sr.color = c;
		}

        public override string ToString() {
            return "Hex " + loc.ToString();
        }

        private class HexModel : MonoBehaviour {
			public SpriteRenderer sr;
			private PolygonCollider2D coll;
			private Rigidbody2D rig;
            private Hex h;

            public void init(Hex h) {
				this.h = h;
				this.tag = "Hex";

				//gameObject.hideFlags = HideFlags.HideInHierarchy; // hide from heirarchy

				transform.localPosition = new Vector3 (h.transform.position.x, h.transform.position.y, Layer.Board);
				transform.localScale = new Vector3 (1.9f, 1.9f, 0);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load <Sprite>("Sprites/Hexagon");
				sr.material = new Material (Shader.Find ("Sprites/Default"));

				coll = gameObject.AddComponent<PolygonCollider2D> ();
				coll.isTrigger = true;

				rig = gameObject.AddComponent<Rigidbody2D> ();
				rig.gravityScale = 0.0f;
            }

            void Update() {
                switch (h.tileType) {
                    case TileType.Wall:
                        sr.color = new Color(0.2f, 0.2f, 0.2f);
                        break;
                    case TileType.Water:
                        sr.color = new Color(0, 0, 0.5f);
                        break;
                }
            }

			public void SetColor(Color c) {
				this.sr.color = c;
			}
        }
    }
}
