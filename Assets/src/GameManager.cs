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
        public static UIManager ui;
        public static CombatTextManager ctm;

		GameCamera gc;
		bool battling;
		GameState state;
		Hex selected;
        AudioSource audioS;

		public Player player;

		void Awake() {
			gc = new GameObject ("Game Camera").AddComponent<GameCamera> ();
			gc.init (Camera.main);

            ctm = new GameObject("Combat Text").AddComponent<CombatTextManager>();

			l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));
            map = LevelLoader.LoadLevel(l, "level1", this);

            var trigger = new GameObject("Trigger").AddComponent<LogTrigger>();
            trigger.init(map.map[new HexLoc(2, 2)]);

            var hero = map.hero;
            gc.setLocation(l.HexPixel(map.hero.h.loc));

            player = new Player(hero);

            ui = gameObject.AddComponent<UIManager>();
            ui.init(map, player);

            audioS = gameObject.AddComponent<AudioSource>();
            audioS.spatialBlend = 0.0f;
            //this.selected = null;
        }
			
		public void MakeDuel() {
			// TODO
		}

		// Clamp down the duel arena
		public void ClampArena() {

		}

		void Update() {
            if (player.nextCommand != null) {
                print("Executing command " + player.nextCommand);

                // We need to null out Player#nextCommand before executing
                // command because otherwise, if the command errors out,
                // the null out statement won't get executed, and the game
                // will attempt to execute the command again next turn.
                var cmd = player.nextCommand;
                player.turns--;
                player.nextCommand = null;
                cmd.Act(map);
                ui.NextTurn();
                if (player.turns <= 0) {
                    map.NewTurn();
                    player.turns = 1;
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