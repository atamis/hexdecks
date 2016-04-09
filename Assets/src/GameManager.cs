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

		void Awake() {
			// initialize the camera
			gc = new GameObject ("Game Camera").AddComponent<GameCamera> ();
			gc.init (Camera.main);

			// initialize the map
			l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));
			world = new WorldMap (l);

			// add the hero
			var hero = new GameObject("Tim").AddComponent<HeroUnit>();
			hero.init(world, world.map[new HexLoc(0, 0)]);

			p = new Player(hero);




			ui = gameObject.AddComponent<UIManager>();
			ui.init(world, p);

			var enemy = new GameObject("EvilTim").AddComponent<EnemyUnit>();
			enemy.init(world, world.map[new HexLoc(1, 1)]);
		}

		void Update() {
			if (p.nextCommand != null) {
				print("Executing command " + p.nextCommand);
				p.nextCommand.Act(world);
				p.nextCommand = null;
			}
		}
	}
}