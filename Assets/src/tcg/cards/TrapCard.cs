using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;
using UnityEngine;

namespace game.tcg.cards {
    class TrapCard : TCGCard {
        public override string GetName() {
            return "Trap Card";
        }

        public override void OnPlay(WorldMap wm, Hex h) {
            new GameObject("Trap Card").AddComponent<TrapTrigger>().init(h);
        }

        public override List<Hex> ValidTargets(WorldMap wm, Hex h) {
            return justHeroValid(wm, h);
        }
    }
}
