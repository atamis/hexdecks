using UnityEngine;
using System.Collections.Generic;
using game.tcg.cards;
using game.world;
using game.world.units;
using game.tcg;
using game.ui;

namespace game {
    class Player {
        public List<TCGCard> hand { get; set; }
        public List<TCGCard> graveyard { get; set; }
        public List<TCGCard> deck { get; set; }

        public HeroUnit hero { get; set; }
        public Command nextCommand;
        public int turns;

        public Player(HeroUnit u) {
            this.hero = u;
            this.turns = 1;

            hand = new List<TCGCard>();
            deck = new List<TCGCard>();
            graveyard = new List<TCGCard>();

            deck.Add(new FireballCard());
            deck.Add(new FireballCard());
            deck.Add(new FlashHealCard());
            deck.Add(new JumpAttackCard());

            DrawCards(3);
        }

        public void DiscardHand() {
            while (hand.Count > 0) {
                var c = hand[0];
                graveyard.Add(c);
                hand.Remove(c);
            }
        }

        public void DrawCard() {
            if (deck.Count == 0) {
                foreach (TCGCard c in graveyard) {
                    deck.Add(c);
                }
                graveyard.Clear();
            }
            hand.Add(deck[0]);
            deck.RemoveAt(0);
        }

        public void DrawCards(int amount) {
            for (int i = 0; i < amount; i++) {
                DrawCard();
            }
        }

        public void NewTurn() {

        }

        internal void Play(TCGCard c, Hex h) {
            nextCommand = new CardCommand(c, h);
            hand.Remove(c);
            graveyard.Add(c);
            DrawCards(1);
        }

    }
}
