using UnityEngine;
using System.Collections;

namespace game.world.levels {
	abstract class BaseLevel {
		private Light light { get; set; }

		public abstract string GetSceneName();


	}
}

