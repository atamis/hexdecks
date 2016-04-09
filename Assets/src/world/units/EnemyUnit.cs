using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using game.world.math;

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
        private bool persuing = false;
        private HeroUnit target;

        public override Sprite getSprite() {
            return Resources.Load<Sprite>("Sprites/Triangle");
        }


        public override void NewTurn() {
            base.NewTurn();
            print(persuing + ", " + target);

            if (!persuing) {
                foreach (KeyValuePair<HexLoc, Hex> kv in w.map) {
                    var hex = kv.Value;
                    if (hex.unit != null &&
                        hex.unit.GetType() == typeof(HeroUnit)) {
                        var hero = (HeroUnit)hex.unit;



                        if (h.loc.Distance(hex.loc) <= 4) {
                            persuing = true;
                            target = hero;
                            break;
                        }

                    }
                }
            }

            if (persuing) {
                var dist = h.loc.Distance(target.h.loc);
                if (dist == 2) {
                    target.ApplyDamage(1);
                } else if (dist == 1) {
                    var nhex = h.loc + (h.loc - target.h.loc);
                    if (w.map.ContainsKey(nhex) && w.map[nhex].Passable()) {
                        h = w.map[nhex];
                    }
                } else {
                    var path = WorldPathfinding.Pathfind(w, h, target.h);
                    var next = path.First.Next.Value;
                    if (next.unit == null) {
                        h = next;
                    }
                }
            }


        }
    }
}