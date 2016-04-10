/*
 * Andrew Amis, Nick Care, Robert Tomcik (2016)
 * The Hex Class
 *
 */

using UnityEngine;
using System.Collections;
using game.world.math;
using game.world.units;
using System;
using System.Collections.Generic;

namespace game.world {
    public enum TileType {
        Normal, Water, Wall
    }

	class Hex : MonoBehaviour {
		private HexModel model;
        private WorldMap w;
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
        public TileType tileType;

        public List<Trigger> triggers;

		public void init(WorldMap w, HexLoc loc) {
			this.loc = loc;
            this.w = w;
            tileType = TileType.Normal;
            triggers = new List<Trigger>();

			model = new GameObject ("Hex Model").AddComponent<HexModel> ();
			model.init (this);

			model.transform.parent = transform;
			transform.localPosition = GameManager.l.HexPixel (loc);
		}

		public void Select() {
			model.sr.color = Color.yellow;
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

        
        internal void NewTurn() {
            if (unit != null && unit.Updated == false) {
                unit.Updated = true;
                unit.NewTurn();
            }
        }

        private class HexModel : MonoBehaviour {
			public SpriteRenderer sr;
			Hex h;

			public void init(Hex h) {
				this.h = h;
				transform.localPosition = new Vector3 (0, 0, Layer.Board);
				transform.localScale = new Vector3 (1.9f, 1.9f, 0);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load <Sprite>("Sprites/Hexagon");

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

        }

    }
}