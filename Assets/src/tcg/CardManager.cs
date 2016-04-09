using UnityEngine;
using System.Collections.Generic;

namespace game.tcg {
	class CardManager : MonoBehaviour {
		List<Card> hand;

		void Awake() {
			hand = new List<Card>();
			for (int i = 0; i < 5; i++) {
				Card c = new GameObject ("Card").AddComponent<Card> ();
				c.init ();
			}
		}


		void Update() {
			
		}
	}
}
