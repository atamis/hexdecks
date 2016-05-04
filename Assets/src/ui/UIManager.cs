using UnityEngine;
using System.Collections;

namespace game.ui {
	class UIManager : MonoBehaviour {
		public static UIManager instance;
		public static Font font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");
		public static GameCamera gc;
		public static NotificationManager ntm;

		public GUIBase gui { get; set; } 

		void Awake() {
			if (instance != null) { 
				Debug.Log ("Can't have more than one UIManager!");
			}
			instance = this;

			ntm = new GameObject ("Notification Manager").AddComponent<NotificationManager> ();

			this.gui = new GameObject ("Intro GUI").AddComponent<GUIIntro> ();
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

	}
}

