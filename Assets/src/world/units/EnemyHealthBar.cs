using UnityEngine;
using System.Collections.Generic;
using game.world.units;
using game.math;
using System;

namespace game.world {
	class EnemyHealthBar : MonoBehaviour {
		public Unit u;
		public HealthBarModel model;

		public void init(Unit u) {
			this.u = u;
			transform.localPosition = new Vector3(0, 0, 1);

			var obj = new GameObject("Health Dot Model");
			obj.transform.parent = transform;
			model = obj.AddComponent<HealthBarModel>();

			model.init(this);
		}

		// Update is called once per frame
		void Update() {

		}

		public class HealthBarModel : MonoBehaviour {
			public EnemyHealthBar b;
			public SpriteRenderer sr;

			public void init(EnemyHealthBar b) {
				this.b = b;

				transform.localScale = new Vector3(2, 2, 2);
				transform.localPosition = LayerV.UnitFX + new Vector3(0, .5f, 0);
				sr = gameObject.AddComponent<SpriteRenderer>();

				setSprite();

				sr.color = new Color(1, 1, 1);
				sr.enabled = false;
			}

			private void setSprite() {
				if (b.u.health == 4) {
					sr.sprite = Resources.Load<Sprite>("Sprites/UI/T_HealthPip4");
				}
				else if (b.u.health == 3) {
					sr.sprite = Resources.Load<Sprite>("Sprites/UI/T_HealthPip3");
				}
				else if (b.u.health == 2) {
					sr.sprite = Resources.Load<Sprite>("Sprites/UI/T_HealthPip2");
				}
				else if (b.u.health == 1) {
					sr.sprite = Resources.Load<Sprite>("Sprites/UI/T_HealthPip");
				}
			}

			void Update() {
				transform.localPosition = LayerV.UnitFX + new Vector3(0, .65f, 0);
				setSprite();
			}
		}
	}
}

