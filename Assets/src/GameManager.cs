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
	public enum GameState {
		Default,
		Paused,
		Selected,
	}

	class GameManager : MonoBehaviour {
		public static WorldMap world;
		public static Player p;
		public static Layout l;
		public static UIManager ui;

		private GameCamera gc;
		private GameState state;

		//public static Card selected;

		void Awake() {
			// initialize the camera
			gc = new GameObject ("Game Camera").AddComponent<GameCamera> ();
			gc.init (Camera.main);

			// initialize the map
			l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));
			world = LevelLoader.LoadLevel(l, "level1");

			p = new Player(world.hero);
            
			ui = gameObject.AddComponent<UIManager>();

			//this.selected = null;
		}

		void Update() {
            if (p.nextCommand != null) {
                print("Executing command " + p.nextCommand);

                // We need to null out Player#nextCommand before executing
                // command because otherwise, if the command errors out,
                // the null out statement won't get executed, and the game
                // will attempt to execute the command again next turn.
                var cmd = p.nextCommand;
                p.nextCommand = null;
                cmd.Act(world);
                world.NewTurn();
            }
		}
	}
}
