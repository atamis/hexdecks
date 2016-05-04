using UnityEngine;
using System.Collections.Generic;
using game.tcg.cards;

namespace game.world.levels {
	abstract class GameLevel {
		public abstract WorldMap GetMap(GameManager gm);

		public abstract string GetSceneName();

		public abstract Sprite GetPassableSprite();

		public abstract Sprite GetImpassableSprite();

		public abstract Sprite GetWaterSprite ();

		public abstract List<TCGCard> GetDeck();

		public abstract int GetNextLevel();
	}
}

