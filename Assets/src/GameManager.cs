using UnityEngine;
using System.Collections;
using game.world;
using game.world.math;
using game.ui;
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

		void Awake() {
			gc = new GameObject ("Game Camera").AddComponent<GameCamera> ();
			gc.init (Camera.main);

			l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));
			map = new WorldMap (l);
			//map.addUnit (new HexLoc (0, 0));

			//this.selected = null;
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

		public void ClampArena() {

		}

		void Update() {
			/*
			if (Input.GetMouseButtonUp (0)) {
				switch (state) {
				case GameState.Default:
					this.selected = GetHexAtMouse ();
					//selected.Select ();
					break;

				case GameState.Selected:
					this.selected = GetHexAtMouse ();
					//selected.Select ();
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
			*/
		}
	}
}