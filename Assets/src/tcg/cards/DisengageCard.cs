using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;

namespace game.tcg.cards {
    class DisengageCard : TCGCard {
        public override string getDescription() {
            return "Attack 1 and move back";
        }

        public override string GetName() {
            return "Disengage";
        }

        public override void OnPlay(WorldMap wm, Hex h) {
            var dir = wm.hero.h.loc - h.loc;
            if (h.unit != null) {
                h.unit.ApplyDamage(1, wm.hero);
            }

            var destLoc = wm.hero.h.loc + dir;

            if (!wm.map.ContainsKey(destLoc)) {
                return;
            }

            var dest = wm.map[destLoc];

            if (dest.unit != null || !dest.Passable()) {
                return;
            }

            wm.hero.h = dest;
        }

        public override List<Hex> ValidTargets(WorldMap wm, Hex h) {
            var targets = h.Neighbors().Where((x) => x.unit != null);
            return targets.ToList();
        }
    }
}
