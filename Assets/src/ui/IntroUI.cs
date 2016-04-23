﻿using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace game.ui {
	class IntroUI : MonoBehaviour {
		public static Font font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");
		internal GameManager gm;
		private GameObject uiFeatures;

		public string[] levels = new string[] {
			"level1",
		};

		public void init() {
			this.name = "UI";

			foreach (string lvl in levels) {
				UILoadButton b = new GameObject ("UI Load Button").AddComponent<UILoadButton> ();
				b.init (lvl);

				b.transform.parent = transform;
			}
		}

		private class UILoadButton : MonoBehaviour { 

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
			private BoxCollider2D coll;
			private string level;
			private GameObject textObj;
			private TextMesh tm;
			private BloomEffect be;

			public void init(string level) {
				this.level = level;

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.material = new Material (Shader.Find ("Custom/BloomShader"));
				sr.sprite = Resources.Load<Sprite> ("Sprites/UI/T_Wood");

				be = gameObject.AddComponent<BloomEffect> ();

				coll = gameObject.AddComponent<BoxCollider2D> ();
				coll.isTrigger = true;

				textObj = new GameObject("Card Text");
				textObj.transform.parent = transform;
				textObj.transform.localPosition = new Vector3(0, 0, -0.1f);

				tm = textObj.AddComponent<TextMesh>();
				tm.text = level;
				tm.color = Color.black;
				tm.font = IntroUI.font;
				tm.alignment = TextAlignment.Center;
				tm.anchor = TextAnchor.MiddleCenter;
				tm.fontSize = 148;
				tm.characterSize = 0.05f;
				tm.GetComponent<Renderer>().material = font.material;
			}

			void OnMouseEnter() {
				be.use = true;
			}

			void OnMouseExit() {
				be.use = false;
			}

			void OnMouseDown() {
				GameManager.level = level;
				SceneManager.LoadSceneAsync ("Main");
			}
		}
	}
}
