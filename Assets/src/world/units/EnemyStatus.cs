using UnityEngine;
using System.Collections.Generic;
using game.world.units;
using game.math;
using System;

namespace game.world
{
	class EnemyStatus : MonoBehaviour
	{
		public Unit u;
		public StatusModel model;
        public bool alert;
        public bool asleep;

		public void init(Unit u) {
			this.u = u;
			transform.localPosition = new Vector3(0, 0, 1);

			var obj = new GameObject("Status Model");
			obj.transform.parent = transform;
			model = obj.AddComponent<StatusModel>();

			model.init(this);

            alert = false;
            asleep = true;
		}

		// Update is called once per frame
		void Update() {

		}

		public class StatusModel : MonoBehaviour {
			public EnemyStatus status;
			public SpriteRenderer sr;
            private float alertTimer;
            private float asleepTimer;
            private float nextZ;
            private const float zInterval = .8f;
            private List<FloatingZ> zz;

            public void init(EnemyStatus status)
            {
                this.status = status;

                //transform.localScale = new Vector3(2, 2, 2);
                transform.localPosition = LayerV.UnitFX + new Vector3(0, .5f, 0);
                sr = gameObject.AddComponent<SpriteRenderer>();

                sr.color = new Color(1, 1, 1);
                sr.enabled = false;

                alertTimer = 0f;
                asleepTimer = 0f;
                nextZ = 0f;

                zz = new List<FloatingZ>();
            }

            private void asleepAnimation()
            {
                asleepTimer += Time.deltaTime;
                if(asleepTimer >= nextZ)
                {
                    FloatingZ newZ = new GameObject("floating z").AddComponent<FloatingZ>();
                    newZ.transform.parent = status.u.transform;
                    newZ.init(status.u);
                    zz.Add(newZ);
                    nextZ += zInterval;
                }

            }

            public void destroyZs()
            {
                foreach(FloatingZ z in zz.ToArray())
                {
                    if(z != null)
                    {
                        Destroy(z.gameObject);
                    }   
                }
            }

            private void alertAnimation()
            {
                alertTimer += Time.deltaTime;
                if(alertTimer < 1)
                {
                    sr.sprite = Resources.Load<Sprite>("Sprites/UI/T_Alert");
                }
                else
                {
                    status.model.sr.enabled = false;
                    status.alert = false;
                }
                
            }

			void Update() {
                if (status.asleep)
                {
                    asleepAnimation();
                }
                else if (status.alert)
                {
                    alertAnimation();
                }
			}
		}
	}
}

