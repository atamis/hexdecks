using UnityEngine;
using System.Collections;

namespace game.ui {
	class UIManager : MonoBehaviour {
		public static UIManager instance;
		public static Font font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");
		public static GameCamera gc;
		public static NotificationManager ntm;
		public static GUIBase gui { get; private set; }

		private ScreenOverlay overlay;

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
			gc.SetLock (true);

			overlay = new GameObject ("Screen Overlay").AddComponent<ScreenOverlay> ();
			overlay.init ();
			overlay.transform.localPosition = new Vector3 (0, 0, -4);
			overlay.transform.parent = gc.transform;

        }

		void Update() {
			if (Input.GetKeyUp (KeyCode.Escape)) {
				if (gui.GetType () == typeof(GUIIntro)) { 
					Application.Quit ();

				} else if (gui.GetType () == typeof(GUIWorld)) {
					gc.SetLock (!gc.locked);
				}
			}
		}

		public static void SetGUI(GUIType type) {
			if (gui != null) {
				gui.gameObject.SetActive (false);
				gui.Delete ();
				Destroy (gui.gameObject);
				gui = null;
			}

			switch (type) {
			case GUIType.Intro:
				gui = new GameObject ("Intro GUI").AddComponent<GUIIntro> ();
				gc.SetLocation (new Vector3 (0, 0, 0));
				gc.SetLock (true);
				if (GameManager.world.hexes != null) {
					Destroy (GameManager.world.hexes.gameObject);
				}

                AudioManager.playTrack2();

                break;

			case GUIType.World:
				gui = new GameObject ("World GUI").AddComponent<GUIWorld> ();
				gc.SetLocation (GameManager.l.HexPixel (GameManager.world.hero.h.loc));
				gc.SetLock (false);
				instance.overlay.FadeToColor (Color.black, Color.clear);
				break;

			default:
				break;
			}
		}

		public static void MakeMessage(string msg) {
			if (gui.GetType () != typeof(GUIWorld)) {
				Debug.Log ("Can't make a message outside of World!");
				return;
			}
			UIMessage m = new GameObject ("UI Message").AddComponent<UIMessage> ();
			m.init (msg);

			m.transform.parent = gc.transform;
			m.transform.localPosition = new Vector3 (-3, -1, Layer.HUD);
			//gc.SetLock (true);
		}
	}
}

