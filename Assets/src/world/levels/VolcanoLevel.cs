﻿using UnityEngine;
using System.Collections.Generic;
using game.tcg;
using game.tcg.cards;

namespace game.world.levels {
	class VolcanoLevel : GameLevel {
		public static Sprite t_sprite1 = Resources.Load<Sprite>("Sprites/Tiles/T_DarkEarth");
		public static Sprite t_sprite2 = Resources.Load<Sprite>("Sprites/Tiles/T_Brick");
		public static Sprite t_water = Resources.Load<Sprite>("Sprites/Tiles/T_Magma");

		public VolcanoLevel() { }

		public override int GetNextLevel() { return 4; }

		public override WorldMap GetMap(GameManager gm) {
			WorldMap map = SaveManager.LoadLevel (GameManager.l, "volcano", gm);

			return map;
		}

		public override List<TCGCard> GetDeck() {
			List<TCGCard> deck = new List<TCGCard> ();

			deck.Add(new SlideCard());
			deck.Add(new SlideCard());
			deck.Add(new KnockBackCard());
			deck.Add(new KnockBackCard());
			deck.Add(new WhirlwindCard());
			deck.Add(new TeleportCard());
			deck.Add(new TrapCard());
			deck.Add(new DisengageCard());
			deck.Add(new JumpAttackCard());
			deck.Add(new JumpAttackCard());
			deck.Add(new BoulderCard());
			deck.Add(new BoulderCard());
			deck.Add (new FireballCard());
			deck.Add (new FireballCard());

			deck.Shuffle();

			return deck;
		}

		public override string GetSceneName() {
			return "Subsea Volcano";
		}

		public override Sprite GetPassableSprite() {
			return t_sprite1;
		}

		public override Sprite GetImpassableSprite() {
			return t_sprite2;
		}

		public override Sprite GetWaterSprite() {
			return t_water;
		}
	}
}

