using UnityEngine;
using System.Collections;

namespace game.ui {
	class UIMessageBox : MonoBehaviour {
		private SpriteRenderer sr;
		private GameObject textObj;
		private TextMesh tm;
		private string message;

		UICloseButton cb;

		private class UICloseButton : MonoBehaviour {
			private SpriteRenderer sr;
			private BoxCollider2D coll;
			private UIMessageBox mb;

			public void init(UIMessageBox mb) {
				this.mb = mb;

				transform.localPosition = new Vector3 (0, 0, Layer.HUDFX);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/UI/T_Wood");

				coll = gameObject.AddComponent<BoxCollider2D> ();
				coll.isTrigger = true;
			}

			void OnMouseDown() {
				Debug.Log ("Boop!");
				this.mb.Die ();
			}
		}

		public void init(string message) {
			this.message = message;

			gameObject.transform.position = new Vector3 (0, 0, Layer.HUD);

			// BACKGROUND
			sr = gameObject.AddComponent<SpriteRenderer> ();
			sr.sprite = Resources.Load<Sprite> ("Sprites/UI/T_Wood");

			// EXIT Button
			cb = new GameObject("Exit Button").AddComponent<UICloseButton> ();
			cb.init (this);
			cb.transform.parent = transform;

			var font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");

			textObj = new GameObject("Card Text");
			textObj.transform.parent = transform;
			textObj.transform.localPosition = new Vector3(-0.40f, 0.5f, -0.1f);

			tm = textObj.AddComponent<TextMesh>();
			tm.text = message;
			tm.fontSize = 148;
			tm.characterSize = 0.008f;
			tm.color = Color.black;
			tm.font = font;
			tm.GetComponent<Renderer>().material = font.material;
		}

		public void Die() {
			gameObject.SetActive (false);
			Destroy (this);
			Debug.Log ("Died");
		}
	}
}