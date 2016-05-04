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
            var trap = new GameObject("Trap Card").AddComponent<TrapTrigger>();
            trap.init(h);
            wm.triggers.Add(trap);
        }

        public override List<Hex> ValidTargets(WorldMap wm, Hex h) {
            return h.Neighbors();
        }

        public override string getDescription()
        {
            return "Place a trap which deals 1 damage around it.";
        }
    }
}
