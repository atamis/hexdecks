using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace game.ui {
	class IntroUI : MonoBehaviour {
		public static Font font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");
		private GameObject uiFeatures;

		public string[] levels = new string[] {
			"level1", "level2", 
			"level3", "level1", 
			"level2", "test",
		};

		Vector2[] pos = new Vector2[] {
			new Vector2(-1.4f, 1.25f), new Vector2(-1.4f, 0f),
			new Vector2(-1.4f, -1.25f), new Vector2(1.4f, 1.25f),
			new Vector2(1.4f, 0f), new Vector2(1.4f, -1.25f),
		};

		public void init() {
			this.name = "UI";

			int i = 0;
			foreach (string lvl in levels) {
				UILoadButton b = new GameObject ("UI Load Button").AddComponent<UILoadButton> ();
				b.init (lvl);

				b.transform.localPosition = pos [i];
				b.transform.parent = transform;

				i++;
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

				gameObject.transform.localScale = new Vector3 (.75f, .75f, 1);

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
				//IntroManager.audio.PlayOneShot (Resources.Load<AudioClip> ("Audio/UI/MenuSelect"));
				GameManager.level = level;
				SceneManager.LoadSceneAsync ("Main");
			}
		}
	}
}
