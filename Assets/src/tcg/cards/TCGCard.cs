using UnityEngine;
using System.Collections.Generic;
using game.world;

namespace game.tcg.cards {
	abstract class TCGCard {
		

		public TCGCard() {

		}

		public abstract List<Hex> ValidTargets();

		public abstract void OnPlay();

		public abstract string GetTitle();
	}
}

