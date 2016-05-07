using UnityEngine;
using System.Collections.Generic;
using game.tcg.cards;

namespace game.world.levels {
	class VolcanoLevel : GameLevel {
		public static Sprite t_sprite1 = Resources.Load<Sprite>("Sprites/Tiles/T_DarkEarth");
		public static Sprite t_sprite2 = Resources.Load<Sprite>("Sprites/Tiles/T_Brick");
		public static Sprite t_water = Resources.Load<Sprite>("Sprites/Tiles/T_Magma");

		public static Sprite t_ranged1 = Resources.Load<Sprite>("Sprites/Enemies/T_BowGoblin1");
		public static Sprite t_ranged2 = Resources.Load<Sprite>("Sprites/Enemies/T_BowGoblin2");
		public static Sprite t_arrow = Resources.Load<Sprite>("Sprites/Enemies/Arrow");

		public VolcanoLevel() { }

		public override int GetNextLevel() { return 4; }

		public override WorldMap GetMap(GameManager gm) {
			WorldMap map = SaveManager.LoadLevel (GameManager.l, "volcano", gm);

			return map;
		}

		public override List<TCGCard> GetDeck() {
			List<TCGCard> deck = new List<TCGCard> ();
			deck.Add (new FireballCard());
			deck.Add (new FireballCard());

			return deck;
		}

		public override Sprite[] GetRangedSprite(){
			Sprite[] sprites = new Sprite[2] {
					t_ranged1, t_ranged2
			};
			return sprites;
		}

		public override string GetSceneName() {
			return "Subsea Volcano";
		}

		public override Sprite GetArrowSprite(){
			return t_arrow;
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
