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

			for (int i = 0; i < 5; i++) {
				Card c = new GameObject ("Card" + i).AddComponent<FireballCard> ();
				c.init ();

				Card c1 = new GameObject ("Card" + i).AddComponent<FireballCard> ();
				c1.init ();

				Card c2 = new GameObject ("Card" + i).AddComponent<FlashHealCard> ();
				c2.init ();
			}
			//library = new List<Card>();
		}



		public void NewTurn() {
		}
	}
}
