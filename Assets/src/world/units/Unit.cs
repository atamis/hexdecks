/*
 * Andrew Amis, Nick Care, Robert Tomcik (2016)
 * The Unit class
 *
 */

using UnityEngine;
using System.Collections;
using game.world.math;

namespace game.world.units {
	class Unit {
		UnitModel model;
		public Hex h { get; set; }
		public HexLoc loc { get; set; }

		public Unit(HexLoc loc) {
			this.loc = loc;

			model = new GameObject ("Unit Model").AddComponent<UnitModel> ();
			model.init (this);

			model.transform.position = GameManager.l.HexPixel (loc);
		}

		public void Destroy() {
			Object.Destroy (this.model);

		}

		private class UnitModel : MonoBehaviour {
			private SpriteRenderer sr;
			Unit u;

			public void init(Unit u) {
				this.u = u;

				transform.localPosition = new Vector3 (0, 0, Layer.Unit);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load<Sprite> ("Circle");
				sr.color = Color.green;
			}

			void Update() {

			}
		}
	}
}

