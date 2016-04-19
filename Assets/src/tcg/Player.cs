using UnityEngine;
using System.Collections.Generic;
using game.world.units;
using game.tcg.cards;

namespace game.tcg {
	[System.Serializable]
	public struct PlayerData {
		public string name;
		public List<CardData> cards;
	}
		
	class Player {			
		public List<Card> hand { get; set; }
		public List<Card> graveyard { get; set; }
		public List<Card> deck { get; set; }

		public HeroUnit hero { get; set; }
		public Command nextCommand;
		public int turns;

		public Player(HeroUnit u) {
			this.hero = u;
			this.turns = 1;

			hand = new List<Card>();
			deck = new List<Card> ();
			graveyard = new List<Card> ();

			Card c = new GameObject ("Card").AddComponent<FireballCard> ();
			c.init (this);

			Card c1 = new GameObject ("Card").AddComponent<FireballCard> ();
			c1.init (this);

			Card c2 = new GameObject ("Card").AddComponent<FlashHealCard> ();
			c2.init (this);

			hand.Add (c); hand.Add (c1); hand.Add (c2);
		}
			
		public void DiscardHand() {
			foreach (Card c in hand) {
				graveyard.Add (c);
				hand.Remove (c);
			}
		}

		public void DrawCards(int amount) {
			for (int i = 0; i < amount; i++) {
				hand.Add (deck [i]);
				deck.RemoveAt (i);
			}
		}

		public void NewTurn() {
			
		}
	}
}
