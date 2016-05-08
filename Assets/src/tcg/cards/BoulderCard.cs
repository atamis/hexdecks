using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;
using UnityEngine;
using game.world.units;

namespace game.tcg.cards {
    class BoulderCard : TCGCard {
        public override string getDescription() {
            return "Summons a boulder with 3 health";
        }

        public override string GetName() {
            return "Boulder";
        }

        public override void OnPlay(WorldMap wm, Hex h) {
            if (h.unit != null) {
                h.unit.health = 0;
                h.unit.CheckDeath();
            }

            var boulder = new GameObject("Boulder").AddComponent<BoulderUnit>();
            boulder.init(wm, h);

            Debug.Log("Hex: " + h);
            Debug.Log("Hex.unit: " + h.unit);
        }

        public override List<Hex> ValidTargets(WorldMap wm, Hex h) {
            return h.Neighbors().Where(n => n.Passable()).ToList();
        }
    }
}
