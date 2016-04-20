using UnityEngine;
using System.Collections;
using game.tcg;
using game.tcg.cards;

namespace game.ui {
	class UICard : MonoBehaviour {
		private enum CardState {
			Default,
			Dragging,
		};

		private CardState state;
		private Vector3 screenPoint;
		public Vector3 origin;

		private SpriteRenderer sr;
		private BoxCollider2D bc;

		private int id; // Card ID

		public void init() {
			this.tag = "Card";
			this.gameObject.layer = LayerMask.NameToLayer("CardLayer");

			sr = gameObject.AddComponent<SpriteRenderer> ();
			sr.sprite = Resources.Load<Sprite> ("Sprites/Square");
			sr.material = new Material (Shader.Find ("Sprites/Default"));
			sr.color = Color.white;

			bc = gameObject.AddComponent<BoxCollider2D> ();
			bc.size = new Vector3 (1.0f, 1.0f, 0);
			bc.isTrigger = true;
		}

		void Update() {

		}

		void SetOrigin(Vector3 pos) {
			this.origin = pos;
		}

		public void SetColor(Color c) {
			sr.color = c;
		}

		void OnMouseDown() {
			this.state = CardState.Dragging;
			screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);

			transform.localEulerAngles = new Vector3(0, 0, 0);
		}

		void OnMouseDrag() {
			Vector3 curPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
			transform.position = Camera.main.ScreenToWorldPoint (curPos);
		}

		void OnMouseUp() {
			CardManager.GetCard (this.id).OnPlay();
		}
	}
}

