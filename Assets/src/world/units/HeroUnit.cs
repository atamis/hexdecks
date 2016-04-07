using UnityEngine;
using System.Collections;
using game.world.math;

namespace game.world.units {
    class HeroUnit : Unit {

        public void init(WorldMap w, Hex h) {
            base.init(w, h, 20);
        }
    }
}
