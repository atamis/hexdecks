using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace game.ui {
	class IntroUI : MonoBehaviour {
		internal GameManager gm;
		private GameObject uiFeatures;

		public void init() {
			this.gm = gm;
			this.name = "UI";

			UILoadButton b = new GameObject ("UI Load Button").AddComponent<UILoadButton> ();
			b.init (this, "level1");

			b.transform.parent = transform;
		}

		private class UILoadButton : MonoBehaviour { 
			private SpriteRenderer sr;
			private BoxCollider2D coll;
			private string level;
			private IntroUI ui;

			public void init(IntroUI ui, string level) {
				this.ui = ui;
				this.level = level;

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/Square");

				coll = gameObject.AddComponent<BoxCollider2D> ();
				coll.isTrigger = true;
			}

			void OnMouseDown() {
				GameManager.level = level;
				SceneManager.LoadSceneAsync ("Main");
			}
		}
	}
}
