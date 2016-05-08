using UnityEngine;
using System.Collections.Generic;
using game.world.units;
using game.math;
using System;

namespace game.world {
	class FloatingZ : MonoBehaviour {
		public Unit u;
		public StatusModel model;
        private float deathTimer;
        private const float lifespan = 2f;

        private Vector3 origin;

		public void init(Unit u) {
			this.u = u;
			transform.localPosition = new Vector3 (0, .5f, -1);
            origin = LayerV.UnitFX;

            var obj = new GameObject("Z model");
			obj.transform.parent = transform;
			model = obj.AddComponent<StatusModel>();

			model.init(this);

            deathTimer = 0f;
		}

		// Update is called once per frame
		void Update() {
            deathTimer += Time.deltaTime;
            if(deathTimer > lifespan)
            {
                model.transform.localPosition = origin;
                deathTimer = 0;
            }
		}

		public class StatusModel : MonoBehaviour {
			private Sprite tex = Resources.Load<Sprite>("Particle/Zzz");
			public FloatingZ z;
			public SpriteRenderer sr;

            public void init(FloatingZ z) {
                this.z = z;

                //transform.localScale = new Vector3(2, 2, 2);
                transform.localPosition = LayerV.UnitFX + new Vector3(0, 0, 0);
                sr = gameObject.AddComponent<SpriteRenderer>();
				sr.sprite = tex;

                sr.color = new Color(1, 1, 1);
                sr.enabled = z.u.status.model.sr.enabled;
            }

			void Update() {
                float speed = Time.deltaTime * 0.5f;
				transform.localPosition += new Vector3(speed, speed, 0);
			}
		}
	}
}

