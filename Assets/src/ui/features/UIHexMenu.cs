using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace game.ui.features {
	class UIHexMenu : MonoBehaviour {
		private class ExitPanel : MenuPanel {
			public override string GetText() {
				return "Quit";
			}

			void OnMouseDown() {
				SceneManager.LoadSceneAsync("Intro");
			}
		}

		private class HelpPanel : MenuPanel {
			public override string GetText() {
				return "Help";
			}

			void OnMouseDown() {
				
			}
		}

		private class ResetPanel : MenuPanel {
			public override string GetText() {
				return "Reset";
			}

			void OnMouseDown() {
				SceneManager.LoadSceneAsync("Main");
			}
		}

		private class LibraryPanel : MenuPanel {
			public override string GetText() {
				return "Library";
			}

			void OnMouseDown() {
				SceneManager.LoadSceneAsync("Library");
			}
		}
			
		private class SettingsPanel : MenuPanel {
			public override string GetText() {
				return "Quit";
			}
		}

		private class OtherPanel : MenuPanel {
			public override string GetText() {
				return "Other";
			}
		}

		private class MenuPanel : MonoBehaviour {
			private SpriteRenderer sr;
			private TextMesh tm;
			private GameObject textObj;
			private PolygonCollider2D coll;

			private Vector3 origin;
			private Vector3 target;

			public virtual string GetText() {
				return "";
			}

			public void init() {
				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/Triangle");

				coll = gameObject.AddComponent<PolygonCollider2D> ();
				coll.isTrigger = true;

				textObj = new GameObject("UI Text");
				textObj.transform.parent = transform;
				textObj.transform.localPosition = new Vector3(0, 0, -0.5f);

				tm = textObj.AddComponent<TextMesh>();
				tm.text = GetText();
				tm.color = Color.red;
				tm.alignment = TextAlignment.Center;
				tm.anchor = TextAnchor.MiddleCenter;
				tm.fontSize = 74;
				tm.characterSize = 0.04f;
				tm.font = UIManager.font;
				tm.GetComponent<Renderer>().material = UIManager.font.material;

				tm.transform.localEulerAngles = new Vector3 (0, 0, -60);
				tm.transform.localPosition = new Vector2 (.25f, .27f);
			}

			void OnMouseEnter() {
				sr.color = Color.blue;
			}

			void OnMouseExit() {
				sr.color = Color.white;
			}
		}

		List<MenuPanel> panels = new List<MenuPanel> ();

		void Awake() {
			Vector2[] locs = new Vector2[6] { 
				new Vector2 (.45f, .25f), new Vector2 (0, .5f), new Vector2 (-.45f, .25f),
				new Vector2 (-.45f, -.25f), new Vector2 (0, -.5f), new Vector2 (.45f, -.25f), 
			};
				
			ExitPanel ep = new GameObject ("Exit Panel").AddComponent<ExitPanel> ();
			ep.init ();
			panels.Add (ep);

			HelpPanel hp = new GameObject ("Help Panel").AddComponent<HelpPanel> ();
			hp.init ();
			panels.Add (hp);

			ResetPanel rp = new GameObject ("Reset Panel").AddComponent<ResetPanel> ();
			rp.init ();
			panels.Add (rp);

			LibraryPanel lp = new GameObject ("Library Panel").AddComponent<LibraryPanel> ();
			lp.init ();
			panels.Add (lp);

			SettingsPanel sp = new GameObject ("Settings Panel").AddComponent<SettingsPanel> ();
			sp.init ();
			panels.Add (sp);

			float scale = 2.0f;
			int i = 0;
			foreach (MenuPanel mp in panels) {
				mp.transform.parent = transform;
				mp.transform.position = locs [i] * scale;

				mp.transform.localScale = new Vector3 (scale, scale, 0);
				mp.transform.localEulerAngles = new Vector3(0, 0, 60 * i);

				i++;
			}
		}

		void OnEnable() {
			transform.position = Camera.main.ScreenToWorldPoint (new Vector3 (Screen.width / 2, Screen.height / 2, 9));
		}
	}
}

