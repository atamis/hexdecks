using UnityEngine;
using System.Collections;

namespace game.common {
	class WorldEventArgs : System.EventArgs {
		public int turn;
	}

	static class EventManager {
		public delegate void WorldEventHandler(WorldEventArgs args);
		public static WorldEventHandler WorldEvent;
	}
}

