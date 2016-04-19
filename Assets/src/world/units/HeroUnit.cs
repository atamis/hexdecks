using UnityEngine;

namespace game.world.units {
	class HeroUnit : Unit {
        public void init(WorldMap w, Hex h) {
            base.init(w, h, 10);
        }

        public override Sprite getSprite() {
            return Resources.Load<Sprite>("Sprites/Hero/T_HeroIdle1");
        }
    }
}
