using UnityEngine;
using System.Collections.Generic;

// TODO
// TOOLTIPS

namespace game.ui {
	class WorldUI : GameUI {
        private WorldHUD ib;
        static internal Font font;
		private UIHexMenu menu;

        void Awake() {
            //uiFolder = new GameObject("UI Elements");
			font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");

			ib = new GameObject("Infobar").AddComponent<WorldHUD>();
            ib.init();

			Color[] cs = new Color[] { Color.red, Color.blue, Color.cyan, Color.green, Color.magenta };
			for (int i = 0; i < 5; i++) {
				UICard c = new GameObject ("Card").AddComponent<UICard> ();
				c.init ();
				c.SetColor (cs [i]);

				float px = (Screen.width / 2) + 100 * Mathf.Cos(0.7853f + i*.3141f);
				float py = 100 * Mathf.Sin (0.7853f + i *.3141f);

				c.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(px, py, 1));
				c.transform.localEulerAngles = new Vector3(0, 0, 45 + (i * 18));

				c.SetOrigin (Camera.main.ScreenToWorldPoint(new Vector3(px, py, 1)));
			}


			menu = new GameObject ("Menu").AddComponent<UIHexMenu>();
			menu.gameObject.SetActive (false);
        }

        void Update() {
			ib.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * .1f, 1));

			if (Input.GetKeyDown (KeyCode.Escape)) {
				this.menu.gameObject.SetActive (!menu.gameObject.activeSelf);
			}
        }

		public abstract class CustomUIFeature : MonoBehaviour {
			private SpriteRenderer sr;
			private TextMesh tm;
			private GameObject textObj;

			public abstract string GetText();

			public abstract string GetTooltip();

			public abstract Sprite GetSprite();

			public void init() {
				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = GetSprite();

				textObj = new GameObject("UI Text");
				textObj.transform.parent = transform;
				textObj.transform.localPosition = new Vector3(0, 0, -0.5f);

				tm = textObj.AddComponent<TextMesh>();
				tm.text = GetText ();
				tm.color = Color.black;
				tm.alignment = TextAlignment.Center;
				tm.anchor = TextAnchor.MiddleCenter;
				tm.fontSize = 148;
				tm.characterSize = 0.04f;
				tm.font = WorldUI.font;
				tm.GetComponent<Renderer>().material = font.material;
			}

			void OnMouseOver() {
				// Make a tooltip
			}
		}

        private class WorldHUD : MonoBehaviour {
            private UIHealthFeature hf;
			private UIBuffFeature bf;
			private UIActionFeature af;

            public void init() {
				// HEALTH FEATURE
                hf = new GameObject("Health Feature").AddComponent<UIHealthFeature>();
                hf.init();

				hf.transform.parent = transform;
				hf.transform.localPosition = new Vector2 (0, 0);

				// BUFFs FEATURE
				bf = new GameObject ("Buff Feature").AddComponent<UIBuffFeature> ();
				bf.init ();

				bf.transform.parent = transform;
				bf.transform.localPosition = new Vector2 (-1.5f, 0);

				// ACTIONS FEATURE
				af = new GameObject ("Action Feature").AddComponent<UIActionFeature> ();
				af.init ();

				af.transform.parent = transform;
				af.transform.localPosition = new Vector2 (1.5f, 0);
            }

            private class UIHealthFeature : CustomUIFeature {
                public override Sprite GetSprite() {
                    return Resources.Load<Sprite>("Sprites/UI_Heart");
                }

				public override string GetText() {
					return "1";
				}

				public override string GetTooltip ()
				{
					return "Tooltip";
				}
            }

			private class UIBuffFeature : CustomUIFeature {
				public override Sprite GetSprite () {
					return Resources.Load<Sprite> ("Sprites/Circle");
				}

				public override string GetText () {
					return "1";
				}

				public override string GetTooltip () {
					return "Tooltip";
				}
			}

			private class UIActionFeature : CustomUIFeature {
				public override Sprite GetSprite () {
					return Resources.Load<Sprite> ("Sprites/Circle");
				}

				public override string GetText () {
					return "";
				}

				public override string GetTooltip () {
					return "Tooltip";
				}
			}
        }


    }
}
