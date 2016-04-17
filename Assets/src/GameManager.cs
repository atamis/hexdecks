using UnityEngine;
using System.Collections.Generic;
using game.world;
using game.math;
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
		Selected,
	}

	class GameManager : MonoBehaviour {
		public static WorldMap world;
		public static Player p;
		public static Layout l;
		public static WorldUI ui;
		public static GameState state;

		GameCamera gc;
		bool battling;
		Hex selected;
        AudioSource audioS;

		//public static Card selected;

		void Awake() {
			// initialize the camera
			gc = new GameObject ("Game Camera").AddComponent<GameCamera> ();
			gc.init (Camera.main);

			// initialize the map
			l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));
            world = SaveManager.LoadLevel(l, "level1", this);

            var trigger = new GameObject("Trigger").AddComponent<LogTrigger>();
            trigger.init(world.map[new HexLoc(2, 2)]);

            var hero = world.hero;
            gc.setLocation(l.HexPixel(world.hero.h.loc));

            p = new Player(hero);

			ui = gameObject.AddComponent<WorldUI>();
			ui.init(world, p);

            audioS = gameObject.AddComponent<AudioSource>();
            audioS.spatialBlend = 0.0f;
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
				p.turns--;
                p.nextCommand = null;
                cmd.Act(world);
                ui.NextTurn();
                if (p.turns == 0) {
					world.NewTurn();
                    world.turns = 1;
                }

            }
		}
	}
}
