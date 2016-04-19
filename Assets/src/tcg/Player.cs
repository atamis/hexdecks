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

			Card c = new GameObject ("Card" + 0).AddComponent<FireballCard> ();
			c.init (this);
			hand.Add(c);

			Card c1 = new GameObject ("Card" + 1).AddComponent<FireballCard> ();
			c1.init (this);
			hand.Add(c1);

			Card c2 = new GameObject ("Card" + 2).AddComponent<FlashHealCard> ();
			c2.init (this);
			hand.Add(c2);

			Card c3 = new GameObject("Card " + 3).AddComponent<JumpAttackCard>();
			c3.init(this);
			hand.Add(c3);

			Card c4 = new GameObject("Card " + 4).AddComponent<KnockBackCard>();
			c4.init(this);
			hand.Add(c4);

			//
			Card c5 = new GameObject("Card " + 4).AddComponent<KnockBackCard>();
			c5.init(this);
			deck.Add(c5);

			/*
			Card c6 = new GameObject ("Card" + 0).AddComponent<FireballCard> ();
			c6.init (this);
			deck.Add(c6);

			Card c7 = new GameObject ("Card" + 0).AddComponent<FireballCard> ();
			c7.init (this);
			deck.Add(c7);
			*/
		}

		public void DiscardHand() {
			foreach (Card c in hand) {
				graveyard.Add (c);
				hand.Remove (c);
			}
		}

		public void DrawCards(int amount) {
			if (amount > deck.Count) {
				foreach (Card c in graveyard) {
					c.gameObject.SetActive (true);
					deck.Add (c);
				}
				//graveyard.Clear ();
				//CardLib.Shuffle (deck);
			}

			for (int i = 0; i < amount; i++) {
				hand.Add (deck [i]);
				deck.RemoveAt (i);
			}
		}

		public void NewTurn() {
			
		}
	}
}
