using System;
using game.world.units;
using UnityEngine;
using UnityEngine.SceneManagement;
using game.tcg.cards;
using game.ui;
using System.Collections.Generic;

namespace game.world.triggers {
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

		internal TriggerModel model;
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
            dead = true;
        }

		// These are called before the move. See Hex#unit
		public abstract void UnitEnter(Unit u);

		public abstract void UnitLeave(Unit u);

		public abstract Sprite getSprite();

		void Update() {
			transform.localPosition = new Vector3(0, 0, 0);
            if (dead) {
                h.w.triggers.Remove(this);
                h = null;
                Destroy(gameObject);
            }
		}

		internal class TriggerModel : MonoBehaviour {
			public SpriteRenderer sr;
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

    class ChestTrigger : Trigger {
        private AudioManager am;
        private int chestType;

        public void init(Hex h, int chestType) {
            base.init(h);
            this.chestType = chestType;
        }

        public override Sprite getSprite() {
            return Resources.Load<Sprite>("Sprites/Tiles/T_Chest");
        }

        public override void UnitEnter(Unit u) {
            if (u.GetType() == typeof(HeroUnit)) {
                List<TCGCard> cards = GameManager.level.GetChestContents(chestType);
                foreach (TCGCard c in cards) {
                    print("Added " + c + " to the player's deck");
                    AudioManager.audioS.PlayOneShot(AudioManager.unlockSound);
                    GameManager.p.deck.Insert(0, c);
                    UIManager.ntm.AddText(GameManager.l.HexPixel(h.loc), "+card" + c.GetName(), Color.black);
                }
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

        private void effect(Hex t) {
            if (t.unit != null) {
                t.unit.ApplyDamage(1, null);
            }
        }

        public override void UnitEnter(Unit u) {
            print("Trap triggered at " + h);
            effect(h);
            foreach(Hex n in h.Neighbors()) {
                effect(n);
            }
            Suicide();
        }

        public override void UnitLeave(Unit u) {
        }
    }

	public class GodrayModel : MonoBehaviour {
		private SpriteRenderer sr;
		private float ticks = 0.0f;

		public void init() {
			sr = gameObject.AddComponent<SpriteRenderer> ();
			sr.sprite = Resources.Load<Sprite>("Sprites/godrays");
		}

		void Update() {
			ticks += 0.005f;
			sr.color = new Color(1, 1, 1, Mathf.Abs (Mathf.Sin (ticks)));
		}
	}
}