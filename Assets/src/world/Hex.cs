﻿/*
 * Andrew Amis, Nick Care, Robert Tomcik (2016)
 * The Hex Class
 *
 */

using UnityEngine;
using System.Collections;
using game.world.math;
using game.world.units;
using System;

namespace game.world {
	class Hex : MonoBehaviour {
		private HexModel model;
		public HexLoc loc { get; set; }
		public Unit unit { get; set; }

		public void init(HexLoc loc) {
			this.loc = loc;

			model = new GameObject ("Hex Model").AddComponent<HexModel> ();
			model.init (this);

			model.transform.parent = transform;
			transform.localPosition = GameManager.l.HexPixel (loc);
		}

		public void Select() {
			model.sr.color = Color.yellow;
		}

        public List<Hex> Neighbors() {
            List<Hex> n = new List<Hex>();

            for (int i = 0; i < 6; i++) {
                HexLoc l = loc.Neighbor(i);
                if (wm.map.ContainsKey(l)) {
                    n.Add(wm.map[l]);
                }
            }

            return n;
        }


        private class HexModel : MonoBehaviour {
			public SpriteRenderer sr;
			Hex h;

			public void init(Hex h) {
				this.h = h;
				transform.localPosition = new Vector3 (0, 0, Layer.Board);
				transform.localScale = new Vector3 (1.9f, 1.9f, 0);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load <Sprite>("Sprites/Hexagon");

			}
		}

        internal void NewTurn() {
            unit.NewTurn();
        }
    }
}