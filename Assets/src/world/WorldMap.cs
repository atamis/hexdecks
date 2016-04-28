﻿using UnityEngine;
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
		private GameObject hFolder;

		public Layout l;
		public HeroUnit hero;
        public List<EnemyUnit> enemies;
		public GameManager gm;
		public int turns { get; set; }

		public WorldMap(Layout l, GameManager gm) {
			this.l = l;
			this.gm = gm;
			this.turns = 0;

			map = new Dictionary<HexLoc, Hex> ();
			hFolder = new GameObject("Hexes");

            enemies = new List<EnemyUnit>();
		}

		public Hex addHex(HexLoc hl) {
			Hex h = new GameObject ("Hex " + hl.ToString ()).AddComponent<Hex> ();
			h.init (this, hl);

			h.transform.parent = hFolder.transform;

			map.Add(hl, h);
			return h;
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

            // Consider updating from the hero outward.
            foreach (KeyValuePair<HexLoc, Hex> kv in map)
            {
                if (!kv.Value.Updated)
                {
                    kv.Value.Updated = true;
                    kv.Value.NewTurn();
                }
            }
		}
	}
}

