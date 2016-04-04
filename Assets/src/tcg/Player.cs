using UnityEngine;
using System.Collections.Generic;
using game.world.math;

namespace game.tcg {
	class Player : Actor {
		public List<Card> library;

		public Player(string name) : base(name) {
			this.pool = 10;

			library = new List<Card> ();
		}

		public void NewTurn() {
			if (hand.Count < 5) {
				deck.DrawCards (1);
			}
		}

		public void Play(Card c) {
			try {
				if (c.cost > this.pool) {
					Debug.Log("Not enough mana!");
				} else {
					
				}
			} catch (System.Exception e) {
				Debug.Log (e.ToString ());
			}
		}

	}
}
