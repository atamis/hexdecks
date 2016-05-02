﻿using UnityEngine;
using System.Collections.Generic;
using game.tcg.cards;

namespace game.world.levels {
	class MireLevel : GameLevel {
		public static Sprite t_sprite1 = Resources.Load<Sprite>("Sprites/Tiles/T_MireGround");
		public static Sprite t_sprite2 = Resources.Load<Sprite>("Sprites/Tiles/T_Mire2");
		public static Sprite t_water = Resources.Load<Sprite>("Sprites/Tiles/T_Water");

		public MireLevel() {}

		public override WorldMap GetMap(GameManager gm) {
			WorldMap map = SaveManager.LoadLevel (GameManager.l, "mire", gm);

			return map;
		}

		public override Light GetLight() {
			Light l = new GameObject ("Light").AddComponent<Light> ();
			l.color = new Color (1, 1, 1);

			return l;
		}

		public override List<TCGCard> GetDeck() {
			List<TCGCard> deck = new List<TCGCard> ();
			deck.Add (new FireballCard());
			deck.Add (new FireballCard());

			return deck;
		}

		public override string GetSceneName() {
			return "Twilight Mire";
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

		public override void GetNextLevel() {

		}
	}
}

