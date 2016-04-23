using UnityEngine;
using System.Collections.Generic;
using game.tcg;

namespace game {
    class Player {
		public Command nextCommand;
		public int turns;

		public List<int> hand;
		public List<int> cards;

		public Player() {
			this.cards = new List<int> ();
			this.hand = new List<int> ();
		}

		// Add card to library

		// shuffle deck

		// draw cards

		//
    }
}
