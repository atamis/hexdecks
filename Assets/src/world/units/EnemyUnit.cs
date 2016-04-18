using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using game.math;

namespace game.world.units {
	class EnemyUnit : Unit {

        public void init(WorldMap w, Hex h) {
            base.init(w, h, 1);
        }

        public override Sprite getSprite() {
            return Resources.Load<Sprite>("Sprites/Square");
        }
    }

    class MeleeEnemy : EnemyUnit {
        private bool persuing = false;
        private HeroUnit target;

        public override Sprite getSprite()
        {
            return Resources.Load<Sprite>("Sprites/Square");
        }

        public override void NewTurn() {
            base.NewTurn();

            if (!persuing)
            {
                var hero = w.hero;
                Hex heroHex = hero.h;
                var path = WorldPathfinding.Pathfind(w, h, heroHex);

                if (path.Count <= 4)
                {
                    persuing = true;
                    target = hero;
                }
            }

            if (persuing)
            {
                var dist = h.loc.Distance(target.h.loc);
                if (dist == 1)
                {
                    target.ApplyDamage(1);
                }
                else {
                    var path = WorldPathfinding.Pathfind(w, h, target.h);
                    var next = path.First.Next.Value;
                    if (next.unit == null)
                    {
                        h = next;
                    }
                }
            }
        }

    }

    class BigMeleeEnemy: MeleeEnemy
    {
        public override Sprite getSprite()
        {
            return Resources.Load<Sprite>("Sprites/Pentagon");
        }

        public new void init(WorldMap w, Hex h)
        {
            base.init(w, h, 2);
        }
    }

    class RangedEnemy : EnemyUnit {
        private bool persuing = false;
        private HeroUnit target;

        public override Sprite getSprite() {
            return Resources.Load<Sprite>("Sprites/Triangle");
        }

        private List<Hex> ValidTargets() {
            List<Hex> targets = new List<Hex>(HexLoc.hex_directions.Length);

            foreach(HexLoc dir in HexLoc.hex_directions) {
                var nloc = h.loc + dir + dir;
                if (w.map.ContainsKey(nloc)) {
                    targets.Add(w.map[nloc]);
                }
            }

            return targets;
        }

        public override void NewTurn() {
            base.NewTurn();

            if (!persuing) {
                var hero = w.hero;
                Hex heroHex = hero.h;
                var path = WorldPathfinding.Pathfind(w, h, heroHex);

                if (path.Count <= 5)
                {
                    persuing = true;
                    target = hero;
                }
            }

            if (persuing) {
                int dist = h.loc.Distance(target.h.loc);

                var targets = ValidTargets();

                foreach(Hex t in targets) {
                    if (t.unit == target) {
                        target.ApplyDamage(1);
                        return;
                    }
                }

                if (dist == 1) {
                    var nhex = h.loc + (h.loc - target.h.loc);
                    if (w.map.ContainsKey(nhex) && w.map[nhex].Passable()) {
                        if (w.map[nhex].unit == null) {
                            h = w.map[nhex];
                        }
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
