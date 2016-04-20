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

		private WorldUI ui;
		private GameCamera gc;

        void Awake() {
			gc = new GameObject ("Game Camera").AddComponent<GameCamera> ();
			gc.init (Camera.main);

			ui = gameObject.AddComponent<WorldUI> ();
			ntm = new GameObject ("Notification Manager").AddComponent<NotificationManager> ();

			l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));
			world = SaveManager.LoadLevel(l, "level1", this);

			var hero = world.hero;
			gc.setLocation(l.HexPixel(world.hero.h.loc));

			var trigger = new GameObject("Trigger").AddComponent<LogTrigger>();
			trigger.init(world.map[new HexLoc(2, 2)]);
        }

        void Update() {

        }
    }
}
