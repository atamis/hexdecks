using System;
using game.world.units;
using UnityEngine;
using UnityEngine.SceneManagement;
using game.tcg.cards;

namespace game.world {
	abstract class Trigger : MonoBehaviour {
		private Hex _h;
		public Hex h {
			get {
				return _h;
			}

			set {
				if (_h != null) {
					h.triggers.Remove(this);
				}

				_h = value;

				if (_h != null) {
					h.triggers.Add(this);
				}
			}
		}

		TriggerModel model;
        private bool dead;

        public virtual void init(Hex h) {
			this.h = h;
            this.dead = false;

			transform.parent = h.transform;
			transform.localPosition = new Vector3(0, 0, 0);

			var obj = new GameObject("Trigger Model");
			obj.transform.parent = transform;
			model = obj.AddComponent<TriggerModel>();

			model.init(this);

		}

        internal void Suicide() {
            this.dead = true;
        }

		// These are called before the move. See Hex#unit
		public abstract void UnitEnter(Unit u);

		public abstract void UnitLeave(Unit u);

		public abstract Sprite getSprite();

		void Update() {
			transform.localPosition = new Vector3(0, 0, 0);
            if (dead) {
                h = null;
                Destroy(gameObject);
            }
		}

		private class TriggerModel : MonoBehaviour {
			SpriteRenderer sr;
			Trigger t;

			public void init(Trigger t) {
				this.t = t;

				gameObject.transform.localPosition = LayerV.HeroUnit;

				sr = gameObject.AddComponent<SpriteRenderer>();
                sr.sprite = t.getSprite();
			}

			void Update() {
				transform.localPosition = LayerV.HeroUnit;
				sr.sprite = t.getSprite();
			}
		}

	}

	class LogTrigger : Trigger {
		public override Sprite getSprite() {
			return null;
		}

		public override void init(Hex h) {
			base.init(h);
		}

		public override void UnitEnter(Unit u) {
			print(u + " entered " + h);
		}

		public override void UnitLeave(Unit u) {
			print(u + " left " + h);
		}
	}

	class EndLevelTrigger : Trigger {
		bool endingGame;
		float timer = 10;

		public override void init(Hex h) {
			base.init(h);
		}

		void Update() {
			if (endingGame) {
				timer -= Time.deltaTime;

				if (timer <= 0) {
					SceneManager.LoadSceneAsync("Main");
				}
			}
		}

		void OnGUI() {
			if (endingGame) {
				GUI.color = Color.red;
				GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 120, 120), "You've got mail (1) " + timer);
			}
		}

		public override void UnitEnter(Unit u) {
			if (u.GetType() == typeof(HeroUnit)) {
				endingGame = true;
			}
		}

		public override void UnitLeave(Unit u) {

		}

		public override Sprite getSprite() {
			return Resources.Load<Sprite>("Sprites/Diamond");
		}
	}

    class ChestTrigger : Trigger {
        TCGCard c;
        public void init(Hex h, TCGCard c) {
            base.init(h);
            this.c = c;
        }

        public override Sprite getSprite() {
            return Resources.Load<Sprite>("Sprites/Tiles/T_Chest");
        }

        public override void UnitEnter(Unit u) {
            if (u.GetType() == typeof(HeroUnit)) {
                print("Added " + c + " to the player's deck");
                GameManager.p.deck.Add(c);
                GameManager.ntm.AddText(GameManager.world.l.HexPixel(h.loc), "+card");
                Suicide();
            }
        }

        public override void UnitLeave(Unit u) {
            
        }
    }

    class TrapTrigger : Trigger {
        public override Sprite getSprite() {
            return Resources.Load<Sprite>("Sprites/Tiles/T_Trap");
        }

        public override void UnitEnter(Unit u) {
            u.ApplyDamage(5);
            Suicide();
        }

        public override void UnitLeave(Unit u) {
        }
    }
}