﻿using UnityEngine;
using System.Collections;
using game.world.math;
using game.world.units;
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

	class Hex {
		private HexModel model;
		private bool highlighted;

		public HexLoc loc { get; set; }
		public Unit unit { get; set; }

		public Hex(HexLoc loc) {
			this.loc = loc;

			model = new GameObject ("Hex Model").AddComponent<HexModel> ();
			model.init (this);
		}

		public override string ToString() {
			return "Hex " + loc.ToString();
		}

		private class HexModel : MonoBehaviour {
			private SpriteRenderer sr;
			private Hex h;
			private BoxCollider2D coll;

			public void init(Hex h) {
				this.h = h;

				gameObject.hideFlags = HideFlags.HideInHierarchy; // hide from heirarchy

				Vector3 tmp = GameManager.l.HexPixel (h.loc);
				transform.position = new Vector3 (tmp.x, tmp.y, Layer.Board);

				transform.localScale = new Vector3 (1.9f, 1.9f, 0);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load <Sprite>("Sprites/Hexagon");
				sr.material = new Material (Shader.Find ("Sprites/Default"));

				coll = gameObject.AddComponent<BoxCollider2D> ();
				coll.isTrigger = true;
			}

			void Update() {

			}

			void OnMouseEnter() {
				switch (GameState) {
				default:
					break;
				}
				sr.color = Color.red;
				//Debug.Log ("Entered Hex");
			}

			void OnMouseExit() {
				sr.color = Color.white;
				//Debug.Log ("Exited Hex");
			}

			void OnMouseDown() {
				switch (GameState) {
				case GameState.Default:
					break;
				default:
					break;
				}
				if (GameManager.selected != null) {
					GameManager.selected.OnPlay ();
				}
				//Debug.Log ("Clicked Hex");
			}
		}
	}
}