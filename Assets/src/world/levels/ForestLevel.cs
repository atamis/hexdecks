using UnityEngine;
using System.Collections.Generic;
using game.tcg.cards;
using game.world.triggers;
using game.math;

namespace game.world.levels {
	class ForestLevel : GameLevel {
		public static Sprite t_sprite1 = Resources.Load<Sprite>("Sprites/Tiles/T_Forest");
		public static Sprite t_sprite2 = Resources.Load<Sprite>("Sprites/Tiles/T_Mushroomed");
		public static Sprite t_water = Resources.Load<Sprite>("Sprites/Tiles/T_Water1");
		//public static Sprite t_melee1;

		public ForestLevel() {}

		public override string GetSceneName() {
			return "Faerie Forest";
		}

		public override int GetNextLevel() { return 1; }

		public override WorldMap GetMap(GameManager gm) {
			WorldMap map = SaveManager.LoadLevel (GameManager.l, "forest", gm);

			// TRIGGER 1
			TutorialTrigger tt0 = new GameObject ("Tutorial Trigger").AddComponent<TutorialTrigger> ();
			tt0.init (map.map [new HexLoc (4, 1, -5)]);

			// TRIGGER 2
			//TutorialTrigger tt1 = new GameObject ("Tutorial Trigger").AddComponent<TutorialTrigger> ();
			//tt1.init (map.map [new HexLoc (4, 1, -5)]);

			return map;
		}
			
		public override List<TCGCard> GetDeck() {
			List<TCGCard> deck = new List<TCGCard> ();

			return deck;
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
