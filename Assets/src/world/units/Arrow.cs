using UnityEngine;
using System.Collections.Generic;
using game.world.units;
using game.math;
using System;

namespace game.world
{
	class Arrow : MonoBehaviour
	{
		public Unit u;
		public ArrowModel model;

		public void init(Unit u) {
			this.u = u;
			transform.localPosition = LayerV.UnitFX + new Vector3(0, .5f, 0);

            var obj = new GameObject("Arrow model");
			obj.transform.parent = transform;
			model = obj.AddComponent<ArrowModel>();

			model.init(this);
		}

		// Update is called once per frame
		void Update() {

		}

		public class ArrowModel : MonoBehaviour {
			public Arrow a;
			public SpriteRenderer sr;

            public void init(Arrow a)
            {
                this.a = a;

                //transform.localScale = new Vector3(2, 2, 2);
                transform.localPosition = LayerV.UnitFX + new Vector3(0, 0, 0);
                sr = gameObject.AddComponent<SpriteRenderer>();
                sr.sprite = Resources.Load<Sprite>("Sprites/UI/T_PlusIcon");

                sr.color = new Color(1, 1, 1);
            }

			void Update() {
                float speed = Time.deltaTime * 0.5f;
				transform.localPosition += new Vector3(speed, speed, 0);
			}
		}
	}
}

