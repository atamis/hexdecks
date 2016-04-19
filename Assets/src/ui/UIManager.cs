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
        private Hex currHex;
        private Hex lastHex;
        private Unit currUnit;
        private Unit lastUnit;

        private int invincibleCD;
        private int aoeCD;
        private int twoactionCD;

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

                if (h != null && p.nextCommand == null) {
                    p.nextCommand = new MoveCommand(p.hero, h);
                }
            }

            if (Input.GetKeyUp(KeyCode.Alpha1) && invincibleCD == 0) {
                invincibleCD = 5;
                p.nextCommand = new InvincibleCommand(w.hero);
            }

            if (Input.GetKeyUp(KeyCode.Alpha2) && aoeCD == 0) {
                aoeCD = 5;
                p.nextCommand = new AOECommand(w.hero);
            }

            if (Input.GetKeyUp(KeyCode.Alpha3) && twoactionCD == 0) {
                twoactionCD = 5;
                p.nextCommand = new DoubleActionCommand(p);
            }

            HandleEnemyMouseOver();

        }

        public void HandleEnemyMouseOver()
        {
            lastUnit = currUnit;
            currHex = GetHexAtMouse();

            if (currHex != null)
            {
                currUnit = currHex.unit;
            }

            if (currUnit != lastUnit && lastUnit != null)
            {
                if (lastUnit.GetType().IsSubclassOf(typeof(EnemyUnit)))
                {
                    lastUnit.mouseExit();
                }
            }

            if (currUnit != null)
            {
                if (currUnit.GetType().IsSubclassOf(typeof(EnemyUnit)))
                {
                    currUnit.mouseEnter();
                }
            }
        }

        void OnGUI()
        {
            if (p != null) GUI.Label(new Rect(150, 10, 100, 30), "Health: " + p.hero.health);
            
            GUI.color = Color.yellow;

            GUI.Label(new Rect(150, 30, 250, 20), "[1] Invincible" + "(" + invincibleCD + ")");

            GUI.Label(new Rect(150, 50, 250, 20), "[2] 1 damage to surounding hexes" + "(" + aoeCD + ")");

            GUI.Label(new Rect(150, 70, 250, 20), "[3] Gain 2 actions" + "(" + twoactionCD + ")");

            if (GUI.Button(new Rect(750, 10, 100, 30), "Reset Level")) {
                SceneManager.LoadSceneAsync("Main");
            }

        }

        internal void NextTurn() {
            invincibleCD = Math.Max(0, invincibleCD - 1);
            aoeCD = Math.Max(0, aoeCD - 1);
            twoactionCD = Math.Max(0, twoactionCD - 1);
        }
    }
}
