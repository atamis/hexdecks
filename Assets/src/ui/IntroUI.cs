using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace game.ui {
	class IntroUI : GameUI {
		void Awake() {
			font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");

			UILoadButton b = new GameObject ("UI Load Button").AddComponent<UILoadButton> ();
			b.init ();
		}

		void Update() {

		}

		private class UILoadButton : MonoBehaviour { 
			private SpriteRenderer sr;
			private string levelName;

			public void init() {

			}

			void OnMouseUp() {
				SceneManager.LoadSceneAsync(levelName);
			}
		}
	}
}
