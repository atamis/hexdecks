using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections.Generic;
using game.math;
using game.world;
using game.tcg;
using game.tcg.cards;
using game.ui.features;

namespace game.ui {
	class GUIWorld : GUIBase {
        public MagnifiedCardModel magCard;
        private WorldHUD ib;
		private UIHexMenu menu;
		private bool starting;

		void Awake() {
			this.name = "UI";

			ib = new GameObject("Infobar").AddComponent<WorldHUD>();
			ib.init(this);
			ib.transform.parent = UIManager.gc.transform;

			menu = new GameObject ("Menu").AddComponent<UIHexMenu>();
			menu.gameObject.SetActive (false);
			menu.transform.parent = UIManager.gc.transform;

			magCard = new GameObject("Magnified Card").AddComponent<MagnifiedCardModel>();
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
						Hex h = MathLib.GetHexAtMouse(GameManager.world);

						if (h != null && GameManager.world.hero.h != h && GameManager.p.nextCommand == null) {
							GameManager.p.nextCommand = new MoveCommand(GameManager.world.hero, h);
						}
					}
				}
			}
        }

		public override void Delete () {
			Destroy (ib.gameObject);
			Destroy (menu.gameObject);
			Destroy (magCard.gameObject);
		}

		public abstract class CustomUIFeature : MonoBehaviour {
			internal SpriteRenderer sr;
			private PolygonCollider2D coll;
			internal TextMesh tm;
			private GameObject textObj;
			private UITooltip tooltip;
			public GUIWorld ui;

			public abstract string GetText();

			public abstract string GetTooltip();

			public abstract Sprite GetSprite();

			public void init(GUIWorld ui) {
				this.ui = ui;
				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = GetSprite();

				textObj = new GameObject("UI Text");
				textObj.transform.parent = transform;
				textObj.transform.localPosition = new Vector3(0, 0, -0.5f);

				tm = textObj.AddComponent<TextMesh>();
				tm.color = Color.black;
				tm.alignment = TextAlignment.Center;
				tm.anchor = TextAnchor.MiddleCenter;
				tm.fontSize = 148;
				tm.characterSize = 0.04f;
				tm.font = UIManager.font;
				tm.GetComponent<Renderer>().material = UIManager.font.material;

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

				public TextMesh tm;
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
					tm.font = UIManager.font;
					tm.GetComponent<Renderer>().material = UIManager.font.material;
				}

				void Update() {
					Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
					transform.position = new Vector3(pos.x, pos.y, Layer.Tooltip);
				}
			}
		}

        public class GameOverScreen : CustomUIFeature {
            public override Sprite GetSprite() {
                return null;
            }

            public override string GetText() {
                return "YOU DIED";
            }

            public override string GetTooltip() {
                return "YOU DIED";
            }
        }

        private class WorldHUD : MonoBehaviour {
			private List<UICard> cards;
            private UIHealthFeature hf;
			//private UIBuffFeature bf;
			//private UIActionFeature af;
			private SpriteRenderer sr;
            private GameOverScreen goScreen;
            private float deathTime;

			public Vector3[] card_locs = new Vector3[] {
				new Vector3 (2f, .75f, 0), new Vector3 (1f, 1f, 0), new Vector3 (0f, 1.25f, 0),
				new Vector3 (-1f, 1f, 0), new Vector3 (-2f, .75f, 0),
			};

			private GUIWorld ui;

			public void init(GUIWorld ui) {
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
				//bf = new GameObject ("Buff Feature").AddComponent<UIBuffFeature> ();
				//bf.init (ui);

				//bf.transform.parent = transform;
				//bf.transform.localPosition = new Vector3 (-1.5f, 0, -1);

				// ACTIONS FEATURE
				//af = new GameObject ("Action Feature").AddComponent<UIActionFeature> ();
				//af.init (ui);

				//af.transform.parent = transform;
				//af.transform.localPosition = new Vector3 (1.5f, 0, -1);

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

                if (goScreen == null && GameManager.world.hero.dead) {
                    goScreen = new GameObject("End Game Screen").AddComponent<GameOverScreen>();
                    goScreen.init(ui);
                    goScreen.tm.color = Color.red;

                    goScreen.transform.parent = transform;
                    goScreen.transform.localPosition = new Vector3(0, 4f, -1);

                    deathTime = Time.time;
                }

                if (goScreen != null && Time.time - deathTime > 5) {
                    SceneManager.LoadScene("Main");
                }

            }

            private class UIHealthFeature : CustomUIFeature {
                public override Sprite GetSprite() {
                    return Resources.Load<Sprite>("Sprites/UI/T_Heart");
                }

				public override string GetText() {
					return GameManager.world.hero.health.ToString();
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

        public class MagnifiedCardModel : MonoBehaviour {
            public TCGCard card;
            private SpriteRenderer sr;
            private GameObject titleObj;
            private TextMesh titleTm;

            private GameObject descObj;
            private TextMesh descTm;

            private bool hovered;
            private BoxCollider2D bc;

            void Start() {
                transform.localScale = new Vector3(2f, 2f, 1);

                sr = gameObject.AddComponent<SpriteRenderer>();
                sr.sprite = Resources.Load<Sprite>("Sprites/UI/T_CardBase");

                var font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");

                titleObj = new GameObject("Card Text");
                titleObj.transform.parent = transform;
                titleObj.transform.localPosition = new Vector3(-0.40f, 0.5f, -0.3f);
                titleObj.transform.localScale = new Vector3(1f, 1f, 1f);

                titleTm = titleObj.AddComponent<TextMesh>();

                //titleTm.text = card.GetName();
                titleTm.fontSize = 148;
                titleTm.characterSize = 0.008f;
                titleTm.color = Color.black;
                titleTm.font = font;

                titleTm.GetComponent<Renderer>().material = font.material;

                descObj = new GameObject("Card Text");
                descObj.transform.parent = transform;
                descObj.transform.localPosition = new Vector3(-0.40f, 0.25f, -0.3f);
                descObj.transform.localScale = new Vector3(1f, 1f, 1f);

                descTm = descObj.AddComponent<TextMesh>();

                //titleTm.text = card.GetName();
                descTm.fontSize = 148;
                descTm.characterSize = 0.006f;
                descTm.color = Color.black;
                descTm.font = font;

                descTm.GetComponent<Renderer>().material = font.material;


                bc = gameObject.AddComponent<BoxCollider2D>();
                bc.size = new Vector3(1.0f, 1.33f, 0);
                bc.isTrigger = true;


                Hide();

            }

            void Show() {
                sr.enabled = true;
                titleTm.text = card.GetName();

                string builder = "";
                descTm.text = "";
                float rowLimit = 0.83f; //find the sweet spot
                string text = card.getDescription();
                string[] parts = text.Split(' ');
                for (int i = 0; i < parts.Length; i++) {
                    descTm.text += parts[i] + " ";
                    if (descTm.GetComponent<Renderer>().bounds.extents.x > rowLimit) {
                        descTm.text = builder.TrimEnd() + System.Environment.NewLine + parts[i] + " ";
                    }
                    builder = descTm.text;
                }

            }

            void Hide() {
                sr.enabled = false;
                titleTm.text = "";
                descTm.text = "";
            }

            void Update() {
                Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width * 5f / 6f, Screen.height * 0.75f, 10));
                transform.position = new Vector3(pos.x, pos.y, Layer.HUD);


                if (card != null) {
                    Show();
                } else {
                    Hide();
                }
            }
        }
    }
}
