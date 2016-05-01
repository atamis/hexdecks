﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;
using game.math;

namespace game.tcg.cards {
    class SlideCard : TCGCard {
        public override string getDescription() {
            return "Slides 3 hexes";
        }

        public override string GetName() {
            return "Slide";
        }

        public override void OnPlay(WorldMap wm, Hex h) {
            if (h.unit == null) {
                wm.hero.h = h;
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

                bool good = true;

                foreach (Hex t in hexes) {
                    if (!t.Passable()) good = false;
                    if (t.unit != null) good = false;
                }

                if (good) {
                    targets.Add(dest);
                }

            }

            return targets;
        }
    }
}
