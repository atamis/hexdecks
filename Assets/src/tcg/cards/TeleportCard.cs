using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;
using game.math;

namespace game.tcg.cards {
    class TeleportCard : TCGCard {
        public override string GetName() {
            return "Teleport";
        }

        public override void OnPlay(WorldMap wm, Hex h) {
            List<Hex> dests = new List<Hex>();
            foreach(KeyValuePair<HexLoc, Hex> kv in wm.map) {
                if (h.loc.Distance(kv.Key) < 5) {
                    dests.Add(kv.Value);
                }
            }

            Hex dest;

            do {
                dest = dests.RandomElement();
            } while (!dest.Passable() || dest.unit != null);

            wm.hero.h = dest;
        }

        public override List<Hex> ValidTargets(WorldMap wm, Hex h) {
            return justHeroValid(wm, h);
        }

        public override string getDescription()
        {
            return "Teleport to a random hex up to 4 hexes away.";
        }
    }
}
