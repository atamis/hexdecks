using UnityEngine;
using System;
using System.Collections.Generic;
using game.tcg.cards;

namespace game.tcg {
	class CardManager : MonoBehaviour {

		// Card registery
		private class CardRegistery<T> {
			static readonly Dictionary<int, Func<T>> _dict = new Dictionary<int, Func<T>>();

			private CardRegistery() { }

			public static T Create(int id) {
				Func<T> constructor = null;
				if (_dict.TryGetValue (id, out constructor)) {
					return constructor();
				}
				throw new ArgumentException (String.Format("No type is registered for id {0}", 0));
			}

			public static void Register(int id, Func<T> constructor) {
				_dict.Add (id, constructor);
			}
		}

		void Awake() {
			CardRegistery<TCGCard>.Register (0, () => new FireballCard());
			CardRegistery<TCGCard>.Register (1, () => new FlashHealCard());
			CardRegistery<TCGCard>.Register (2, () => new JumpAttackCard());
		}

		public static TCGCard GetCard(int id) {
			return CardRegistery<TCGCard>.Create (id);
		}
	}
}

