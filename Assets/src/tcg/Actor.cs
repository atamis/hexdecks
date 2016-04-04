using UnityEngine;
using System.Collections.Generic;
using game.world.math;
using game.world.units;

namespace game.tcg {
	class Actor {
		public Deck deck { get; set; }
		public List<Card> hand { get; set; }

		public string name { get; set; }
		public int pool { get; set; }

		public HeroUnit hero { get; set; }

		public Actor(string name) {
			this.name = name; 

			this.deck = new Deck ();
		}

		public void EnterBattle() {
			this.pool = 10;
		}

		public virtual bool PlayCard(Card c, HexLoc hl) {
			return false; // return false when the card isn't played
		}
	}
}

