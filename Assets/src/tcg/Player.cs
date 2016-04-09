using UnityEngine;
using System.Collections.Generic;
using game.world.math;
using game.world.units;

namespace game.tcg {
	class Player {
        public List<Card> library;
        
        public Deck deck { get; set; }
        public List<Card> hand { get; set; }

        public HeroUnit hero { get; set; }

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
