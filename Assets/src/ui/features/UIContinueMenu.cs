using UnityEngine;
using System.Collections;

// Load next level
// Statistics
namespace game.ui.features {
	class UIContinueMenu : MonoBehaviour {
		private SpriteRenderer sr;

		public void init() {
			// BACKGROUND
			sr = gameObject.AddComponent<SpriteRenderer>();
			sr.sprite = Resources.Load<Sprite>("Sprites/UI/T_Wood");

			// CONTINUE BUTTON
			UIButton cb = new GameObject("Continue Button").AddComponent<UIButton>();
			cb.init(Resources.Load<Sprite>("Sprites/UI/T_Wood"), ContinueFunc);

			cb.transform.parent = transform;
			cb.transform.localPosition = new Vector3(0, -.75f, 0);
			cb.transform.localScale = new Vector3(.5f, .5f, 1);

			MakeTitle();
		}

		public void ContinueFunc() {
			GameManager.LoadLevel(GameManager.level.GetNextLevel());
			Destroy (gameObject);
		}

		public void MakeTitle() {
			GameObject title = new GameObject("Continue Menu Text");
			title.transform.parent = transform;
			title.transform.localPosition = new Vector3(0, 0, -0.1f);

			TextMesh tm = title.AddComponent<TextMesh>();
			tm.text = "Your journey \n continues!";
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
