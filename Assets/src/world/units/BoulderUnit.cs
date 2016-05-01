using System;
using System.Collections.Generic;
using UnityEngine;

namespace game.world.units {
    class BoulderUnit : Unit {

        public void init(WorldMap w, Hex h) {
            base.init(w, h, 3);
        }

        public override List<Hex> GetAttackPattern() {
            return new List<Hex>();
        }

        public override Sprite getSprite() {
            return Resources.Load<Sprite>("Sprites/Diamond");
        }

    }
}
