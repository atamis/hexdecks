using UnityEngine;
using System.Collections.Generic;

namespace game.ui {
	class UIHexMenu : MonoBehaviour {
		private class MenuPanel : MonoBehaviour {
			private SpriteRenderer sr;
			private TextMesh tm;
			private GameObject textObj;
			private PolygonCollider2D coll;

			private Vector3 origin;
			private Vector3 target;

			public void init(string label) {
				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/Triangle");
				sr.material = new Material (Shader.Find ("Custom/OutlineShader"));

				coll = gameObject.AddComponent<PolygonCollider2D> ();
				coll.isTrigger = true;

				textObj = new GameObject("UI Text");
				textObj.transform.parent = transform;
				textObj.transform.localPosition = new Vector3(0, 0, -0.5f);

				Font font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");

				tm = textObj.AddComponent<TextMesh>();
				tm.text = label;
				tm.color = Color.black;
				tm.alignment = TextAlignment.Center;
				tm.anchor = TextAnchor.MiddleCenter;
				tm.fontSize = 148;
				tm.characterSize = 0.04f;
				tm.font = WorldUI.font;
				tm.GetComponent<Renderer>().material = font.material;
			}

			void OnMouseEnter() {
				sr.color = Color.blue;
			}

			void OnMouseExit() {
				sr.color = Color.white;
			}
		}


		void Awake() {
			List<MenuPanel> panels = new List<MenuPanel> ();
		
			Vector2[] locs = new Vector2[6] { 
				new Vector2 (.45f, .25f), new Vector2 (0, .5f), new Vector2 (-.45f, .25f),
				new Vector2 (-.45f, -.25f), new Vector2 (0, -.5f), new Vector2 (.45f, -.25f), 
			};

			string[] labels = new string[] {
				"Help", "Reset", "Library", "Quit", "Unknown", "Unknown",
			};

			float scale = 2.0f;
			for (int i = 0; i < 6; i++) {
				MenuPanel mp = new GameObject ("Panel").AddComponent<MenuPanel> ();
				mp.init ("Words");

				mp.transform.parent = transform;

				mp.transform.position = locs [i] * scale;
				mp.transform.localScale = new Vector3 (scale, scale, 0);
				mp.transform.localEulerAngles = new Vector3(0, 0, 60 * i);

				panels.Add(mp);
			}
		}

		void OnEnable() {
			transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, 9));
		}
	}
}

