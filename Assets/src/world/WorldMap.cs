using UnityEngine;
using System;
using System.Collections.Generic;
using game.world.units;
using game.math;
using game.world.triggers;

namespace game.world {
	[System.Serializable]
	struct WorldData {
		public List<HexData> hdata;
	}

	class WorldMap {
		public Dictionary<HexLoc, Hex> map;
		public GameObject hexes;
        public Dictionary<HexLoc, Hex> noWallMap;
		public Layout l;
		public HeroUnit hero;
        public List<EnemyUnit> enemies;
        public List<SummonerEnemy> summoners;
        public bool alerted;
        public List<Trigger> triggers;
		public GameManager gm;
		public int turns { get; set; }

		public WorldMap(Layout l, GameManager gm) {
			this.l = l;
			this.gm = gm;
			this.turns = 0;

			map = new Dictionary<HexLoc, Hex> ();
			hexes = new GameObject("Hexes");
            noWallMap = new Dictionary<HexLoc, Hex>();

            enemies = new List<EnemyUnit>();
            summoners = new List<SummonerEnemy>();
            triggers = new List<Trigger>();
            alerted = false;
		}

		public Hex addHex(HexLoc hl) {
			Hex h = new GameObject ("Hex " + hl.ToString ()).AddComponent<Hex> ();
			h.init (this, hl);

			h.transform.parent = hexes.transform;

			map.Add(hl, h);
			return h;
		}

        public void setNoWallMap()
        {
            foreach (KeyValuePair<HexLoc, Hex> kv in map)
            {
                if (kv.Value.tileType != TileType.Wall)
                {
                    noWallMap.Add(kv.Key, kv.Value);
                }
            }
        }

		internal void NewTurn() {

            /*foreach (KeyValuePair<HexLoc, Hex> kv in map) {
				kv.Value.Updated = false;
			}*/

            foreach(EnemyUnit e in enemies) {
                e.Updated = false;
            }

            foreach (Trigger t in triggers) {
                t.h.Updated = false;
            }


            bool notDone = true;
             
            while (notDone) {
                notDone = false;
                for (int i = 0; i < enemies.Count; i++) {
                    if (enemies[i].Updated == false) {
                        enemies[i].NewTurn();

                        if (enemies[i].Updated == true) {
                            notDone = true;
                            break;
                        }
                    }
                }
            }
            

            /*bool notDone = true;
            while (notDone)
            {
                notDone = false;

                foreach (EnemyUnit e in enemies.ToArray())
                {
                    if (e.Updated == false)
                    {
                        e.NewTurn();
                        if(e.Updated == true)
                        {
                            notDone = true;
                        }
                    }
                }
            }*/

            if (alerted)
            {
                AudioManager.audioS.PlayOneShot(AudioManager.aggroSound, 2f);
                alerted = false;
            }

            foreach (EnemyUnit e in enemies)
            {
                e.Updated = true;
                e.BuffUpdate();
            }

            foreach (SummonerEnemy s in summoners)
            {
                if(s.spawnTimer > 0)
                {
                    s.spawnTimer -= 1;
                }
            }

            // Consider updating from the hero outward.
            foreach (Trigger t in triggers.ToArray())
            {
                if (!t.h.Updated)
                {
                    t.h.Updated = true;
                    t.h.NewTurn();
                }
            }
		}
	}
}

