using UnityEngine;
using System.Collections.Generic;

namespace game.tcg {
	static class CardLib {
		public static Dictionary<int, System.Type> card_dict;

		public static void Shuffle<T>(this IList<T> l) {
			System.Random rng = new System.Random ();

			int n = l.Count;
			while (n > 1) {
				n--;
				int k = rng.Next (n + 1);
				T value = l [k];
				l [k] = l [n];
				l [n] = value;
			}
		}

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

