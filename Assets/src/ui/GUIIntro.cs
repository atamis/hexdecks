using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace game.ui {
	class GUIIntro : GUIBase {
		private Vector2[] b_locs = new Vector2[] {
			new Vector2(-2.2f, 2f), new Vector2(-2.2f, 0f),
			new Vector2(-2.2f, -2f), new Vector2(2.2f, 2f),
			new Vector2(2.2f, 0f), new Vector2(2.2f, -2f)
		};

		private List<UILoadButton> buttons;
		private GameObject title; private GameObject credits;

		void Awake() {
			this.name = "UI";
			this.buttons = new List<UILoadButton> ();

			for (int i = 0; i < 6; i++) {
				UILoadButton b = new GameObject ("LoadButton").AddComponent<UILoadButton> ();
				b.init (i);

				b.transform.position = b_locs [i];
				b.transform.parent = transform;

				buttons.Add (b);
			}

			Camera.main.backgroundColor = Color.black;
			drawTitle();
		}

		private void drawTitle(){
			title = new GameObject("Title Text");
			title.transform.parent = transform;
			title.transform.localPosition = new Vector3(0, 4, -0.1f);

			TextMesh tm = title.AddComponent<TextMesh>();
			tm.text = "HexDex";
			tm.color = Color.white;
			tm.alignment = TextAlignment.Center;
			tm.anchor = TextAnchor.MiddleCenter;
			tm.fontSize = 164;
			tm.characterSize = 0.05f;
			tm.font = UIManager.GetFont();
			tm.GetComponent<Renderer>().material = UIManager.GetFont().material;

            credits = new GameObject("Title Text");
			credits.transform.parent = transform;
			credits.transform.localPosition = new Vector3(0, -4, -0.1f);

			TextMesh tm2 = credits.AddComponent<TextMesh>();
			tm2.text = "Created By: Dan Marsh, Nick Care, Andrew Amis, Robert Tomcik, Dan Karcher";
			tm2.color = Color.white;
			tm2.font = UIManager.GetFont();
			tm2.alignment = TextAlignment.Center;
			tm2.anchor = TextAnchor.MiddleCenter;
			tm2.fontSize = 64;
			tm2.characterSize = 0.05f;
			tm2.GetComponent<Renderer>().material = UIManager.GetFont().material;
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
			private int lvl;

			private GameObject textObj;
			private TextMesh tm;
			private BloomEffect be;

			public void init(int lvl) {
				this.lvl = lvl;

				gameObject.transform.localScale = new Vector3 (1.1f, 1.1f, 1);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.material = new Material (Shader.Find ("Custom/BloomShader"));
				sr.sprite = Resources.Load<Sprite> ("Sprites/UI/T_Wood");

				be = gameObject.AddComponent<BloomEffect> ();

				coll = gameObject.AddComponent<BoxCollider2D> ();
				coll.isTrigger = true;

				textObj = new GameObject("Button Text");
				textObj.transform.parent = transform;
				textObj.transform.localPosition = new Vector3(0, 0, -0.1f);

				tm = textObj.AddComponent<TextMesh>();
				tm.text = GameManager.GetLevel(lvl).GetSceneName();
				tm.color = Color.black;
				tm.font = UIManager.GetFont();
				tm.alignment = TextAlignment.Center;
				tm.anchor = TextAnchor.MiddleCenter;
				tm.fontSize = 89;
				tm.characterSize = 0.05f;
				tm.GetComponent<Renderer>().material = UIManager.GetFont().material;
			}

			void OnMouseEnter() {
				be.use = true;
			}

			void OnMouseExit() {
				be.use = false;
			}

			void OnMouseDown() {
				//AudioManager.effects.PlayOneShot (Resources.Load<AudioClip> ("Audio/UI/MenuSelect"));
				GameManager.LoadLevel(lvl);
			}
		}
	}
}
