using UnityEngine;
using System.Collections.Generic;
using game.math;
using game.tcg;
using System;
using game.render;
using game.ui;

namespace game.world.units {
	[System.Serializable]
	public struct UnitData {
		public int loc;
	}

	abstract class Unit : MonoBehaviour {
		UnitModel model;
		public float timer;

		private Hex _h;
		public Hex h {
			get {
				return _h;
			}
			set {
				if (_h != null) {
					_h.unit = null;
				}


				if (value != null) {
					if (value.unit != null) {
						throw new HexOccupiedError(value);
					}
					if (!value.Passable()) {
						throw new HexNotPassableError(value);
					}
				}

				_h = value;

				if (_h != null) {
					transform.parent = _h.transform;
					_h.unit = this;
				}
			}
		}

		internal TemporaryEffect invincible;
        internal TemporaryEffect stunned;
		public int health;
        public int maxHealth;
		public EnemyHealthBar bar;
        public EnemyStatus status;
		public WorldMap w;
		internal bool Updated;

		public bool healthShown;
		public bool mousedOver;
		public bool lastFrameMousedOver;

		public abstract List<Hex> GetAttackPattern ();

		public void init(WorldMap w, Hex h, int health) {
			this.w = w;
			this.h = h;
			this.health = health;
            this.maxHealth = health;
			this.timer = 0f;

			invincible = new TemporaryEffect();
            stunned = new TemporaryEffect();
			mousedOver = false;

			bar = gameObject.AddComponent<EnemyHealthBar>();
			bar.transform.parent = transform;
			bar.init(this);

            status = gameObject.AddComponent<EnemyStatus>();
            status.transform.parent = transform;
            status.init(this);

            var obj = new GameObject("Unit Model");
			obj.transform.parent = transform;
			model = obj.AddComponent<UnitModel>();

			model.init(this);

		}

		public virtual void CheckDeath() {
			if (health <= 0) {
				RenderManager.instance.DustCloud (new Vector3(transform.position.x, transform.position.y, Layer.UnitsFX));
				Die();
			}
		}

		internal void ApplyDamage(int v, Unit source) {
			if (!invincible.isActive()) {
				print("Applying " + v + " damage");
                Color c;

                if (source != null && source.GetType() == typeof(HeroUnit)) {
                    c = Color.yellow;
                } else {
                    c = Color.red;
                }

				UIManager.ntm.AddText(transform.position, "-" + v, c);
				health = health - v;
			}
			CheckDeath();
		}

        internal void ApplyHealing(int v, Unit source) {
            var old = health;
            health += v;
            health = Math.Min(maxHealth, health);
            UIManager.ntm.AddText(h.transform.position, "+" + (health - old), Color.green);
        }

        public virtual Sprite getSprite() {
			return Resources.Load<Sprite>("Sprites/Circle");
		}

		public virtual void NewTurn() {
            if (!stunned.isActive())
            {
                TurnActions();
            }
        }

        public virtual void BuffUpdate()
        {
            invincible.NewTurn();
            stunned.NewTurn();
        }

        public virtual void TurnActions() {

        }

		void Update() {
			//transform.localPosition = new Vector3(0, 0, 0);
            transform.localPosition = Vector3.Lerp(transform.localPosition, Vector3.zero, 0.3f);
            timer += Time.deltaTime;
		}

		public void ShowHealth(bool b) {
			bar.model.sr.enabled = b;
		}

		public virtual void Die() {
			foreach (Hex hex in GetAttackPattern()) {
				hex.Highlight (Color.white);
			}

			h = null;
			Destroy(model);
			Destroy(this.gameObject);
		}

		private class UnitModel : MonoBehaviour {
			private SpriteRenderer sr;
			private Unit u;

			public void init(Unit u) {
				this.u = u;

				transform.localPosition = LayerV.HeroUnit;


				sr = gameObject.AddComponent<SpriteRenderer>();
				//sr.material = new Material(Shader.Find("Custom/OutlineShader"));
				sr.sprite = u.getSprite();
				//gameObject.AddComponent<SpriteOutline>();

				sr.color = new Color(1, 1, 1);

			}

			void Update() {
				sr.sprite = u.getSprite();
			}
		}
	}

	class TemporaryEffect {
		public int duration;
		public TemporaryEffect() {

		}

		public bool isActive() {
			return duration > 0;
		}

		internal void NewTurn() {
			duration--;
		}
	}
}
