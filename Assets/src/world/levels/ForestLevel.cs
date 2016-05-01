using UnityEngine;
using System.Collections;

namespace game.world.levels {
	class ForestLevel : BaseLevel {
		public static Sprite t_sprite1 = Resources.Load<Sprite>("Sprites/Tiles/T_Forest1");
		public static Sprite t_sprite2 = Resources.Load<Sprite>("Sprites/Tiles/T_Forest1");
		public static Sprite t_water = Resources.Load<Sprite>("Sprites/Tiles/T_Forest1");

		public static WorldMap Load() {
			WorldMap map = SaveManager.LoadLevel (GameManager.l, "forest.txt", null);

			return map;
		}

		public override Light GetLight() {
			Light l = new GameObject ("Light").AddComponent<Light> ();
			l.color = new Color (1, 1, 1);

			return l;
		}

		public override void GetDeck() {
			return;
		}

		public override string GetSceneName() {
			return "Faerie Forest";
		}

		public override Sprite GetPassableSprite() {
			return t_sprite1;
		}

		public override Sprite GetImpassableSprite() {
			return t_sprite2;
		}

		public void GetNextLevel() {

		}
	}
}
