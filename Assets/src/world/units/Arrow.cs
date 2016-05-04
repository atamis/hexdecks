using UnityEngine;
using System.Collections.Generic;
using game.world.units;
using game.math;
using System;

namespace game.world {
	class Arrow : MonoBehaviour {
		public Unit u;
		public ArrowModel model;
        private AudioManager am;

        public void init(Unit u) {
			this.u = u;

            transform.localPosition = LayerV.UnitFX;

            var obj = new GameObject("Arrow model");
			obj.transform.parent = transform;
			model = obj.AddComponent<ArrowModel>();

			model.init(this);
		}
			
		void Update() { }

		public class ArrowModel : MonoBehaviour {
			public Arrow a;
			public SpriteRenderer sr;
            public static Vector3 defaultArrowDirection = new Vector3(-1, 0, 0);

            public void init(Arrow a) {
                this.a = a;

                //transform.localScale = new Vector3(2, 2, 2);
                transform.localPosition = LayerV.UnitFX + new Vector3(0, 0, 0);
                sr = gameObject.AddComponent<SpriteRenderer>();
                sr.sprite = Resources.Load<Sprite>("Sprites/Enemies/Arrow");

                sr.color = new Color(1, 1, 1);
            }

			void Update() {
                float speed = Time.deltaTime * 25f;

                if (a.u.w.hero != null)
                {
                    Vector3 direction = a.u.w.hero.transform.position - transform.position;

                    float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);

                    transform.eulerAngles = new Vector3(0, 0, angle + 180);
                    transform.localPosition += direction.normalized * speed;

                    if ((a.u.w.hero.transform.position - transform.position).magnitude < 0.5f) {
						AudioManager.audioS.PlayOneShot(AudioManager.arrowSound);
                        a.u.w.hero.ApplyDamage(1, a.u);
                        Destroy(gameObject);
                    }
                }
			}
		}
	}
}

