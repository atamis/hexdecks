﻿using UnityEngine;
using System.Collections.Generic;
using game.tcg.cards;

namespace game.world.levels {
	abstract class GameLevel {
		public abstract WorldMap GetMap(GameManager gm);

		public abstract string GetSceneName();

		public abstract Sprite GetPassableSprite();

		public abstract Sprite GetImpassableSprite();

		public abstract Sprite GetWaterSprite ();

		public abstract Sprite[] GetRangedSprite();

		public abstract Sprite[] GetMeleeSprite();

		public abstract List<TCGCard> GetDeck();

		public abstract Sprite GetArrowSprite();

		public abstract int GetNextLevel();
        public abstract List<TCGCard> GetChestContents(int chestType);

        public abstract int playerMaxHealth {
            get;
        }
    }
}
