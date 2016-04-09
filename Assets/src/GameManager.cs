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

            var hero = new GameObject("Tim").AddComponent<HeroUnit>();
            hero.init(map, map.map[new HexLoc(0, 0)]);

            player = new Player(hero);

            ui = gameObject.AddComponent<UIManager>();
            ui.init(map, player);

            var enemy = new GameObject("EvilTim").AddComponent<EnemyUnit>();
            enemy.init(map, map.map[new HexLoc(1, 1)]);

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
                player.nextCommand.Act(map);
                player.nextCommand = null;
            }
		}
	}
}