﻿using UnityEngine;
using System.Collections;
using game.world.units;

namespace game.world.triggers {
	class EndLevelTrigger : Trigger {
		bool endingGame;
		float timer = 10;

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
			if (endingGame) {
				timer -= Time.deltaTime;

				if (timer <= 0) {
					GameManager.LoadLevel (GameManager.level.GetNextLevel ());
				}
			}
			ticks += Time.deltaTime;
		}

		// TODO
		// Glorious "You've got mail message box"

		public override void UnitEnter(Unit u) {
			if (u.GetType() == typeof(HeroUnit)) {
				endingGame = true;
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

