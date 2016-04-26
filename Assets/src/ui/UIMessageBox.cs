using UnityEngine;
using System.Collections;

namespace game.ui {
	class UIMessageBox : MonoBehaviour {
		private SpriteRenderer sr;
		private BoxCollider2D coll;

		private string message;

		public void init(string message) {
			this.message = message;

			gameObject.transform.position = new Vector3 (0, 0, Layer.HUD);

			// BACKGROUND
			sr = gameObject.AddComponent<SpriteRenderer> ();
		}
	}
}