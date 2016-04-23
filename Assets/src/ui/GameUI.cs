using UnityEngine;
using System.Collections;

namespace game.ui {
	abstract class GameUI : MonoBehaviour {
		public static Font font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");
		internal GameCamera gc;

		public abstract void init(GameManager gm);
	}
}

