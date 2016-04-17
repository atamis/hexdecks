using game.world;
using game.math;
using game.world.units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.world {
    class SaveManager {
/*
_ = normal
W = wall
w = water
e = end tile
p = player
m = melee
r = range
M = big melee
c = chest
*/

        public static WorldMap Default(Layout l, GameManager gm) {
            WorldMap w = new WorldMap(l, gm);

            for (int i = 0; i < 20; i++) {
                for (int j = 0; j < 20; j++) {
                    w.addHex(new HexLoc(i, j));
                }
            }

            var hero = new GameObject("Tim").AddComponent<HeroUnit>();
            hero.init(w, w.map[new HexLoc(0, 0)]);
            w.hero = hero;

            var renemy = new GameObject("RangedEvilTim").AddComponent<RangedEnemy>();
            renemy.init(w, w.map[new HexLoc(5, 5)]);

            var menemy = new GameObject("MeleeEvilTim").AddComponent<MeleeEnemy>();
            menemy.init(w, w.map[new HexLoc(5, 6)]);


            return w;

        }

        private static bool isTileChar(char c) {
            switch (c) {
                case '_':
                case 'W':
                case 'w':
                case 'e':
                case 'p':
                case 'm':
                case 'r':
                case 'M':
                case 'c':
                    return true;
                default:
                    return false;
            }
        }

        public static WorldMap LoadLevel(Layout l, string name, GameManager gm) {
            TextAsset lvl = Resources.Load<TextAsset>("Levels/" + name);

            if (lvl == null) {
                return null;
            }

            WorldMap w = new WorldMap(l, gm);

            var lines = lvl.text.Split('\n');

            for(int x = 0; x < lines.Length; x++) {
                var line = lines[x];
                for(int y = 0; y < line.Length; y++) {
                    var chr = line[y];
                    //UnityEngine.Debug.Log(chr + ", " + (int) chr);

                    // Only \r needed, but both for clarity.
                    // Needed because windows uses \r\n for newlines
                    if (isTileChar(chr)) {
                        var h = w.addHex(new HexLoc(y, x));
                        switch (chr) {
                            case 'W':
                                h.tileType = TileType.Wall;
                                break;
                            case 'w':
                                h.tileType = TileType.Water;
                                break;
                            case 'p':
                                var hero = new GameObject("Tim").AddComponent<HeroUnit>();
                                hero.init(w, h);
                                w.hero = hero;
                                break;
                            case 'm':
                                var menemy = new GameObject("MeleeEvilTim").AddComponent<MeleeEnemy>();
                                menemy.init(w, h);
                                break;
                            case 'M':
                                var bigmenemy = new GameObject("BigMeleeEvilTim").AddComponent<BigMeleeEnemy>();
                                bigmenemy.init(w, h);
                                break;
                            case 'r':
                                var renemy = new GameObject("RangedEvilTim").AddComponent<RangedEnemy>();
                                renemy.init(w, h);
                                break;
                            case 'e':
                                new GameObject("EndLevelTrigger").AddComponent<EndLevelTrigger>().init(h);
                                break;
                            default:
                                // Not Implemented: e, M, c
                                break;
                        }
                    }
                }
            }

            return w;
        }
    }
}
