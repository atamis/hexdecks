using UnityEngine;
using System.Collections;

namespace game.world.levels {
	abstract class BaseLevel {
		public abstract string GetSceneName();

		public abstract Sprite GetPassableSprite();

		public abstract Sprite GetImpassableSprite();

		public abstract Light GetLight();

		public abstract void GetDeck();
	}
}

