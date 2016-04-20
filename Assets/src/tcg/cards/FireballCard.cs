using UnityEngine;
using System.Collections.Generic;
using game.world;

namespace game.tcg.cards {
	class FireballCard : TCGCard {

		public override string GetTitle ()
		{
			return "Fireball";
		}
		
		public override List<Hex> ValidTargets () {
			throw new System.NotImplementedException ();
		}

		public override void OnPlay () {
			throw new System.NotImplementedException ();
		}
	}
}

