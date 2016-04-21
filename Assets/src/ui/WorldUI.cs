using UnityEngine;
using System.Collections.Generic;
using game.tcg;

// TODO
// TOOLTIPS

namespace game.ui {
	class WorldUI : GameUI {
        private WorldHUD ib;
		private UIHexMenu menu;
	
        void Awake() {
            //uiFolder = new GameObject("UI Elements");
			font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");

			ib = new GameObject("Infobar").AddComponent<WorldHUD>();
            ib.init();

			menu = new GameObject ("Menu").AddComponent<UIHexMenu>();
			menu.gameObject.SetActive (false);

			overlay = new GameObject("Overlay").AddComponent<ScreenOverlay> ();

			CardManager cm = gameObject.AddComponent<CardManager> ();
        }

        void Update() {
			ib.transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * .1f, 10));

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
			private List<UICard> cards;
            private UIHealthFeature hf;
			private UIBuffFeature bf;
			private UIActionFeature af;
			private SpriteRenderer sr;

			Color[] cs = new Color[] { Color.red, Color.blue, Color.cyan, Color.green, Color.magenta };

            public void init() {
				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/UI/T_MainPanelNoIcons");

				// HEALTH FEATURE
                hf = new GameObject("Health Feature").AddComponent<UIHealthFeature>();
                hf.init();

				hf.transform.parent = transform;
				hf.transform.localPosition = new Vector3 (0, 0, -1);

				// BUFFs FEATURE
				bf = new GameObject ("Buff Feature").AddComponent<UIBuffFeature> ();
				bf.init ();

				bf.transform.parent = transform;
				bf.transform.localPosition = new Vector3 (-1.5f, 0, -1);

				// ACTIONS FEATURE
				af = new GameObject ("Action Feature").AddComponent<UIActionFeature> ();
				af.init ();

				af.transform.parent = transform;
				af.transform.localPosition = new Vector3 (1.5f, 0, -1);

				// CARDS
				cards = new List<UICard> ();
				for (int i = 0; i < 5; i++) {
					UICard c = new GameObject ("Card").AddComponent<UICard> ();
					c.init ();
					c.SetColor (cs [i]);

					c.transform.localPosition = new Vector3 (0, 1, 0);
					c.transform.parent = transform;
					//c.SetOrigin (Camera.main.ScreenToWorldPoint(new Vector3(px, py, 1)));
					cards.Add(c);
				}
            }

			public Vector3[] card_locs = new Vector3[] { 
				new Vector3 (2f, .75f, 0), new Vector3 (1f, 1f, 0), new Vector3 (0f, 1.25f, 0), 
				new Vector3 (-1f, 1f, 0), new Vector3 (-2f, .75f, 0),
			};
				
			void Update() {
				int i = 0;
				foreach (UICard c in cards) {
					c.SetOrigin(transform.position + card_locs[i]);
					//c.transform.localEulerAngles = new Vector3 (0, 0, 45 + 18 * i);
					i++;
				}
			}

            private class UIHealthFeature : CustomUIFeature {
                public override Sprite GetSprite() {
                    return Resources.Load<Sprite>("Sprites/UI/T_Heart");
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
					return Resources.Load<Sprite> ("Sprites/UI/T_LightningIcon");
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
					return Resources.Load<Sprite> ("Sprites/UI/T_PlusIcon");
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
