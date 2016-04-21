using UnityEngine;
using System.Collections.Generic;
using game.world;

namespace game.tcg.cards {
	abstract class TCGCard {
		public TCGCard() {

		}

		public abstract List<Hex> ValidTargets(WorldMap wm, Hex h);

		public abstract void OnPlay(WorldMap wm, Hex h);

		public abstract string GetName();
	}
}

