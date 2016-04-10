using UnityEngine;
using System.Collections;
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

        public abstract bool CanPlay(WorldMap w, Hex h);

		public abstract void OnPlay(WorldMap w, Hex h);

        public abstract List<Hex> ValidTargets(WorldMap w, Hex h);

		public void init() {
			model = new GameObject ("Card Model").AddComponent<CardModel> ();
			model.init (this);

			model.transform.parent = transform;
		}

		private class CardModel : MonoBehaviour {
			SpriteRenderer sr;
			BoxCollider2D coll;
			private Card c;
			bool hovering;

			public void init(Card c) {
				this.c = c;

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/Square");
				sr.material = new Material (Shader.Find ("Sprites/Default"));

				coll = gameObject.AddComponent<BoxCollider2D> ();
				coll.isTrigger = true;
			}

			void Update() {
				gameObject.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(-1,-1, 10)); // Covert to Viewport Space
			}

			void OnMouseEnter() {
				// iterate through the valid casting locations (impassable terrain is invalid)
				Debug.Log ("Entered Card");
			}

			void OnMouseExit() {
				// move down, unhighlight targets
				Debug.Log ("Left Card");
			}

			void OnMouseDown() {
				
				// Set the current playing target to this card
				// Hightlight or lock card in position

				// OnMouseEnter Hex -> highlight card effect
				Debug.Log ("Clicked");
			}
		}
	}
}
