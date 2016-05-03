using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;
using game.math;

namespace game.tcg.cards {
    class SlideCard : TCGCard {
        public override string getDescription() {
            return "Slides 3 hexes and attacks forwards";
        }

        public override string GetName() {
            return "Slide";
        }

        public override void OnPlay(WorldMap wm, Hex h) {
            var dir = (h.loc - wm.hero.h.loc).Normalize();

            if (h.unit == null) {
                wm.hero.h = h;
            }

            var t_loc = h.loc + dir;
            UnityEngine.Debug.Log(t_loc);
            if (wm.map.ContainsKey(t_loc)) {
                var t_h = wm.map[t_loc];
                if (t_h.unit != null) {
                    t_h.unit.ApplyDamage(1, wm.hero);
                }
            }
        }
        public override List<Hex> ValidTargets(WorldMap w, Hex h) {
            List<Hex> targets = new List<Hex>(HexLoc.hex_directions.Length);

            foreach (HexLoc dir in HexLoc.hex_directions) {
                var loc1 = h.loc + dir;
                var loc2 = loc1 + dir;
                var loc3 = loc2 + dir;

                if (!w.map.ContainsKey(loc1) || !w.map.ContainsKey(loc2) || !w.map.ContainsKey(loc3)) {
                    continue;
                }

                var h1 = w.map[loc1];
                var h2 = w.map[loc2];
                var h3 = w.map[loc3];

                var dest = h3;
                var hexes = new Hex[] { h1, h2, h3 };
                
                foreach (Hex t in hexes) {
                    if (!t.Passable()) break;
                    if (t.unit != null) break;
                    targets.Add(t);
                }

            }

            return targets;
        }
    }
}
