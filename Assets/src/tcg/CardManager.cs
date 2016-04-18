using UnityEngine;
using System.Collections.Generic;
using game.tcg.cards;

namespace game.tcg {
	class CardManager {
		public static Dictionary<int, System.Type> card_dict;

		/*
		public static void RegisterCard<T>(int id) where T : Card {
			card_dict.Add (id, typeof(T));
		}

		public static Card CreateCard(int id) {
			var a = card_dict [id];
			var c = new GameObject ("Card").AddComponent<a.GetType()>();
		}
		*/
	}

}


