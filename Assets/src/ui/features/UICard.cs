using UnityEngine;
using System.Collections.Generic;
using game.math;
using game.tcg;
using game.tcg.cards;
using game.world;

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
        private SpriteRenderer artSr;
		private BoxCollider2D bc;

		private TCGCard card {
            get {
                return GameManager.p.hand[idx];
            }
        }
        private int idx;
		private List<Hex> targets;
        private GameObject textObj;
        private TextMesh tm;
		private GUIWorld ui;

		public void init(GUIWorld ui, int idx) {
			this.ui = ui;
            this.idx = idx;
			this.tag = "Card";
			this.gameObject.layer = LayerMask.NameToLayer("CardLayer");
            this.transform.localPosition = new Vector3(0, 0, -1);

			sr = gameObject.AddComponent<SpriteRenderer> ();
			sr.sprite = Resources.Load<Sprite> ("Sprites/UI/T_CardBaseHand");
			sr.material = new Material (Shader.Find ("Sprites/Default"));
			sr.color = Color.white;

			bc = gameObject.AddComponent<BoxCollider2D> ();
			bc.size = new Vector3 (1.0f, 1.33f, 0);
			bc.isTrigger = true;

            textObj = new GameObject("Card Text");
            textObj.transform.parent = transform;
            textObj.transform.localPosition = new Vector3(-0.40f, 0.5f, -0.1f);

			tm = textObj.AddComponent<TextMesh>();

			tm.text = card.GetName();
			tm.fontSize = 148;
			tm.characterSize = 0.008f;
			tm.color = Color.black;
			tm.font = UIManager.font;
			tm.GetComponent<Renderer>().material = UIManager.font.material;

            var artObj = new GameObject("Card Art");
            artObj.transform.parent = transform;
            artObj.transform.localPosition = new Vector3(-0.008f, 0.079f, -0.3f);
            artObj.transform.localScale = new Vector3(0.5561915f, 0.547473f, 0.5f);

            artSr = artObj.AddComponent<SpriteRenderer>();

        }

        void Update() {
			tm.text = card.GetName();
            artSr.sprite = card.GetSprite();

			if (this.state != CardState.Dragging) {
				transform.position = Vector3.MoveTowards (transform.position, origin, 1.0f);
			}
		}
				
		public void SetOrigin(Vector3 pos) {
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
			Hex h = MathLib.GetHexAtMouse (GameManager.world);
			if (h != null && targets.Contains(h)) {
                GameManager.p.Play(card, h);
			}
			this.state = CardState.Default;
		}

		void OnMouseEnter() {
            ui.magCard.card = card;
			this.targets = card.ValidTargets (GameManager.world, GameManager.world.hero.h);
			foreach (Hex h in this.targets) {
				h.Highlight (Color.red);
			}
		}

		void OnMouseExit() {
            ui.magCard.card = null;
            foreach (Hex h in this.targets) {
				h.Highlight (Color.white);
			}
		}
	}
}

