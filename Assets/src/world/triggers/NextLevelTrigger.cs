using UnityEngine;
using System.Collections;
using game.world.units;
using game.ui;
using game.ui.features;

namespace game.world.triggers {
	class EndLevelTrigger : Trigger {
		bool endingGame;
		//float timer = 10;

		private int idx = 0;
		private float ticks;
		const float spriteInterval = .8f;
		float lastSwitch;

		private Sprite[] sprites = new Sprite[] {
			Resources.Load<Sprite>("Sprites/Tiles/T_Entrance0"),
			Resources.Load<Sprite>("Sprites/Tiles/T_Entrance1"),
		};

		public override void init(Hex h) {
			base.init(h);
		}

		void Update() {
			ticks += Time.deltaTime;
		}

		// TODO
		// Glorious "You've got mail message box"

		public override void UnitEnter(Unit u) {
			if (u.GetType() == typeof(HeroUnit)) {
                AudioManager.playerVictory();
				UIManager.gc.SetLock (true);

				var menu = new GameObject ("Continue Menu").AddComponent<UIContinueMenu> ();
				menu.transform.parent = UIManager.gc.transform;
				menu.transform.localPosition = new Vector3 (0, 0, Layer.HUD);
				menu.init ();
				//endingGame = true;
			}
		}

		public override void UnitLeave(Unit u) { }

		public override Sprite getSprite() {
			if (ticks >= lastSwitch + spriteInterval) {
				lastSwitch = ticks;
				idx = idx + 1;
				idx = idx % 2;
			}
			return sprites[idx];
		}
	}
}

