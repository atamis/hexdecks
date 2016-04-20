using UnityEngine;
using System.Collections;
using game.math;
using game.tcg;
using System;

namespace game.world.units {
	[System.Serializable]
	public struct UnitData {
		public int loc;
	}

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
		public int health;
		public EnemyHealthBar pips;
		public WorldMap w;
		internal bool Updated;

		public bool healthShown;
		public bool mousedOver;
		public bool lastFrameMousedOver;

		public void init(WorldMap w, Hex h, int health) {
			this.w = w;
			this.h = h;
			this.health = health;

			invincible = new TemporaryEffect();
			mousedOver = false;

			pips = gameObject.AddComponent<EnemyHealthBar>();
			pips.transform.parent = transform;
			pips.init(this);

			var obj = new GameObject("Unit Model");
			obj.transform.parent = transform;
			model = obj.AddComponent<UnitModel>();

			model.init(this);

		}

		public void CheckDeath() {
			if (health <= 0) {
				Die();
			}
		}

		internal void ApplyDamage(int v) {
			if (!invincible.isActive()) {
				print("Applying " + v + " damage");
				GameManager.ntm.AddText(transform.position, "-" + v);
				health = health - v;
			}
			CheckDeath();
		}

		public virtual Sprite getSprite() {
			return Resources.Load<Sprite>("Sprites/Circle");
		}

		public virtual void NewTurn() {
			hideAtkPattern();
			invincible.NewTurn();
		}

		void Update() {
			transform.localPosition = new Vector3(0, 0, 0);

		}

		public virtual void showAtkPattern()
		{

		}

		public virtual void hideAtkPattern()
		{

		}

		public virtual void Die() {
			hideAtkPattern();
			h = null;
			Destroy(model);
			Destroy(this.gameObject);
		}

		public void mouseEnter()
		{
			pips.model.sr.enabled = true;
			showAtkPattern();
		}

		public void mouseExit()
		{
			pips.model.sr.enabled = false;
			hideAtkPattern();
		}

		private class UnitModel : MonoBehaviour {
			SpriteRenderer sr;
			Unit u;

			public void init(Unit u) {
				this.u = u;

				transform.localPosition = LayerV.HeroUnit;

				sr = gameObject.AddComponent<SpriteRenderer>();
				sr.sprite = u.getSprite();

				sr.color = new Color(1, 1, 1);
			}

			void Update() {
				transform.localPosition = LayerV.HeroUnit;
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


