using UnityEngine;
using System.Collections;

namespace game.world.units {
	class EnemyUnit : Unit {
        public void init(WorldMap w, Hex h) {
            base.init(w, h, 2);
        }

        public override Sprite getSprite() {
            return Resources.Load<Sprite>("Sprites/Square");
        }

    }

    class MeleeEnemy : EnemyUnit {
        public override void NewTurn() {
            base.NewTurn();
        }

    }

    class RangedEnemy : EnemyUnit {
        public override void NewTurn() {
            base.NewTurn();
        }

    }
}