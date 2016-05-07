﻿using UnityEngine;
using System.Collections.Generic;
using game.tcg.cards;

namespace game.world.levels {
	class CatacombLevel : GameLevel {
		public static Sprite t_sprite1 = Resources.Load<Sprite>("Sprites/Tiles/T_Ground1");
		public static Sprite t_sprite1a = Resources.Load<Sprite>("Sprites/Tiles/T_Ground2");
		public static Sprite t_sprite2 = Resources.Load<Sprite>("Sprites/Tiles/T_Brick");
		public static Sprite t_water = Resources.Load<Sprite>("Sprites/Tiles/T_Water1");

		public static Sprite t_ranged1 = Resources.Load<Sprite>("Sprites/Enemies/T_BowGoblin1");
		public static Sprite t_ranged2 = Resources.Load<Sprite>("Sprites/Enemies/T_BowGoblin2");
		public static Sprite t_arrow = Resources.Load<Sprite>("Sprites/Enemies/Arrow");
		
		public CatacombLevel() {}

		public override int GetNextLevel() { return 5; }

		public override string GetSceneName() {
			return "Lost Catacombs";
		}

		public override WorldMap GetMap(GameManager gm) {
			WorldMap map = SaveManager.LoadLevel (GameManager.l, "catacomb", null);

			return map;
		}

		public override List<TCGCard> GetDeck() {
			List<TCGCard> deck = new List<TCGCard> ();
			deck.Add (new FireballCard());

			return deck;
		}

		public override Sprite GetPassableSprite() {
			float random = Random.value;
			if (random < 0.1f) {
				return t_sprite1a;
			}
			return t_sprite1;
		}

		public override Sprite GetImpassableSprite() {
			return t_sprite2;
		}

		public override Sprite GetArrowSprite(){
			return t_arrow;
		}

		public override Sprite[] GetRangedSprite(){
			Sprite[] sprites = new Sprite[2] {
					t_ranged1, t_ranged2
			};
			return sprites;
		}

		public override Sprite GetWaterSprite() {
			return t_water;
		}
	}
}
