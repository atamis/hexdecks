using UnityEngine;
using System.Collections.Generic;

namespace game.tcg {
	class CardManager : MonoBehaviour {
		enum ActionState {
			Default,
			Playing
		};

		public List<Card> hand; // list of cards
		public Card selected; // card being played

		public void init() {
			hand = new List<Card>();
			for (int i = 0; i < 5; i++) {
				Card c = new GameObject ("Card").AddComponent<Card> ();
				c.init ();
			}
		}

		void Update() {
			
		}
	}
}
