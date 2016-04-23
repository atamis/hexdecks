using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game.tcg {
    static class CardLib {
        public static void Shuffle<T>(this List<T> l) {
            System.Random rng = new System.Random();

            int n = l.Count;
            while (n > 1) {
                n--;
                int k = rng.Next(n + 1);
                T value = l[k];
                l[k] = l[n];
                l[n] = value;
            }
        }

        public static T RandomElement<T>(this List<T> l) {
            System.Random rng = new System.Random();
            int i = rng.Next(l.Count);
            return l[i];
        }

    }
}
