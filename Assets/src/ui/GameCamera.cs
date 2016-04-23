using UnityEngine;
using System.Collections;

namespace game.ui {
	class GameCamera : MonoBehaviour {
		private ScreenOverlay overlay;
		private Camera cam;
		float speed = 1f;
		private Vector3? goal;

		public void init(Camera cam) {
			this.cam = cam;
			this.cam.transform.parent = transform;
			this.cam.backgroundColor = new Color(0.25f, 0.25f, 0.25f);
			goal = null;

			overlay = new GameObject("Overlay").AddComponent<ScreenOverlay> ();
		}

		public void setLocation(Vector3 v) {
			goal = v;
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
			overlay.transform.parent = transform;

			if (goal.HasValue) {
				if (closeToGoal()) {
					goal = null;
				} else {
					transform.localPosition = Vector3.Slerp(transform.localPosition, goal.Value, 0.05f);
				}
			}

			//var scroll = Input.GetAxis("Mouse ScrollWheel");

			// Scroll up is a positive change, but increasing size
			// zooms out, so we subtract.
			//cam.orthographicSize -= scroll;

			// Ensure 1 <= size <= 20.
			cam.orthographicSize = System.Math.Min(System.Math.Max(1, cam.orthographicSize), 20);

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

			if (Input.GetKeyDown (KeyCode.LeftShift) | Input.GetKeyDown (KeyCode.RightShift)) {

			}
		}

		private class ScreenOverlay : MonoBehaviour {
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
	}


}
