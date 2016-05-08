using UnityEngine;
using System.Collections.Generic;
using game.tcg;
using game.tcg.cards;
using System;

namespace game.world.levels {
	class CryptLevel : GameLevel {
		public static Sprite t_sprite1 = Resources.Load<Sprite>("Sprites/Tiles/T_Ground1");
		public static Sprite t_sprite1a = Resources.Load<Sprite>("Sprites/Tiles/T_Ground2");
		public static Sprite t_sprite2 = Resources.Load<Sprite>("Sprites/Tiles/T_Brick");
		public static Sprite t_water = Resources.Load<Sprite>("Sprites/Tiles/T_Water1");

		public static Sprite t_ranged1 = Resources.Load<Sprite>("Sprites/Enemies/T_BowGoblin1");
		public static Sprite t_ranged2 = Resources.Load<Sprite>("Sprites/Enemies/T_BowGoblin2");
		public static Sprite t_arrow = Resources.Load<Sprite>("Sprites/Enemies/Arrow");

		public static Sprite t_melee1 = Resources.Load<Sprite>("Sprites/Enemies/T_Bat1");
		public static Sprite t_melee2 = Resources.Load<Sprite>("Sprites/Enemies/T_Bat2");

        public override int playerMaxHealth {
            get {
                return 10;
            }
        }

        public CryptLevel() {}

		public override WorldMap GetMap(GameManager gm) {
			WorldMap map = SaveManager.LoadLevel (GameManager.l, "crypt", gm);

			return map;
		}

		public override int GetNextLevel() { return 1; }

		public override List<TCGCard> GetDeck() {
			List<TCGCard> deck = new List<TCGCard> ();

			deck.Add (new SlideCard());
			deck.Add (new SlideCard());
			deck.Add (new KnockBackCard());
			deck.Add (new KnockBackCard());
			deck.Add (new WhirlwindCard());
			deck.Add (new WhirlwindCard());
			deck.Add (new TeleportCard());
			deck.Add (new TrapCard());
			deck.Add (new DisengageCard());
			deck.Add (new JumpAttackCard());
			deck.Add (new JumpAttackCard());
			deck.Add (new BoulderCard());
			deck.Add (new BoulderCard());
			deck.Add (new FireballCard());
			deck.Add (new FireballCard());
			deck.Add (new FlashHealCard());
			deck.Add (new FlashHealCard());
			deck.Add(new DiscardHandCard());
			deck.Add(new DiscardHandCard());

			deck.Shuffle();

			return deck;
		}

		public override string GetSceneName() {
			return "Decrepit Crypt";
		}

		public override Sprite GetPassableSprite() {
			float random = UnityEngine.Random.value;
			if (random < 0.1f) {
				return t_sprite1a;
			}
			return t_sprite1;
		}

		public override Sprite[] GetRangedSprite(){
			Sprite[] sprites = new Sprite[2] {
					t_ranged1, t_ranged2
			};
			return sprites;
		}

		public override Sprite[] GetMeleeSprite(){
			Sprite[] sprites = new Sprite[2] {
				t_melee1, t_melee2
			};
			return sprites;
		}

		public override Sprite GetArrowSprite(){
			return t_arrow;
		}

		public override Sprite GetImpassableSprite() {
			return t_sprite2;
		}

		public override Sprite GetWaterSprite() {
			return t_water;
		}

        public override List<TCGCard> GetChestContents(int chestType) {
            var cards = new List<TCGCard>();

            switch (chestType) {
                case 0:
                    cards.Add(new DoubleActionCard());
                    break;
                case 1:
                    cards.Add(new WhirlwindCard());
                    break;
            }

            return cards;
        }
    }
}
