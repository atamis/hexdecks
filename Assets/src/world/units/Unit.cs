﻿/*
 * Andrew Amis, Nick Care, Robert Tomcik (2016)
 * The Unit class
 *
 */

using UnityEngine;
using System.Collections;
using game.world.math;
using game.tcg;
using System;

namespace game.world.units {
	class Unit : MonoBehaviour {
        UnitModel model;

        private Hex _h;
		public Hex h {
            get {
                return _h;
            }
            set {
                if (_h != null) {
                    _h.unit = null;
                }


                if (value != null && value.unit != null) {
                    throw new HexOccupiedError(_h);
                }

                _h = value;
                
                if (_h != null) {
                    transform.parent = _h.transform;
                    _h.unit = this;
                }
            }
        }

        public void CheckDeath() {
            if (health <= 0) {
                Die();
            }
        }

        public int health;
        public WorldMap w;
        internal bool Updated;

        public void init(WorldMap w, Hex h, int health) {
            this.w = w;
            this.h = h;
            this.health = health;


            var obj = new GameObject("Unit Model");
            obj.transform.parent = transform;
            model = obj.AddComponent<UnitModel>();

            model.init(this);

        }

        internal void ApplyDamage(int v) {
            print("Applying " + v + " damage");
            health = health - v;
            CheckDeath();
        }

        public virtual Sprite getSprite() {
            return Resources.Load<Sprite>("Sprites/Circle"); ;
        }

        public virtual void NewTurn() {

        }

        void Update() {
            transform.localPosition = new Vector3(0, 0, 0);
        }

        public void Die() {
            h = null;
            Destroy(model);
            Destroy(this.gameObject);
		}

        private class UnitModel : MonoBehaviour {
            SpriteRenderer sr;
            Unit u;

            public void init(Unit u) {
                this.u = u;

                gameObject.transform.localPosition = LayerV.HeroUnit;

                sr = gameObject.AddComponent<SpriteRenderer>();
                sr.sprite = u.getSprite();

                sr.color = new Color(0, 0, 0);
            }

            void Update() {
                transform.localPosition = LayerV.HeroUnit;
                sr.sprite = u.getSprite();
            }
        }
    }

}


