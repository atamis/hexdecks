using game.tcg;
using game.world;
using game.world.math;
using game.world.units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/*
 * TODO
 * Card Locations
 * 
 */

namespace game.ui {
	public enum GameState {
		Default,
		Selected,
	}

	class UIManager : MonoBehaviour {
		private List<GameObject> objs;

		void Awake() {
			objs = new List<GameObject> ();

			for (int i = 0; i < 3; i++) {
				var obj = GameObject.CreatePrimitive (PrimitiveType.Quad);
				objs.Add (obj);
			}
		}

		public Hex GetHexAtMouse() {
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			HexLoc l = GameManager.l.PixelHex(worldPos);
			if (GameManager.world.map.ContainsKey(l)) {
				Hex h = GameManager.world.map[l];
				return h;
			}
			return null;
		}


		void Update() {
			// Update card positions
			foreach (GameObject o in objs) {
				o.transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (100, 100, 10));
			}

			if (Input.GetMouseButtonUp(0)) {
				Hex h = GetHexAtMouse();
				if (h != null) {
					GameManager.p.nextCommand = new MoveCommand(GameManager.p.hero, h);
				}
			}
		}
	}
}
