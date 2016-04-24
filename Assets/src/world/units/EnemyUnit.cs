using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using game.math;

namespace game.world.units {
	abstract class EnemyUnit : Unit {
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

        Sprite[] sprites = new Sprite[2] {
            Resources.Load<Sprite>("Sprites/Enemies/T_GoblinIdle1"),
            Resources.Load<Sprite>("Sprites/Enemies/T_GoblinIdle2")
        };
        const float spriteInterval = .8f;
        float lastSwitch;
        int idx;

        public new void init(WorldMap w, Hex h)
        {
            base.init(w, h, 1);

            idx = 0;
            lastSwitch = timer;

        }

        public override Sprite getSprite()
        {
            if (timer >= lastSwitch + spriteInterval)
            {
                lastSwitch = timer;
                idx = idx + 1;
                idx = idx % 2;
            }
            return sprites[idx];
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

		public override List<Hex> GetAttackPattern() {
			List<Hex> targets = new List<Hex> ();
			foreach (Hex neighbor in h.Neighbors()) {
				if (neighbor.tileType == TileType.Normal) {
					targets.Add (neighbor);
				}
			}
			return targets;
		}
	}

	class BigMeleeEnemy: MeleeEnemy
	{
        Sprite[] sprites = new Sprite[2] {
            Resources.Load<Sprite>("Sprites/Enemies/T_ShieldGoblinIdle1"),
            Resources.Load<Sprite>("Sprites/Enemies/T_ShieldGoblinIdle2")
        };
        const float spriteInterval = .8f;
        float lastSwitch;
        int idx;

        public new void init(WorldMap w, Hex h)
        {
            base.init(w, h, 2);

            idx = 0;
            lastSwitch = timer;

        }

        public override Sprite getSprite()
        {
            if (timer >= lastSwitch + spriteInterval)
            {
                lastSwitch = timer;
                idx = idx + 1;
                idx = idx % 2;
            }
            return sprites[idx];
        }
    }

	class RangedEnemy : EnemyUnit {
		private bool persuing = false;
		private HeroUnit target;

        Sprite[] sprites = new Sprite[2] {
            Resources.Load<Sprite>("Sprites/Enemies/T_BowGoblinIdle1"),
            Resources.Load<Sprite>("Sprites/Enemies/T_BowGoblinIdle2")
        };
        const float spriteInterval = .8f;
        float lastSwitch;
        int idx;

        public new void init(WorldMap w, Hex h)
        {
            base.init(w, h, 1);

            idx = 0;
            lastSwitch = timer;

        }

        public override Sprite getSprite()
        {
            if (timer >= lastSwitch + spriteInterval)
            {
                lastSwitch = timer;
                idx = idx + 1;
                idx = idx % 2;
            }
            return sprites[idx];
        }

        public override List<Hex> GetAttackPattern() {
			List<Hex> targets = new List<Hex> ();

			foreach (HexLoc dir in HexLoc.hex_directions) {
                var loc1 = h.loc + dir;
				var nloc = h.loc + dir + dir;

                if (w.map.ContainsKey(loc1)) {
                    if (w.map[loc1].tileType == TileType.Wall) {
                        continue;
                    }
                } else {
                    continue;
                }

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

				var targets = GetAttackPattern();

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

