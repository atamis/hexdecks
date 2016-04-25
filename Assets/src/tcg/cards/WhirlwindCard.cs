using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using game.world;

namespace game.tcg.cards {
    class WhirlwindCard : TCGCard {

        public override string GetName() {
            return "Whirlwind";
        }

        public override void OnPlay(WorldMap w, Hex h) {

            var targets = h.Neighbors().Where((x) => x.unit != null);

            foreach(Hex hex in targets)
            {
                hex.unit.ApplyDamage(1);
            }
            
        }

        public override List<Hex> ValidTargets(WorldMap w, Hex h) {
            var targets = h.Neighbors().Where((x) => x.unit != null);
            if(targets.ToList().Count > 0)
            {
                return new List<Hex>() { h };
            }
            else
            {
                return targets.ToList();
            }
        }

        public override string getDescription()
        {
            return "Deal 1 damage to all adjacent enemies.";
        }
    }
}
