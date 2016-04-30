using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;

namespace game.tcg.cards {
    class KnockBackCard : TCGCard {

        public override string GetName() {
            return "Knockback";
        }

        public override void OnPlay(WorldMap w, Hex h) {
            var dir = w.hero.h.loc - h.loc;

            h.unit.stunned.duration = 1;
            h.unit.ApplyDamage(1, w.hero);

            if (!w.map.ContainsKey(h.loc - dir)) {
                return;
            }

            var dest = w.map[h.loc - dir];

            if (dest.unit != null) {
                dest.unit.ApplyDamage(1, w.hero);
            } else if (h.unit != null && dest.Passable()) {
                h.unit.h = dest;
            }
        }

        public override List<Hex> ValidTargets(WorldMap w, Hex h) {
            var targets = h.Neighbors().Where((x) => x.unit != null);
            return targets.ToList();
        }

        public override string getDescription()
        {
            return "Knock adjacent enemy back 1 space, dealing 1 damage and stunning them for 1 turn.";
        }
    }
}
