using UnityEngine;
using System.Collections;

namespace game.ui {
	internal class ScreenOverlay : MonoBehaviour {
		private SpriteRenderer sr;
		private float fadeSpeed = 1.5f;

		void Awake() {
			gameObject.layer = LayerMask.NameToLayer ("UI");
			//gameObject.hideFlags = HideFlags.HideInHierarchy;

			sr = gameObject.AddComponent<SpriteRenderer> ();
			sr.sprite = Resources.Load<Sprite> ("Sprites/Square");
			sr.color = Color.black;
		}

		void Start() {
			float h_screen = Camera.main.orthographicSize * 2;
			float w_screen = h_screen / Screen.height * Screen.width;
			transform.localScale = new Vector3(w_screen / sr.sprite.bounds.size.x, h_screen * sr.sprite.bounds.size.y, 1);
		}

		public void FadeToClear(Color start) {
			sr.color = Color.Lerp (sr.color, Color.clear, fadeSpeed * Time.deltaTime);
		}

		void Update() {
			FadeToClear (Color.black);
		}

	}
		
	class GameUI : MonoBehaviour {
		internal ScreenOverlay overlay;
		static internal Font font;
	}
}

