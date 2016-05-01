using UnityEngine;
using System.Collections;

namespace game.ui {
	class GameCamera : MonoBehaviour {
		private ScreenOverlay overlay;
		private Camera cam;
		float speed = 1f;
		private Vector3? goal;
		private bool locked;
        public static AudioManager audiom;

        public void init(Camera cam) {
			this.cam = cam;
			this.cam.transform.parent = transform;
			this.cam.backgroundColor = new Color(0.05f, 0.05f, 0.05f);
			goal = null;
			locked = false;
		}

		void Start() {
			overlay = new GameObject("Overlay").AddComponent<ScreenOverlay> ();
			overlay.transform.parent = transform;
			overlay.init (this);

            audiom = gameObject.AddComponent<AudioManager>();
        }

		public void setLocation(Vector3 v) {
			goal = v;
		}

		public void SetLock(bool locked) {
			this.locked = locked;
		}

		private bool closeToGoal() {
			if (goal.HasValue) {
				var v1p = transform.localPosition - goal.Value;
				return v1p.sqrMagnitude < 0.3;
			} else {
				return true;
			}
		}

		// Update is called once per frame
		void Update() {
			if (goal.HasValue) {
				if (closeToGoal()) {
					goal = null;
				} else {
					transform.localPosition = Vector3.Slerp(transform.localPosition, goal.Value, 0.05f);
				}
			}

			// Ensure 1 <= size <= 20.
			cam.orthographicSize = System.Math.Min(System.Math.Max(1, cam.orthographicSize), 20);

			if (locked == false) {
				var control = new Vector3(0, 0, 0);
				if (Input.GetKey(KeyCode.A)) {
					control.x -= speed;
				}

				if (Input.GetKey(KeyCode.D)) {
					control.x += speed;
				}

				if (Input.GetKey(KeyCode.W)) {
					control.y += speed;
				}

				if (Input.GetKey(KeyCode.S)) {
					control.y -= speed;
				}

				// Include zoom-level to make zoomed-out movement faster.
				transform.localPosition += control * Time.deltaTime * cam.orthographicSize;
			}
		}

		private class ScreenOverlay : MonoBehaviour {
			public SpriteRenderer sr;
			private GameCamera gc;
			private float fadeSpeed = 1.5f;

			public void init(GameCamera gc) {
				this.gc = gc;

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
				/*
				if (sr.color.a == 0) {
					this.gc.locked = false;
				}
				*/
			}

			void Update() {
				FadeToClear (Color.black);
			}
		}
	}


}
