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

		internal void refreshSprite(){
			switch (this.tileType) {
				case TileType.Normal:
					float random = Random.value;
					if(random < .1f) model.sr.sprite = Resources.Load <Sprite>("Sprites/Tiles/T_Ground2");
					else model.sr.sprite = Resources.Load <Sprite>("Sprites/Tiles/T_Ground1");
					break;
				case TileType.Wall:
				model.sr.sprite = Resources.Load <Sprite>("Sprites/Tiles/T_Forest");
					break;
				case TileType.Water:
				model.sr.sprite = Resources.Load <Sprite>("Sprites/Tiles/T_Water1");
					break;
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
			private bool colliding;

			public void init(Hex h) {
				this.h = h;
				this.tag = "Hex";
				this.gameObject.layer = LayerMask.NameToLayer("HexLayer");

				transform.localPosition = new Vector3 (h.transform.position.x, h.transform.position.y, Layer.Hex);
				transform.localScale = new Vector3 (1.9f, 1.9f, 1);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				//sr.material = new Material(Shader.Find("Sprites/Diffuse"));

				float random = Random.value;
				if(random < .25f) {
					sr.sprite = Resources.Load <Sprite>("Sprites/Tiles/T_Ground2");
				} else {
					sr.sprite = Resources.Load <Sprite>("Sprites/Tiles/T_Ground1");
				}

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
					sr.color = new Color(0.4f, 0.4f, 0.4f);
					break;
				case TileType.Water:
					sr.color = new Color(1f, 1f, 1f);
					break;
				}
			}

			public void SetColor(Color c) {
				this.sr.color = c;
			}

			public void OnMouseOver() {
				if (!colliding) {
					if (h.unit != null) {
                        if (h.unit.GetType() != typeof(HeroUnit))
                        {
                            h.unit.ShowHealth(true);
                        }
						foreach (Hex hex in h.unit.GetAttackPattern()) {
							hex.Highlight (Color.yellow);
						}
					}
				}
			}

			public void OnMouseExit() {
				if (this.h.unit != null) {
					if (this.h.unit != null) {
						this.h.unit.ShowHealth (false);
						foreach (Hex hex in h.unit.GetAttackPattern()) {
							hex.Highlight (Color.white);
						}
					}
				}
			}

			public void OnCollisionEnter2D(Collision2D coll) {
				this.colliding = true;
			}

			public void OnCollisionExit2D(Collision2D coll) {
				this.colliding = false;
			}
		}
	}
}
