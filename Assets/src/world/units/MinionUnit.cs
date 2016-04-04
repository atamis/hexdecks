using UnityEngine;
using System.Collections;

namespace game.world.units {
	class MinionUnit : Unit {
		private MinionModel model;

		public MinionUnit(string name, int health) {
			this.name = name;
			this.health = health;

			model = new GameObject ("Minion Model").AddComponent<MinionModel> ();
			model.init (this);

			model.transform.position = GameManager.l.HexPixel (loc);
		}
			
		private class MinionModel : MonoBehaviour {
			SpriteRenderer sr;
			MinionUnit mu;

			public void init(MinionUnit mu) {
				this.mu = mu;

				gameObject.transform.localPosition = new Vector3 (0, 0, Layer.Unit);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprite/Square");

				sr.color = Color.cyan;
			}

			void Update() {

			}
		}
	}
}