using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace game.ui {
	class IntroUI : GameUI {
		internal GameManager gm;

		/*
		private string[] levels = new string[] { 
			"tutorial",
			"level1",
			"level2",
		};
		*/

		public override void init(GameManager gm) {
			this.gm = gm;

			UILoadButton b = new GameObject ("UI Load Button").AddComponent<UILoadButton> ();
			b.init (this, "level1");
		}

		void Update() {

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
				ui.gm.LoadMap (level);
			}
		}
	}
}
