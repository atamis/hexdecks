using UnityEngine;
using System;
using System.Collections.Generic;
using game.world.units;
using game.math;

namespace game.world {
	[System.Serializable]
	struct WorldData {
		public List<HexData> hdata;
	}

	class WorldMap {
		public Dictionary<HexLoc, Hex> map;
        public Dictionary<HexLoc, Hex> noWallMap;
        private GameObject hFolder;

		public Layout l;
		public HeroUnit hero;
        public List<EnemyUnit> enemies;
        public List<SummonerEnemy> summoners;
        public List<Trigger> triggers;
		public GameManager gm;
		public int turns { get; set; }

		public WorldMap(Layout l, GameManager gm) {
			this.l = l;
			this.gm = gm;
			this.turns = 0;

			map = new Dictionary<HexLoc, Hex> ();
            noWallMap = new Dictionary<HexLoc, Hex>();
            hFolder = new GameObject("Hexes");

            enemies = new List<EnemyUnit>();
            summoners = new List<SummonerEnemy>();
            triggers = new List<Trigger>();
		}

		public Hex addHex(HexLoc hl) {
			Hex h = new GameObject ("Hex " + hl.ToString ()).AddComponent<Hex> ();
			h.init (this, hl);

			h.transform.parent = hFolder.transform;

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

			foreach (KeyValuePair<HexLoc, Hex> kv in map) {
				kv.Value.Updated = false;
			}

            bool notDone = true;
            while (notDone)
            {
                notDone = false;

                foreach (EnemyUnit e in enemies)
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
            }

            foreach (EnemyUnit e in enemies)
            {
                e.Updated = true;
            }

            foreach (SummonerEnemy s in summoners)
            {
                if(s.spawnTimer > 0)
                {
                    s.spawnTimer -= 1;
                }
            }

            // Consider updating from the hero outward.
            foreach (Trigger t in triggers)
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

