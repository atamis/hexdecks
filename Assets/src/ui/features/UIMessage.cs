using UnityEngine;
using System.Collections;

namespace game.ui {
	class UIMessage : MonoBehaviour {
		private SpriteRenderer sr;
		private GameObject textObj;
		private TextMesh tm;

		UICloseButton cb;

		private class UICloseButton : MonoBehaviour {
			private SpriteRenderer sr;
			private BoxCollider2D coll;
			private UIMessage mb;

			public void init(UIMessage mb) {
				this.mb = mb;

				transform.localPosition = new Vector3 (1.5f, -.75f, Layer.HUDFX);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/UI/T_Wood");

				coll = gameObject.AddComponent<BoxCollider2D> ();
				coll.isTrigger = true;
			}

			void OnMouseDown() {
				Destroy (this.mb.gameObject);
			}
		}

		public void init(string msg) {
			transform.localPosition = new Vector3 (0, 0, Layer.HUD);

			// BACKGROUND
			sr = gameObject.AddComponent<SpriteRenderer> ();
			sr.sprite = Resources.Load<Sprite> ("Sprites/UI/T_Wood");

			// EXIT Button
			cb = new GameObject("Exit Button").AddComponent<UICloseButton> ();
			cb.init (this);

			cb.transform.localScale = new Vector3 (.25f, .5f, 1);

			cb.transform.parent = transform;

			textObj = new GameObject("Card Text");
			textObj.transform.parent = transform;
			textObj.transform.localPosition = new Vector3(0, 0, -.1f);

			tm = textObj.AddComponent<TextMesh>();
			tm.fontSize = 64;
			tm.characterSize = 0.04f;
			tm.color = Color.black;
			tm.alignment = TextAlignment.Center;
			tm.anchor = TextAnchor.MiddleCenter;
			tm.font = UIManager.font;
			tm.GetComponent<Renderer>().material = UIManager.font.material;

			float rowLimit = 2.0f; //find the sweet spot
			string builder = "";
			string text = msg;
			string[] parts = text.Split(' ');
			tm.text = "";

			for (int i = 0; i < parts.Length; i++) {
				tm.text += parts[i] + " ";

				if (tm.GetComponent<Renderer>().bounds.extents.x > rowLimit) {
					tm.text = builder.TrimEnd() + System.Environment.NewLine + parts[i] + " ";
				}

				builder = tm.text;
			}
		}
	}
}