using UnityEngine;
using System.Collections.Generic;
using game.world;
using game.math;
using game.ui;

namespace game.tcg.cards {
	[System.Serializable]
	public struct CardData {
		public int id;
	}

	abstract class Card : MonoBehaviour {
		private CardModel model;
		public string title { get; set; }
		public string bodyText { get; set; }
		public int cost { get; set; }

		public void init() {
			model = new GameObject ("Card Model").AddComponent<CardModel> ();
			model.init (this);

			model.transform.parent = transform;
			model.transform.position = new Vector2 (1, 1);
		}

		public abstract bool CanPlay (WorldMap w, Hex h);

		public abstract void OnPlay (WorldMap w, Hex h);

		public abstract List<Hex> ValidTargets (WorldMap w, Hex h);

		public abstract List<Hex> PreCast(Hex h, int direction);

		public CardData Serialize() {
			return new CardData() {
				//id = this.id;
			};
		}

		public static Card Deserialize(CardData data) {
			return null;
		}

		private class CardModel : MonoBehaviour {
			private SpriteRenderer sr;
			private BoxCollider2D coll;

			private enum CardState {
				Default,
				Dragging,
			};
			private CardState state;

			private Vector3 screenPoint;
			public Vector3 origin;
			private GameObject target;

			private Card c;

			public void init(Card c) {
				this.c = c;
				this.tag = "Card";
				this.gameObject.layer = LayerMask.NameToLayer("CardLayer");

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/Square");
				sr.material = new Material (Shader.Find ("Sprites/Default"));
				sr.color = Color.green;

				coll = gameObject.AddComponent<BoxCollider2D> ();
				coll.size = new Vector3 (1.0f, 1.0f, 0);
				coll.isTrigger = true;
			}

			void Update() {
				if (this.state != CardState.Dragging) {
					transform.position = Vector3.MoveTowards (transform.position, origin, 1.0f);
				}
			}

			void SetOrigin(Vector3 pos) {
				this.origin = pos;
			}
				
			void OnMouseEnter() {
				foreach (Hex h in this.c.ValidTargets(GameManager.world, GameManager.p.hero.h)) {
					h.Highlight(Color.red);
				}
			}

			void OnMouseExit() {
				// Highlight valid targets
				foreach (Hex h in this.c.ValidTargets(GameManager.world, GameManager.p.hero.h)) {
					h.Highlight(Color.white);
				}
			}

			void OnMouseStay() {
				// TODO Render Helper Model
			}

			void OnMouseDown() {
				WorldUI.selected = this.c;
				GameObject.FindGameObjectWithTag ("Cursor").SendMessage ("SetState", 1);

				this.state = CardState.Dragging;
				screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			}

			void OnMouseUp() {
				GameObject.FindGameObjectWithTag ("Cursor").SendMessage ("SetState", 2);
				this.state = CardState.Default;
			}

			void OnMouseDrag() {
				Vector3 curPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
				transform.position = Camera.main.ScreenToWorldPoint (curPos);
			}

			void OnTriggerEnter2D() {

			}
			/*
			void OnMouseUp() {
				this.state = CardState.Default;
				Hex h = MathLib.GetHexAtMouse();

				if (h != null) {
					this.c.OnPlay (GameManager.world, h);
					Debug.Log ("Played Card");
					Destroy(this.c);
				}

				foreach (Hex h2 in this.cache) {
					h2.Highlight (Color.white);
				}
			}

			
			void OnMouseOver() {
				// Highlight valid targets


				// render the helper model
				switch (this.state) {
				case CardState.Default:
					// TODO RENDER HELPER MODEL
					break;
				default:
					break;
				}
			}
			*/
		}

		private class CardHelperModel : MonoBehaviour {
			private TextMesh text;

			public void init() {

			}
		}
	}
}