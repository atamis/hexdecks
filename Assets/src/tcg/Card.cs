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
		private BoxCollider2D coll;

		public Card() {

		}

		public virtual void OnPlay(HexLoc hl) {

		}

		public virtual bool ValidTarget(Hex h) {
			return false;
		}

		private class CardModel : MonoBehaviour {
			SpriteRenderer sr;
			private Card c;

			public void init(Card c) {
				this.c = c;

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/Square");
			}

			void Update() {


			}
		}
	}
}
