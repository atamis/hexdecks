using game.tcg;
using game.world;
using game.world.math;
using game.world.units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace game.ui {
    class UIManager : MonoBehaviour {
        private Player p;
        private WorldMap w;

        public void init(WorldMap w, Player p) {
            this.w = w;
            this.p = p;
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

                if (h != null) {
                    p.nextCommand = new MoveCommand(p.hero, h);
                }
            }
        }

        void OnGUI()
        {
            if (p != null) GUI.Label(new Rect(150, 10, 100, 30), "Health: " + p.hero.health);

            if (GUI.Button(new Rect(750, 10, 100, 30), "Reset Level"))
            {
                SceneManager.LoadSceneAsync("Main");
            }
        }
    }
}
