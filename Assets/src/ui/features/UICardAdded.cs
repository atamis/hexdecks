using UnityEngine;
using System.Collections;

namespace game.ui.features {
	class UICardAdded : MonoBehaviour {
		private SpriteRenderer sr;

		public void init(string message) {
			// BACKGROUND
			sr = gameObject.AddComponent<SpriteRenderer>();
			sr.sprite = Resources.Load<Sprite>("Sprites/UI/T_Wood");

			// CONTINUE BUTTON
			UIButton cb = new GameObject("Continue Button").AddComponent<UIButton>();
			cb.init(Resources.Load<Sprite>("Sprites/UI/T_Wood"), ContinueFunc);

			cb.transform.parent = transform;
			cb.transform.localPosition = new Vector3(0, -.75f, 0);
			cb.transform.localScale = new Vector3(.5f, .5f, 1);
		}

		public void ContinueFunc() {
			Destroy (gameObject);
		}

		public void MakeTitle() {
			GameObject title = new GameObject("Continue Menu Text");
			title.transform.parent = transform;
			title.transform.localPosition = new Vector3(0, 0, -0.1f);

			TextMesh tm = title.AddComponent<TextMesh>();
			tm.text = "You've got mail!";
			tm.color = Color.black;
			tm.alignment = TextAlignment.Center;
			tm.anchor = TextAnchor.MiddleCenter;
			tm.fontSize = 64;
			tm.characterSize = 0.05f;
			tm.font = UIManager.font;
			tm.GetComponent<Renderer>().material = UIManager.font.material;
		}
	}
}
