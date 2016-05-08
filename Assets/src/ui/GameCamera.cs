using UnityEngine;
using System.Collections;
using game;

namespace game.ui {
	class GameCamera : MonoBehaviour {
		private Camera cam;
		float speed = 1f;
		private Vector3? goal;
		public bool locked { get; private set; }
		public bool CamLock;

        public void init(Camera cam) {
			this.cam = cam;
			this.cam.transform.parent = transform;
			this.cam.backgroundColor = new Color(0.05f, 0.05f, 0.05f);
			goal = null;
			locked = false;
			this.cam.orthographicSize = 5.5f;
			CamLock = false;
		}

		public void SetLocation(Vector3 dest) {
			transform.localPosition = dest;
		}

		public void SetGoal(Vector3 dest) {
			if (!locked) {
				goal = dest;
			}
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
			if(Input.GetKeyUp(KeyCode.LeftShift)){
				CamLock = !CamLock;
			}
			if(CamLock){
			 	SetGoal(new Vector3(GameManager.l.HexPixel(GameManager.world.hero.h.loc).x,
				GameManager.l.HexPixel(GameManager.world.hero.h.loc).y - 1, .05f
				));
			}

			if (goal.HasValue && CamLock) {
				if (closeToGoal()) {
					goal = null;
					locked = false;
				} else {
					transform.localPosition = Vector3.Slerp(transform.localPosition, goal.Value, 0.05f);
				}
			}

			// Ensure 1 <= size <= 20.
			cam.orthographicSize = System.Math.Min(System.Math.Max(1, cam.orthographicSize), 20);

			if (!locked) {
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
	}


}
