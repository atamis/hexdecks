using UnityEngine;
using System.Collections.Generic;
using game.math;
using game.world;
using game.tcg;

// TODO
// TOOLTIPS

namespace game.ui {
	class WorldUI : MonoBehaviour {
		public static Font font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");
		internal GameCamera gc;

		public GameManager gm;
        private WorldHUD ib;
		private UIHexMenu menu;
		private bool starting;

		public void init(GameManager gm) {
			this.gm = gm;
			this.name = "UI";

			gc = new GameObject ("Game Camera").AddComponent<GameCamera> ();
			gc.init (Camera.main);

			ib = new GameObject("Infobar").AddComponent<WorldHUD>();
			ib.init(this);
			ib.transform.parent = gc.transform;

			menu = new GameObject ("Menu").AddComponent<UIHexMenu>();
			menu.gameObject.SetActive (false);
			menu.transform.parent = gc.transform;
		}

        void Update() {
			// UPDATE HUD LOCATION
			Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height * .1f, 10));
			ib.transform.position = new Vector3 (pos.x, pos.y, Layer.HUD);

			if (Input.GetKeyDown (KeyCode.Escape)) {
				this.menu.gameObject.SetActive (!menu.gameObject.activeSelf);
				this.ib.gameObject.SetActive (!ib.gameObject.activeSelf);
			}

			// HANDLE MOVEMENT
			if (Input.GetMouseButtonUp (0)) {
				RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
				if (hit.collider != null) {
					if (hit.collider.gameObject.tag == "Hex") {
						Hex h = MathLib.GetHexAtMouse(gm.world);

						if (h != null && gm.world.hero.h != h && GameManager.p.nextCommand == null) {
							GameManager.p.nextCommand = new MoveCommand(gm.world.hero, h);
						}
					}
				}
			}
        }

		public abstract class CustomUIFeature : MonoBehaviour {
			internal SpriteRenderer sr;
			private PolygonCollider2D coll;

			internal TextMesh tm;
			private GameObject textObj;

			public abstract string GetText();

			public abstract string GetTooltip();

			public abstract Sprite GetSprite();

			private UITooltip tooltip;

			public WorldUI ui;

			public void init(WorldUI ui) {
				this.ui = ui;
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

				coll = gameObject.AddComponent<PolygonCollider2D> ();
				coll.isTrigger = true;

				tooltip = new GameObject ("UI Tooltip").AddComponent<UITooltip> ();
				tooltip.init (GetTooltip());

				tooltip.transform.parent = transform;
				tooltip.gameObject.SetActive (false);
			}

			void OnMouseOver() {
				tooltip.gameObject.SetActive (true);
			}

			void OnMouseExit() {
				tooltip.gameObject.SetActive (false);
			}

			private class UITooltip : MonoBehaviour {
				private SpriteRenderer sr;
				private string text;

				private TextMesh tm;
				private GameObject textObj;

				public void init(string text) {
					this.text = text;
				
					sr = gameObject.AddComponent<SpriteRenderer> ();
					sr.sprite = Resources.Load<Sprite> ("Sprites/UI/T_Wood");
					gameObject.transform.localScale = new Vector3 (.5f, .5f, 1);

					textObj = new GameObject("UI Text");
					textObj.transform.parent = transform;
					textObj.transform.localPosition = new Vector3(0, 0, -1);

					tm = textObj.AddComponent<TextMesh>();
					tm.text = this.text;
					tm.color = Color.black;
					tm.alignment = TextAlignment.Center;
					tm.anchor = TextAnchor.MiddleCenter;
					tm.fontSize = 148;
					tm.characterSize = 0.03f;
					tm.font = WorldUI.font;
					tm.GetComponent<Renderer>().material = font.material;
				}

				void Update() {
					Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					transform.position = new Vector3(pos.x, pos.y, Layer.Tooltip);
				}
			}
		}

        private class WorldHUD : MonoBehaviour {
			private List<UICard> cards;
            private UIHealthFeature hf;
			private UIBuffFeature bf;
			private UIActionFeature af;
			private SpriteRenderer sr;

			Color[] cs = new Color[] { 
				Color.red, Color.blue, Color.cyan, Color.green, Color.magenta 
			};

			public Vector3[] card_locs = new Vector3[] { 
				new Vector3 (2f, .75f, 0), new Vector3 (1f, 1f, 0), new Vector3 (0f, 1.25f, 0), 
				new Vector3 (-1f, 1f, 0), new Vector3 (-2f, .75f, 0),
			};

			private WorldUI ui;

			public void init(WorldUI ui) {
				this.ui = ui;
				this.gameObject.layer = LayerMask.NameToLayer ("UI");

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/UI/T_MainPanelNoIcons");

				// HEALTH FEATURE
                hf = new GameObject("Health Feature").AddComponent<UIHealthFeature>();
                hf.init(ui);

				hf.transform.parent = transform;
				hf.transform.localPosition = new Vector3 (0, 0, -1);

				// BUFFs FEATURE
				bf = new GameObject ("Buff Feature").AddComponent<UIBuffFeature> ();
				bf.init (ui);

				bf.transform.parent = transform;
				bf.transform.localPosition = new Vector3 (-1.5f, 0, -1);

				// ACTIONS FEATURE
				af = new GameObject ("Action Feature").AddComponent<UIActionFeature> ();
				af.init (ui);

				af.transform.parent = transform;
				af.transform.localPosition = new Vector3 (1.5f, 0, -1);

				// CARDS
				cards = new List<UICard> ();
				for (int i = 0; i < 5; i++) {
					UICard c = new GameObject ("Card").AddComponent<UICard> ();
					c.init (ui, i);
					//c.SetColor (cs [i]);

					c.transform.localPosition = new Vector3 (0, 1, 0);
					c.transform.parent = transform;
					//c.SetOrigin (Camera.main.ScreenToWorldPoint(new Vector3(px, py, 1)));
					cards.Add(c);
				}
            }
				
			void Update() {
				// UPDATE THE CARD LOCATIONS
				int i = 0;
				while (i < 5) {
					cards [i].SetOrigin (transform.position + card_locs [i]);
					i++;
				}

			}

            private class UIHealthFeature : CustomUIFeature {
                public override Sprite GetSprite() {
                    return Resources.Load<Sprite>("Sprites/UI/T_Heart");
                }

				public override string GetText() {
					return ui.gm.world.hero.health.ToString();
				}

				public override string GetTooltip () {
					return "Health";
				}

				void Update() {
					this.tm.text = GetText ();
				}
            }

			private class UIBuffFeature : CustomUIFeature {
				public override Sprite GetSprite () {
					return Resources.Load<Sprite> ("Sprites/UI/T_PlusIcon");

				}

				public override string GetText () {
					// TODO
					// Buff Duration
					return "1";
				}

				public override string GetTooltip () {
					return "Buff";
				}
			}

			private class UIActionFeature : CustomUIFeature {
				public override Sprite GetSprite () {
					return Resources.Load<Sprite> ("Sprites/UI/T_LightningIcon");
				}

				public override string GetText () {
					return "";
				}

				public override string GetTooltip () {
					return "Actions";
				}
			}
        }
    }
}
