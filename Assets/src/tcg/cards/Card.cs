using UnityEngine;
using System;
using System.Collections.Generic;
using game.world;
using game.ui;
using game.math;
using game.tcg;

// TODO
// Cards as commands
// Fix Colliders
// Registering new cards

namespace game.tcg.cards {
	[System.Serializable]
	public struct CardData {
		public int id;
	}

	class CardCommand : Command {
		public override void Act(WorldMap w) {
			// How do I fit this into a command?
		}
	}

	abstract class Card : MonoBehaviour {
		private CardModel model;

		public string GetText() {
			return "";
		}

		public Sprite GetSprite() {
			return Resources.Load<Sprite> ("Sprites/Circle");
		}

		public abstract List<Hex> ValidTargets (WorldMap w, Hex h); 

		public abstract List<Hex> PreCast(Hex h, int direction);

		public abstract bool CanPlay (WorldMap w, Hex h);

		public abstract void OnPlay (WorldMap w, Hex h);

		public void init() {
			model = new GameObject ("Card Model").AddComponent<CardModel> ();
			model.init (this);

			model.transform.parent = transform;
			model.transform.position = new Vector2 (1, 1);
		}

		private class CardModel : MonoBehaviour {
			private enum CardState {
				Default,
				Dragging,
			};
			private CardState state;

			private SpriteRenderer sr;
			private BoxCollider2D coll;

			private Vector3 screenPoint;
			public Vector3 origin;
			private GameObject target;

			private Card c;
			private List<Hex> targets;

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

			// DONE
			void OnMouseEnter() {
				this.targets = this.c.ValidTargets (GameManager.world, GameManager.world.hero.h);
				foreach (Hex h in this.targets) {
					h.Highlight (Color.red);
				}
			}

			// DONE
			void OnMouseExit() {
				foreach (Hex h in this.targets) {
					h.Highlight (Color.white);
				}
			}

			void OnMouseDown() {
				//WorldUI.selected = this.c;
				//GameObject.FindGameObjectWithTag ("Cursor").SendMessage ("SetState", 1);

				this.state = CardState.Dragging;
				screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
			}

			void OnMouseUp() {
				//WorldUI.selected = null;
				//GameObject.FindGameObjectWithTag ("Cursor").SendMessage ("SetState", 2);

				Hex h = MathLib.GetHexAtMouse ();
				if (h != null && this.targets.Contains(h)) {
					c.OnPlay (GameManager.world, h);
					Destroy (this);
				}
				this.state = CardState.Default;
			}

			void OnMouseDrag() {
				Vector3 curPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
				transform.position = Camera.main.ScreenToWorldPoint (curPos);
			}
		}

		private class CardHelperModel : MonoBehaviour {
			public void init() {

			}
		}
	}
}