using UnityEngine;
using System.Collections.Generic;
using game.world.math;
using game.world.units;

namespace game.tcg {
	[System.Serializable]
	public struct PlayerData {
		public string name;
	}

	class Player {
		public HeroUnit hero { get; set; } // how to load this??
		public List<Card> library;


		// for rendering
		public Deck deck { get; set; }
		public List<Card> hand { get; set; }

		public Command nextCommand;

		public Player(HeroUnit hero) {
			this.hero = hero;

			this.deck = new Deck();
			hand = new List<Card>();
			library = new List<Card>();
		}

		public void NewTurn() {
			if (hand.Count < 3) {
				deck.DrawCards (1);
			}
		}
	}
}
