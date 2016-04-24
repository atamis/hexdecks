using UnityEngine;
using System.Collections;
using game.ui;

namespace game {
	class IntroManager : MonoBehaviour {
		private IntroUI ui;

		void Awake() {
			ui = gameObject.AddComponent<IntroUI> ();
			ui.init ();
		}
	}
}
