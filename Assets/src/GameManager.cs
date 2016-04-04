using UnityEngine;
using System.Collections;
using game.world;
using game.world.math;
using game.world.units;
using game.ui;
using game.tcg;
/*
 * Andrew Amis, Nick Care, Robert Tomcik (2016)
 * The main HexDecks manager, references to the scenes should direct here
 *
 */

namespace game {
	enum GameState {
		Default,
		Selected,
	}

	class GameManager : MonoBehaviour {
		public static WorldMap map;
		public static Layout l;

		GameCamera gc;
		bool battling;
		GameState state;
		Hex selected;

		public Player player;

		void Awake() {
			gc = new GameObject ("Game Camera").AddComponent<GameCamera> ();
			gc.init (Camera.main);

			l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));
			map = new WorldMap (l);

			map.addUnit (new HeroUnit("Tim"), new HexLoc (0, 0));

			//this.selected = null;
		}
			
		public void MakeDuel() {
			// TODO
		}

		public Hex GetHexAtMouse() {
			Vector3 worldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			HexLoc l = map.l.PixelHex (worldPos);
			if (map.hexes.ContainsKey (l)) {
				Hex h = map.hexes [l];
				return h;
			}
			return null;
		}

		// Clamp down the duel arena
		public void ClampArena() {

		}

		void Update() {
			if (selected != null) {
				selected.Select ();
			}

			if (Input.GetMouseButtonUp (0)) {
				switch (state) {
				case GameState.Default:
					this.selected = GetHexAtMouse ();
					if (selected != null) {
						state = GameState.Selected;
					} else {
						state = GameState.Default;
					}
					break;

				case GameState.Selected:
					this.selected = GetHexAtMouse ();
					if (selected != null) {
						state = GameState.Selected;
					} else {
						state = GameState.Default;
					}
					break;

				}
			} else if (Input.GetMouseButton (1)) {
				switch (state) {
				case GameState.Default:
					break;

				case GameState.Selected:
					Hex h = GetHexAtMouse ();
					h.unit = selected.unit;
					selected.unit = null;
					break;

				}
			}
		}

		// Drag and drog logic
		private class DragTest : MonoBehaviour {
			Vector3 screenPoint;
			Vector3 offset;

			void OnMouseDown() {
				screenPoint = Camera.main.WorldToScreenPoint (gameObject.transform.position);
				offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(
					Input.mousePosition.x, 
					Input.mousePosition.y, 
					screenPoint.z));
			}

			void OnMouseDrag() {
				Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);

				Vector3 curPosition   = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset;
				transform.position = curPosition;
			}
		}
	}
}