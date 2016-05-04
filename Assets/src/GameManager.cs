using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using game.math;
using game.ui;
using game.world;
using game.world.levels;
using game.world.triggers;
using game.tcg.cards;
using game.render;

namespace game {
    class GameManager : MonoBehaviour {
		public static GameManager instance;
		public static Layout l = new Layout(Orientation.Pointy, new Vector2(1, 1), new Vector2(0, 0));
		public static Player p;
		public static AudioManager audiom;
		public static WorldMap world;
		public static GameLevel level;

		private class LevelRegistery<T> {
			static readonly Dictionary<int, Func<T>> _dict = new Dictionary<int, Func<T>>();

			private LevelRegistery() { }

			public static T Create(int id) {
				Func<T> constructor = null;
				if (_dict.TryGetValue (id, out constructor)) {
					return constructor();
				}
				throw new ArgumentException (String.Format("No level is registered for id {0}", 0));
			}

			public static void Register(int id, Func<T> constructor) {
				_dict.Add (id, constructor);
			}
		}

		void Awake() {
			if (instance != null) {
				Debug.Log ("GameManager is a singleton.");
			}
			instance = this;

			LevelRegistery<GameLevel>.Register (0, () => new ForestLevel());
			LevelRegistery<GameLevel>.Register (1, () => new MireLevel());
			LevelRegistery<GameLevel>.Register (2, () => new RiverLevel());
			LevelRegistery<GameLevel>.Register (3, () => new VolcanoLevel());
			LevelRegistery<GameLevel>.Register (4, () => new CatacombLevel());
			LevelRegistery<GameLevel>.Register (5, () => new CryptLevel());

			gameObject.AddComponent<RenderManager> ();
			gameObject.AddComponent<UIManager> ();
			audiom = gameObject.AddComponent<AudioManager>();

			p = new Player();
		}

		public static GameLevel GetLevel(int id) {
			return LevelRegistery<GameLevel>.Create (id);
		}

		public static void LoadLevel(int id) {
			if (world != null) {
				Destroy (world.hexes.gameObject);
			}

			level = LevelRegistery<GameLevel>.Create (id);
			world = level.GetMap (instance);

			p.hero = world.hero;
			p.deck = level.GetDeck ();

			UIManager.SetGUI (GUIType.World);
		}

        void Update() {
			if (UIManager.gui.GetType () == typeof(GUIWorld)) {
				if (world.hero.dead) {
					return;
				}

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
}
