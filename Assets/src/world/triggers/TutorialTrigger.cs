using UnityEngine;
using System.Collections;
using game.world.units;
using game.ui;

/*
 * 0 - intro
 * 1 - move camera to location
 * 2 - enemy basics
 * 3 - enemy movement
 * 4 - chests
 * 5 - description of a card
 * 6 -
 * 7 -
 * 
 * 
 */

namespace game.world.triggers {
	class TutorialTrigger : Trigger {
		private string[] messages = new string[] {
			"Hello Player! To move around the world click on and of the tiles!", 					// 0
			"Your objective is to reach purple portal!",											// 1
			"But vicious enemies stand in your way! Click on them to attack",						// 2
			"Whenver you move, your enemies move as well, they get to attack too!",					// 3
			"Scattered around the world are some chests filled with ancient magicks. Collect them", // 4
			"They say that the fae are a slippery folk, you too can dash around quickly",			// 5
			"Now that you've harnessed the power of the efreet, you can cast fireballs",			// 6
			"You're as agile as a fish! You can jump around more!",									// 7
			"It was a favorite prank of the merfolk to drop a boulder on an unsuspecting friend",	// 8
			"Remenants of the previous crusades are littered about the crypt",						// 9
			"",
		};

		private int id = 0;

		public void init (Hex h, int id) {
			base.init (h);
			this.id = id;
		}

		public override void UnitEnter (Unit u) {
			// Post Message
			new GameObject("Message Box").AddComponent<UIMessageBox>().init(messages[this.id]);

			switch (this.id) {
			case 0:
				break;
			case 1:
				// move camera to objective location
				break;
			default:
				break;
			}
		}

		public override void UnitLeave (Unit u) {
			
		}

		public override Sprite getSprite () {
			return null;
		}
	}
}

