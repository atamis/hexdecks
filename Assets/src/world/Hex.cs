using UnityEngine;
using System.Collections.Generic;
using game.world.units;
using game.math;

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
        public List<Trigger> triggers;
		private bool selected { get; set; }

        public HexLoc loc { get; set; }
        private Unit _unit;
		public Unit unit {
            get {
                return _unit;
            }
            set {
                if (_unit == null && value != null) {
                    foreach (Trigger t in triggers) {
                        t.UnitEnter(value);
                    }
                }

                if (_unit != null && value == null) {
                    foreach (Trigger t in triggers) {
                        t.UnitLeave(unit);
                    }
                }

                _unit = value;

            }
        }

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
            triggers = new List<Trigger>();
            selected = false;

            model = new GameObject ("Hex Model").AddComponent<HexModel> ();
			model.init (this);

			model.transform.parent = transform;
			transform.localPosition = GameManager.l.HexPixel (loc);
		}

		public void Select() {
            selected = true;
		}

        public void Deselect() {
            selected = false;
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

		public override string ToString() {
			return "Hex " + loc.ToString();
		}

		public void Highlight(Color c) {
			this.model.sr.color = c;
		}

        public class HexModel : MonoBehaviour {
			public SpriteRenderer sr;
			private PolygonCollider2D coll;
			private Rigidbody2D rig;
            private Hex h;

            public void init(Hex h) {
				this.h = h;
				this.tag = "Hex";
				this.gameObject.layer = LayerMask.NameToLayer("HexLayer");

				//gameObject.hideFlags = HideFlags.HideInHierarchy; // hide from heirarchy

				transform.localPosition = new Vector3 (h.transform.position.x, h.transform.position.y, Layer.Board);
				transform.localScale = new Vector3 (1.9f, 1.9f, 0);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load <Sprite>("Sprites/Tiles/T_Ground");
				sr.material = new Material (Shader.Find ("Sprites/Default"));

				coll = gameObject.AddComponent<PolygonCollider2D> ();
				coll.isTrigger = true;

				rig = gameObject.AddComponent<Rigidbody2D> ();
				rig.gravityScale = 0.0f;
            }

            void Update() {
                switch (h.tileType) {
					case TileType.Normal:
					if (Input.GetKeyDown (KeyCode.LeftShift)) {
						if (h.selected) {
							sr.color = Color.yellow;
						} else {
							sr.color = Color.white;
							}
						}
						break;
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
