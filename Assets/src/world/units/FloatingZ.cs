using UnityEngine;
using System.Collections.Generic;
using game.world.units;
using game.math;
using System;

namespace game.world
{
	class FloatingZ : MonoBehaviour
	{
		public Unit u;
		public StatusModel model;
        private float deathTimer;
        private const float lifespan = 2f;

		public void init(Unit u) {
			this.u = u;
			transform.localPosition = LayerV.UnitFX + new Vector3(0, .5f, 0);

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
                Destroy(gameObject);
            }
		}

		public class StatusModel : MonoBehaviour {
			public FloatingZ z;
			public SpriteRenderer sr;

            public void init(FloatingZ z)
            {
                this.z = z;

                //transform.localScale = new Vector3(2, 2, 2);
                transform.localPosition = LayerV.UnitFX + new Vector3(0, 0, 0);
                sr = gameObject.AddComponent<SpriteRenderer>();
                sr.sprite = Resources.Load<Sprite>("Sprites/Particles/Zzz");

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

