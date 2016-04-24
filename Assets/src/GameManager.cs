using UnityEngine;
using System;
using System.Collections.Generic;
using game.math;
using game.ui;
using game.world;
using game.tcg.cards;

namespace game {
    class GameManager : MonoBehaviour {
		public static Layout l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));
		public static Player p;
		public static NotificationManager ntm;
		public static string level = "level1";

		private WorldUI ui;

		private WorldMap _world;
		public WorldMap world {
			get {
				if (ui.GetType() != typeof(WorldUI)) {
					throw new Exception ("Can't access world in IntroUI!");
				}
				return _world;
			}
			private set {
				_world = value;
			}
		}

		void Awake() {
			ntm = new GameObject ("Notification Manager").AddComponent<NotificationManager> ();
			p = new Player();
		}

		void Start() {
			ui = gameObject.AddComponent<WorldUI> ();
			world = SaveManager.LoadLevel(l, level, this);
			p.hero = world.hero;

			var trigger = new GameObject("Trigger").AddComponent<LogTrigger>();
			trigger.init(world.map[new HexLoc(2, 2)]);

			ui.init (this);
			ui.gc.setLocation(l.HexPixel(world.hero.h.loc));
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
