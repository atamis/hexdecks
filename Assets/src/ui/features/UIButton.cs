using UnityEngine;
using System;

namespace game.ui.features {
    class UIButton : MonoBehaviour {
        [ExecuteInEditMode]
        private class BloomEffect : MonoBehaviour {
            private SpriteRenderer sr;
            public float intensity = 0.0075f;
            public float bloom = 0.5f;
            public bool use = false;

            void Awake() {
                sr = GetComponent<SpriteRenderer>();
            }

            void Update() {
                UpdateBloom (use);
            }

            void UpdateBloom(bool show) {
                MaterialPropertyBlock mpb = new MaterialPropertyBlock ();
                sr.GetPropertyBlock (mpb);
                mpb.SetFloat ("_intensity", show ? 0 : intensity);
                mpb.SetFloat ("_bloom", show ? 0 : bloom);
                sr.SetPropertyBlock (mpb);
            }
        }

        private SpriteRenderer sr;
        private BloomEffect be;
        private BoxCollider2D coll;
		private Action func;

		public void init(Sprite sprite, Action func) {
			this.func = func;

            sr = gameObject.AddComponent<SpriteRenderer> ();
            sr.material = new Material (Shader.Find ("Custom/BloomShader"));
			sr.sprite = sprite;

            coll = gameObject.AddComponent<BoxCollider2D> ();
            coll.isTrigger = true;

            be = gameObject.AddComponent<BloomEffect> ();
        }

        void OnMouseEnter() {
            be.use = true;
        }

        void OnMouseExit() {
            be.use = false;
        }

        void OnMouseUp() {
            func();
        }
    }
}
