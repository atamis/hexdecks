using UnityEngine;
using System.Collections.Generic;

namespace game.tcg {
	class Deck {
		public List<Card> cards;

		public Deck() {
			this.cards = new List<Card> ();

			for (int i = 0; i < 10; i++) {
				//cards.Add (new Card ());
			}
		}

		// draw the top i cards of your library
		public List<Card> DrawCards(int i) {
			List<Card> tmp = new List<Card> ();
			tmp.AddRange (cards.GetRange (0, i));
			cards.RemoveRange (0, i);
			return tmp;
		}
	}
}
