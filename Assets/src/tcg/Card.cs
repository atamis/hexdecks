using UnityEngine;
using System.Collections;
using game.world;
using game.world.math;

namespace game.tcg {
	public enum Targets {
		Empty,
		Minion,
		Hero,
	};
		
	class Card : MonoBehaviour {
		public string name { get; set; }
		public int cost { get; set; }

		private CardModel model;

		public void init() {
			model = new GameObject ("Card Model").AddComponent<CardModel> ();
			model.init (this);

			model.transform.parent = transform;
		}

		public virtual void OnPlay(HexLoc hl) {

		}

		public virtual bool ValidTarget(Hex h) {
			return false;
		}

		private class CardModel : MonoBehaviour {
			SpriteRenderer sr;
			BoxCollider2D coll;
			private Card c;

			public void init(Card c) {
				this.c = c;

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/Square");
				sr.material = new Material (Shader.Find ("Sprites/Default"));

				coll = gameObject.AddComponent<BoxCollider2D> ();
				coll.isTrigger = true;
			}

			void Update() {
				gameObject.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(0,1,10)); // Covert to Viewport Space
			}

			void OnMouseEnter() {
				Debug.Log ("Entered Card");
			}
		}
	}
}
