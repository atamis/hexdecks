using UnityEngine;
using System.Collections;

namespace game.ui {
	class UIManager : MonoBehaviour {
		public static UIManager instance;
		public static Font font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");
		public static GameCamera gc;
		public static NotificationManager ntm;
		public static GUIBase gui { get; private set; }

		void Awake() {
			if (instance != null) { 
				Debug.Log ("Can't have more than one UIManager!");
			}
			instance = this;

			ntm = new GameObject ("Notification Manager").AddComponent<NotificationManager> ();
			gui = new GameObject ("Intro GUI").AddComponent<GUIIntro> ();
		}

		void Start() {
			gc = new GameObject ("Game Camera").AddComponent<GameCamera> ();
			gc.init (Camera.main);
		}

		void Update() {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				if (gui.GetType () == typeof(GUIIntro)) { 
					Application.Quit ();

				} else if (gui.GetType () == typeof(GUIWorld)) {
					gc.SetLock (true);
				}
			}
		}

		public static void SetGUI(GUIType type) {
			if (gui != null) {
				gui.gameObject.SetActive (false);
				Destroy (gui.gameObject);
				gui = null;
			}

			switch (type) {
			case GUIType.Intro:
				gui = new GameObject ("Intro GUI").AddComponent<GUIIntro> ();
				break;
			case GUIType.World:
				gui = new GameObject ("World GUI").AddComponent<GUIWorld> ();
				gc.setLocation(GameManager.l.HexPixel(GameManager.world.hero.h.loc));
				break;
			default:
				break;
			}
		}
	}
}

