using UnityEngine;
using System.Collections;
using game;
using game.ui;
using game.world;
using game.world.math;
using System.Collections.Generic;

namespace game.tcg {
	[System.Serializable]
	public struct CardData {
		public string name;
	}

	public enum Targets {
		Empty,
		Minion,
		Hero,
	};

	// TODO
	// Abstract Card
	abstract class Card : MonoBehaviour {
		private CardModel model;
		public string title { get; set; }
        public string bodyText { get; set; }
		public int cost { get; set; }

		public abstract bool CanPlay (WorldMap w, Hex h);

		public abstract void OnPlay (WorldMap w, Hex h);

		public abstract List<Hex> ValidTargets (WorldMap w, Hex h);

		public void init() {
			model = new GameObject ("Card Model").AddComponent<CardModel> ();
			model.init (this);

			model.transform.parent = transform;
			model.transform.position = new Vector2 (1, 1);
		}

		private class CardModel : MonoBehaviour {
			private SpriteRenderer sr;
			private BoxCollider2D coll;
			private bool dragging;

			private Vector3 screenPoint;
			public Vector3 origin;
			private GameObject target;

			private Card c;

			public void init(Card c) {
				this.c = c;
				this.tag = "Card";
				//gameObject.layer = "CardLayer";
				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/Square");
				sr.material = new Material (Shader.Find ("Sprites/Default"));

				coll = gameObject.AddComponent<BoxCollider2D> ();
				coll.size = new Vector3 (1f, 1.0f, 0);
				coll.isTrigger = true;
			}

			void Update() {
				this.origin = Camera.main.ScreenToWorldPoint (new Vector3 (0, -.9f, 10));
				if (!dragging) {
					transform.position = Vector3.MoveTowards (transform.position, origin, 1.0f);
				}
			}

			void OnTriggerEnter2D(Collider2D coll) {
				/*
				Vector2 worldPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				RaycastHit2D hit = Physics2D.Raycast (worldPos, Vector2.zero);
				if (hit.collider != null && hit.collider.tag == "Hex") {
					
					hit.collider.gameObject.SendMessage ("SetColor", Color.red);
				}
				*/

				if ((coll.gameObject.tag == "Hex") && dragging) {
					//this.target = coll.gameObject;
					coll.gameObject.SendMessage ("SetColor", Color.red);
				}

			}

			void OnTriggerExit2D(Collider2D coll) {
				if (coll.gameObject.tag == "Hex") {
					coll.gameObject.SendMessage ("SetColor", Color.white);
					//this.target = null;
				}
			}

			void OnMouseDown() {
				screenPoint = Camera.main.WorldToScreenPoint(gameObject.transform.position);
				dragging = true;
			}

			public Hex GetHexAtMouse() {
				Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
				HexLoc l = GameManager.l.PixelHex(worldPos);
				if (GameManager.world.map.ContainsKey(l)) {
					Hex h = GameManager.world.map[l];
					return h;
				}
				return null;
			}

			void OnMouseUp() { 
				dragging = false;
				Hex h = GetHexAtMouse ();
				if (h != null) {
					this.c.OnPlay (GameManager.world, h);
					Debug.Log ("Played Card");
				}
				//this.target = null;
			}

			void OnMouseDrag() {
				Vector3 curPos = new Vector3 (Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
				transform.position = Camera.main.ScreenToWorldPoint (curPos);
			}

			/*
			void OnMouseEnter() {
				foreach (Hex h in c.ValidTargets(GameManager.world, GameManager.world.hero.h)) {
					h.Highlight (Color.red);
				}
				Debug.Log ("Entered Card");
			}

			void OnMouseExit() {
				foreach (Hex h in c.ValidTargets(GameManager.world, GameManager.world.hero.h)) {
					h.Highlight (Color.white);
				}
				Debug.Log ("Left Card");
			}
			*/
		}
	}

	class FireballCard : Card {
		public override bool CanPlay(WorldMap w, Hex h) {
			if (h.unit != null) {
				return true;
			}
			return false;
		}

		public override void OnPlay(WorldMap w, Hex h) {
			if (h.unit != null) {
				h.unit.health -= 3;
			}
		}

		public override List<Hex> ValidTargets(WorldMap w, Hex h) {
			List<Hex> tmp = new List<Hex> ();
			foreach (Hex h2 in h.Neighbors()) {
				if (h2.Passable()) {
					tmp.Add (h2);
				}
			}
			return tmp;
		}

		public Sprite GetSprite() {
			return Resources.Load<Sprite> ("Sprites/Circle");
		}
	}
}
