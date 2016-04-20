using UnityEngine;
using System.Collections;

namespace game.world.units {
	[System.Serializable]
	public struct UnitData {
		public int id;
	}

	class Unit {
		UnitModel model;

		private class UnitModel : MonoBehaviour {

		}
	}

	class UnitEffect {
		public int duration;

		public UnitEffect() {

		}

		public bool isActive() {
			return duration > 0;
		}

		internal void NewTurn() {
			duration--;
		}
	}
}


