using UnityEngine;
using System.Collections.Generic;
using game.tcg;
using game.tcg.cards;
using game.world.triggers;
using game.math;

namespace game.world.levels {
	class ForestLevel : GameLevel {
		public static Sprite t_sprite1 = Resources.Load<Sprite>("Sprites/Tiles/T_Forest");
		public static Sprite t_sprite2 = Resources.Load<Sprite>("Sprites/Tiles/T_Mushroomed");
		public static Sprite t_water = Resources.Load<Sprite>("Sprites/Tiles/T_Water1");

		public static Sprite t_ranged1 = Resources.Load<Sprite>("Sprites/Enemies/T_BowGoblin1");
		public static Sprite t_ranged2 = Resources.Load<Sprite>("Sprites/Enemies/T_BowGoblin2");
		public static Sprite t_arrow = Resources.Load<Sprite>("Sprites/Enemies/Arrow");

		public static Sprite t_melee1 = Resources.Load<Sprite>("Sprites/Enemies/T_MushBoroom1");
		public static Sprite t_melee2 = Resources.Load<Sprite>("Sprites/Enemies/T_MushBoroom2");

		public static Sprite[] t_melee = new Sprite[] {
			Resources.Load<Sprite>("Sprites/Units/T_Mushboroom0"),
			Resources.Load<Sprite>("Sprites/Units/T_Mushboroom1"),
			Resources.Load<Sprite>("Sprites/Units/T_Mushboroom2"),
		};

		public ForestLevel() {}

		public override string GetSceneName() {
			return "Faerie Forest";
		}

		public override int GetNextLevel() { return 1; }

		public override WorldMap GetMap(GameManager gm) {
			WorldMap world = SaveManager.LoadLevel (GameManager.l, "forest", gm);

			// TUTORIAL TRIGGERS
			int i = 0;
			HexLoc[] tlocs = new HexLoc[] {
				new HexLoc (4, 1, -5),
				new HexLoc (3, 4, -7),
				new HexLoc (13, 11, -24), // tactical manuvers
				new HexLoc (22, 2, -24), // intro to cards
				new HexLoc (23, 1, -24), // intro to
				new HexLoc (37, 12, -49),
			};

			foreach (HexLoc hl in tlocs) {
				TutorialTrigger tt = new GameObject("Tutorial Trigger").AddComponent<TutorialTrigger>();
				tt.init(world.map[hl]);
				tt.id = i;

				i++;
			}

			return world;
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

			deck.Shuffle();

			return deck;
		}

		public override Sprite GetPassableSprite() {
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

		public override Sprite[] GetMeleeSprite(){
			Sprite[] sprites = new Sprite[2] {
				t_melee1, t_melee2
			};
			return sprites;
		}

		public override Sprite GetWaterSprite() {
			return t_water;
		}
	}
}
