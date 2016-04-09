﻿/*
 * Andrew Amis, Nick Care, Robert Tomcik (2016)
 * The Hex Class
 *
 */

using UnityEngine;
using System.Collections;
using game.world.math;
using game.world.units;

namespace game.world {
	class Hex : MonoBehaviour {
		private HexModel model;
		public HexLoc loc { get; set; }
		public Unit unit { get; set; }
		bool selected;

		public void init(HexLoc loc) {
			this.loc = loc;

			model = new GameObject ("Hex Model").AddComponent<HexModel> ();
			model.init (this);

			model.transform.parent = transform;
			transform.localPosition = GameManager.l.HexPixel (loc);

			selected = false;
		}

		public void Select() {
			if (selected == true) {
				model.sr.color = Color.yellow;
			} else {
				model.sr.color = Color.white;
			}

		}

		public void Highlight() {
		
		}

		private class HexModel : MonoBehaviour {
			public SpriteRenderer sr;
			Hex h;

			public void init(Hex h) {
				this.h = h;
				transform.localPosition = new Vector3 (0, 0, Layer.Board);
				transform.localScale = new Vector3 (1.9f, 1.9f, 0);

				sr = gameObject.AddComponent<SpriteRenderer> ();
				sr.sprite = Resources.Load <Sprite>("Sprites/Hexagon");
				sr.material = new Material (Shader.Find ("Sprites/Default"));

			}
		}
	}
}