using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using game.math;

namespace game.world.units {
	abstract class EnemyUnit : Unit {
        public bool persuing = false;
        public HeroUnit target;

        public void init(WorldMap w, Hex h) {
			base.init(w, h, 1);
		}

		public override Sprite getSprite() {
			return Resources.Load<Sprite>("Sprites/Square");
		}

        public void setPersuing()
        {
            persuing = true;
            target = w.hero;
        }

        
    }

	class MeleeEnemy : EnemyUnit {

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


        public override void TurnActions() {

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
                else
                {
                    Updated = true;
                }
			}

			if (persuing)
			{
				var dist = h.loc.Distance(target.h.loc);
				if (dist == 1)
				{
					target.ApplyDamage(1, this);
                    Updated = true;
				}
				else {
					var path = WorldPathfinding.Pathfind(w, h, target.h);
					var next = path.First.Next.Value;
					if (next.unit == null)
					{
						h = next;
                        Updated = true;
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
				bool gotHit;

        public new void init(WorldMap w, Hex h)
        {
            base.init(w, h, 2);

            idx = 0;
						gotHit = false;
            lastSwitch = timer;

        }

        public override Sprite getSprite()
        {
						if(gotHit == false && health == 1){
								sprites = new Sprite[2] {
				            Resources.Load<Sprite>("Sprites/Enemies/T_GoblinIdle1"),
				            Resources.Load<Sprite>("Sprites/Enemies/T_GoblinIdle2")
				        };
								gotHit = true;

						}

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

		public override void TurnActions() {

            List<Hex> neighbs = h.Neighbors();
            List<Hex> hneighbs = w.hero.h.Neighbors();

            if (!persuing) {
				var hero = w.hero;
				Hex heroHex = hero.h;
				var path = WorldPathfinding.Pathfind(w, h, heroHex);

                if (path.Count <= 5)
				{
					persuing = true;
					target = hero;
				}
                else
                {
                    Updated = true;
                }
			}

			if (persuing) {
				int dist = h.loc.Distance(target.h.loc);

				var targets = GetAttackPattern();

				foreach(Hex t in targets) {
					if (t.unit == target) {
						target.ApplyDamage(1, this);
                        Updated = true;
						return;
					}
				}

                if (dist == 1)
                {
                    var nhex = h.loc + (h.loc - target.h.loc);
                    if (w.map.ContainsKey(nhex) && w.map[nhex].Passable())
                    {
                        if (w.map[nhex].unit == null)
                        {
                            h = w.map[nhex];
                            Updated = true;
                        }
                    }

                    if (!Updated) { 

                        bool isSafe;

                        foreach (Hex neighb in neighbs)
                        {
                            if (Updated == false)
                            {
                                isSafe = true;

                                foreach (Hex hneighb in hneighbs)
                                {
                                    if (neighb == hneighb)
                                    {
                                        isSafe = false;
                                    }
                                }

                                if (isSafe)
                                {
                                    if (neighb.unit == null && neighb.Passable())
                                    {
                                        h = neighb;
                                        Updated = true;
                                    }
                                }
                            }
                        }
                    }
                }
                else if (dist == 2)
                {
                    List<Hex> attackHexes = new List<Hex>();

                    foreach (HexLoc dir in HexLoc.hex_directions)
                    {
                        var nloc = w.hero.h.loc + dir + dir;

                        if (w.map.ContainsKey(nloc))
                        {
                            if (w.map[nloc].tileType == TileType.Normal)
                            {
                                attackHexes.Add(w.map[nloc]);
                            }
                        }
                    }

                    foreach (Hex neighbor in neighbs)
                    {
                        if (!Updated)
                        {
                            foreach (Hex t in attackHexes)
                            {
                                if (neighbor == t)
                                {
                                    if (t.unit == null)
                                    {
                                        h = t;
                                        Updated = true;
                                        continue;
                                    }
                                }
                            }
                        }
                    }
                }
                else {
                    var path = WorldPathfinding.Pathfind(w, h, target.h);
                    var next = path.First.Next.Value;
                    if (next.unit == null)
                    {
                        h = next;
                        Updated = true;
                    }
                }
			}
		}
	}

    class SummonerEnemy : EnemyUnit
    {
        public int spawnTimer;
        private const int spawnCD = 3;
        private const int maxMinions = 2;
        private List<MeleeEnemy> minions;

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
            spawnTimer = 0;
            minions = new List<MeleeEnemy>();
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

        public override List<Hex> GetAttackPattern()
        {
            List<Hex> targets = new List<Hex>();

            return targets;
        }

        public bool spawnMeleeEnemy()
        {
            foreach(MeleeEnemy e in minions.ToArray())
            {
                if(e == null)
                {
                    minions.Remove(e);
                }
            }

            if(minions.Count < maxMinions)
            {
                List<Hex> neighbs = h.Neighbors();
                List<Hex> openNeighbs = new List<Hex>(); 

                foreach(Hex neighb in neighbs)
                {
                    if(neighb.Passable() && neighb.unit == null)
                    {
                        openNeighbs.Add(neighb);
                    }
                }

                if(openNeighbs.Count > 0)
                {
                    System.Random rng = new System.Random();
                    int i = rng.Next(openNeighbs.Count);

                    MeleeEnemy minion = new GameObject("MeleeEvilTim").AddComponent<MeleeEnemy>();
                    minion.init(w, openNeighbs[i]);
                    minion.setPersuing();
                    minions.Add(minion);
                    return true;
                }
                else
                {
                    return false;
                }   
            }
            else
            {
                return false;
            }
        }

        public override void TurnActions()
        {
            List<Hex> neighbs = h.Neighbors();

            if (!persuing)
            {
                var hero = w.hero;
                Hex heroHex = hero.h;
                var path = WorldPathfinding.Pathfind(w, h, heroHex);

                if (path.Count <= 5)
                {
                    persuing = true;
                    target = hero;
                }
            }

            if (persuing)
            {
                if(spawnTimer == 0)
                {
                    if (spawnMeleeEnemy())
                    {
                        spawnTimer = spawnCD;
                        Updated = true;
                        return;
                    }
                }

                Debug.Log("hit");

                var dist = h.loc.Distance(target.h.loc);
                foreach(Hex neighb in neighbs)
                {

                    Debug.Log("hit1");
                    if (!Updated)
                    {
                        Debug.Log("hit2");
                        if (neighb.Passable() && neighb.loc.Distance(target.h.loc) > dist)
                        {
                            Debug.Log("hit3");
                            if (neighb.unit == null)
                            {
                                Debug.Log("hit4");
                                h = neighb;
                                Updated = true;
                            }
                        }
                    }
                }
            }
        }
    }
}
