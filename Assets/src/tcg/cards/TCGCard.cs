using UnityEngine;
using System.Collections.Generic;
using game.world;

namespace game.tcg.cards {
	abstract class TCGCard {
		public TCGCard() {

		}

		public abstract List<Hex> ValidTargets(WorldMap wm, Hex h);

        internal List<Hex> justHeroValid(WorldMap wm, Hex h) {
            List<Hex> tmp = new List<Hex>();
            tmp.Add(wm.hero.h);
            return tmp;
        }

		public abstract void OnPlay(WorldMap wm, Hex h);

        public abstract string getDescription();

		public abstract string GetName();

        public void Combo() {
            GameManager.p.turns += 1;
        }
	}
}

