using UnityEngine;
using System.Collections.Generic;
using game.math;
using game.ui;
using game.world;

namespace game {
    class GameManager : MonoBehaviour {
		public static WorldMap world;
		public static Layout l;
		public static NotificationManager ntm;
		public static Player p;

		private WorldUI ui;
		private GameCamera gc;

        void Awake() {
			gc = new GameObject ("Game Camera").AddComponent<GameCamera> ();
			gc.init (Camera.main);

			l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));
			world = SaveManager.LoadLevel(l, "level1", this);

			var hero = world.hero;
			//gc.setLocation(l.HexPixel(world.hero.h.loc));

			var trigger = new GameObject("Trigger").AddComponent<LogTrigger>();
			trigger.init(world.map[new HexLoc(2, 2)]);

			p = new Player(hero);

			// Make sure this happens last 
			ui = gameObject.AddComponent<WorldUI> ();
			ntm = new GameObject ("Notification Manager").AddComponent<NotificationManager> ();
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
				if (p.turns <= 0) {
					world.NewTurn();
					p.turns = 1;
				}

			}
        }
    }
}
