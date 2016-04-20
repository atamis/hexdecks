using UnityEngine;
using System.Collections;

namespace game.ui {
	class GameCamera : MonoBehaviour { 
		Camera cam;
		float speed = 1f;
		private Vector3? goal;

		public void init(Camera cam) {
			this.cam = cam;
			this.cam.transform.parent = transform;
			this.cam.backgroundColor = new Color(0.25f, 0.25f, 0.25f);
			goal = null;
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
	}
}
