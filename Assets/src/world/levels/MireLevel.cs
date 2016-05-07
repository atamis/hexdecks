using UnityEngine;
using System.Collections.Generic;
using game.tcg.cards;

namespace game.world.levels {
	class MireLevel : GameLevel {
		public static Sprite t_sprite1 = Resources.Load<Sprite>("Sprites/Tiles/T_MireGround");
		public static Sprite t_sprite2 = Resources.Load<Sprite>("Sprites/Tiles/T_Mire2");
		public static Sprite t_water = Resources.Load<Sprite>("Sprites/Tiles/T_MireWater");

		public static Sprite t_ranged1 = Resources.Load<Sprite>("Sprites/Enemies/T_FishFlinger1");
		public static Sprite t_ranged2 = Resources.Load<Sprite>("Sprites/Enemies/T_FishFlinger2");
		public static Sprite t_arrow = Resources.Load<Sprite>("Sprites/Enemies/FlungFish");

		public MireLevel() {}

		public override int GetNextLevel() { return 2; }

		public override WorldMap GetMap(GameManager gm) {
			WorldMap map = SaveManager.LoadLevel (GameManager.l, "mire", gm);

			return map;
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

		public override Sprite GetArrowSprite(){
			return t_arrow;
		}

		public override Sprite GetPassableSprite() {
			return t_sprite1;
		}

		public override Sprite[] GetRangedSprite(){
			Sprite[] sprites = new Sprite[2] {
					t_ranged1, t_ranged2
			};
			return sprites;
		}

		public override Sprite GetImpassableSprite() {
			return t_sprite2;
		}

		public override Sprite GetWaterSprite() {
			return t_water;
		}
	}
}
