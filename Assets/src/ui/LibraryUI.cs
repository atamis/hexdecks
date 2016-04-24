using UnityEngine;
using System.Collections;

namespace game.ui {
	class LibraryUI : MonoBehaviour {
		public void init(Player p) {

		}

		private class LibraryCard : MonoBehaviour {
			private SpriteRenderer sr;

			private enum CardState {
				InDeck,
			};

			public void init() {
				sr = gameObject.AddComponent<SpriteRenderer> ();
			}
		}
	}
}

