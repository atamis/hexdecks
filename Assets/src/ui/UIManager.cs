using game.tcg;
using game.world;
using game.world.math;
using game.world.units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.ui {
    class UIManager : MonoBehaviour {
        private Player p;
        private WorldMap w;
		GameObject guiCards;

        public void init(WorldMap w, Player p) {
            this.w = w;
            this.p = p;

			List<Card> cards = new List<Card> ();

			guiCards = new GameObject ("Cards");
			for (int i = 0; i < 5; i++) {
				Card c = new GameObject ("Card" + i).AddComponent<Card> ();
				c.init ();

				c.transform.parent = guiCards.transform;
			}
        }

        public Hex GetHexAtMouse() {
            Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            HexLoc l = w.l.PixelHex(worldPos);
            if (w.map.ContainsKey(l)) {
                Hex h = w.map[l];
                return h;
            }
            return null;
        }

        void Update() {
            if (Input.GetMouseButtonUp(0)) {
                Hex h = GetHexAtMouse();

                //p.nextCommand = new MoveCommand(p.hero, h);
            }
        }

        void OnGUI() {
			
        }
    }
}
