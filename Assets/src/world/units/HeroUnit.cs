using UnityEngine;
using System.Collections.Generic;

namespace game.world.units {
	class HeroUnit : Unit {
		Sprite[] sprites = new Sprite[3] {
			Resources.Load<Sprite>("Sprites/Hero/T_HeroIdle1"),
			Resources.Load<Sprite>("Sprites/Hero/T_HeroIdle2"),
			Resources.Load<Sprite>("Sprites/Hero/T_HeroIdle3")
		};

		const float spriteInterval = .8f;
		float lastSwitch;
		int idx;

		public void init(WorldMap w, Hex h) {
			base.init(w, h, 10);

			idx = 0;
			lastSwitch = timer;

		}

		public override Sprite getSprite() {
			if(timer >= lastSwitch + spriteInterval) {
				lastSwitch = timer;
				idx = idx + 1;
				idx = idx % 3;
			}
			return sprites[idx];
		}

		public override List<Hex> GetAttackPattern ()
		{
			return new List<Hex> ();
		}
	}
}

