using UnityEngine;
using System.Collections;

namespace game.ui {
	class ScreenOverlay : MonoBehaviour {
		public SpriteRenderer sr;
		private float fadeSpeed = .5f;
		private Color goal;

		public void init() {
			sr = gameObject.AddComponent<SpriteRenderer> ();
			sr.sprite = Resources.Load<Sprite> ("Sprites/Square");
			sr.color = Color.clear;
		}

		void Start() {
			float h_screen = Camera.main.orthographicSize * 2;
			float w_screen = h_screen / Screen.height * Screen.width;
			transform.localScale = new Vector3(w_screen / sr.sprite.bounds.size.x, h_screen * sr.sprite.bounds.size.y, 1);
		}

		public void FadeToColor(Color start, Color end) {
			sr.color = start;
			this.goal = end;
		}

		void Update() {
			sr.color = Color.Lerp (sr.color, this.goal, fadeSpeed * Time.deltaTime);
		}
	}
}

