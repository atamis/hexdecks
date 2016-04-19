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
		

		public HeroUnit hero { get; set; }
		public Dictionary<Card, bool> library;
		public List<Card> hand { get; set; }
		public List<Card> graveyard { get; set; }

		public Command nextCommand;
		public int turns;

		public Player(HeroUnit hero) {
			this.hero = hero;
			this.turns = 1;

			hand = new List<Card>();

			//for (int i = 0; i < 5; i++) {
			Card c = new GameObject ("Card" + 0).AddComponent<FireballCard> ();
			c.init ();
            hand.Add(c);

			Card c1 = new GameObject ("Card" + 1).AddComponent<FireballCard> ();
			c1.init ();
            hand.Add(c1);

			Card c2 = new GameObject ("Card" + 2).AddComponent<FlashHealCard> ();
			c2.init ();
            hand.Add(c2);

            Card c3 = new GameObject("Card " + 3).AddComponent<JumpAttackCard>();
            c3.init();
            hand.Add(c3);
			//}
			//library = new List<Card>();
		}



		public void NewTurn() {
		}
	}
}
