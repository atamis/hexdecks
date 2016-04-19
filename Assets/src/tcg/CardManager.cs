using UnityEngine;
using System.Collections.Generic;
using game.tcg.cards;

namespace game.tcg {
	class CardManager : MonoBehaviour {
		private Player p;

		public void init() {
			this.p = GameManager.p;
		}

		void Update() {
			// update card locations
			float offset = Screen.height * .3f;
			int i = 1;

			foreach (Card c in p.hand) {
				Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(offset * i, Screen.height / 2 - Screen.height * .4f, 1));
				c.SetOrigin (pos);
				i++;
			}
		}
	}

}


