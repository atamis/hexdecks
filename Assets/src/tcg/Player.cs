using UnityEngine;
using System.Collections.Generic;
using game.world.math;
using game.world.units;

namespace game.tcg {
	[System.Serializable]
	public struct PlayerData {
		public string name;
		public List<CardData> cards;
	}

	class Player {
		public HeroUnit hero { get; set; }
		public List<Card> library;


		// for rendering
		public Deck deck { get; set; }
		public List<Card> hand { get; set; }

		public Command nextCommand;

		public Player(HeroUnit hero) {
			this.hero = hero;

			this.deck = new Deck();
			hand = new List<Card>();
			for (int i = 0; i < 5; i++) {
				Card c = new GameObject ("Card" + i).AddComponent<Card> ();

				c.init ();
			}
			library = new List<Card>();

		
		}

		public void NewTurn() {
			if (hand.Count < 3) {
				deck.DrawCards (1);
			}
		}
	}
}
