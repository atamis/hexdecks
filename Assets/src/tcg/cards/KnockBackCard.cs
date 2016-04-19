using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;

namespace game.tcg.cards {
    class KnockBackCard : Card {
        public override string GetCardText() {
            return "Attack and knock back 1";
        }

        public override string GetName() {
            return "Knockback";
        }

        public override void OnPlay(WorldMap w, Hex h) {
            var dir = w.hero.h.loc - h.loc;

            if (!w.map.ContainsKey(h.loc + dir)) {
                throw new Exception("Hex doesn't exist");
            }

            var dest = w.map[h.loc + dir];

            h.unit.ApplyDamage(1);

            if (dest.unit != null) {
                dest.unit.ApplyDamage(1);
            } else if (h.unit != null && dest.Passable()) {
                print("moving");
                h.unit.h = dest;
            }
        }

        public override List<Hex> ValidTargets(WorldMap w, Hex h) {
            var targets = h.Neighbors().Where((x) => x.unit != null);
            return targets.ToList();
        }
    }
}
