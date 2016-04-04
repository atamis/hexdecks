using UnityEngine;
using System.Collections;
using game.world.math;

namespace game.world.units {
	class HeroUnit : Unit {
		private HeroModel model;

		public HeroUnit(string name)  {
			this.health = 20;

			model = new GameObject ("Hero Model").AddComponent<HeroModel> ();
			model.init (this);

			model.transform.position = GameManager.l.HexPixel (loc);
		}

		private class HeroModel : MonoBehaviour {
			SpriteRenderer sr;
			HeroUnit hu;

			public void init(HeroUnit hu) {
				this.hu = hu;

				gameObject.transform.position = new Vector3 (0, 0, -1);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Sprites/Circle");

				sr.color = new Color (1, 1, 1);
			}

			void Update() {

			}
		}
	}
}
