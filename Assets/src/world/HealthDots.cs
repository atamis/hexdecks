using UnityEngine;
using System.Collections.Generic;
using game.world.units;
using game.world.math;
using System;

namespace game.world
{
    class HealthDots : MonoBehaviour
    {
        private EnemyUnit u;
        private HealthDotsModel model;

        public void init(EnemyUnit u)
        {
            var obj = new GameObject("Health Dot Model");
            obj.transform.parent = transform;
            model = obj.AddComponent<HealthDotsModel>();

            model.init(this);
        }

        // Update is called once per frame
        void Update()
        {

        }

        private class HealthDotsModel : MonoBehaviour
        {
            HealthDots d;
            SpriteRenderer sr;

            public void init(HealthDots d)
            {
                this.d = d;

                //gameObject.transform.localPosition = LayerV.EnemyUnits;

                sr = gameObject.AddComponent<SpriteRenderer>();

                setSprite();

                sr.color = new Color(0, 0, 0);
            }

            private void setSprite()
            {
                if (d.u.health == 4)
                {
                    sr.sprite = Resources.Load<Sprite>("Sprites/Circle");
                }
                else if (d.u.health == 3)
                {
                    sr.sprite = Resources.Load<Sprite>("Sprites/Circle");
                }
                else if (d.u.health == 2)
                {
                    sr.sprite = Resources.Load<Sprite>("Sprites/Circle");
                }
                else if (d.u.health == 1)
                {
                    sr.sprite = Resources.Load<Sprite>("Sprites/Circle");
                }
            }

            void Update()
            {
                setSprite();
            }
        }
    }
}