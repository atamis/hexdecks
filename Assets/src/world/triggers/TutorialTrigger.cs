using UnityEngine;
using System.Collections;
using game.world.units;
using game.ui;
using game.math;

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
			"Scattered around the world are some chests filled with ancient magicks. Collect them.", // 4
			"It was a favorite prank of the merfolk to drop a boulder on an unsuspecting friend",			// 5
			"Now that you've harnessed the power of the efreet, you can cast fireballs",			// 6
			"You're as agile as a fish! You can jump around more!",									// 7
			"",		// 8
			"Remenants of the previous crusades are littered about the crypt",						// 9
			"",
		};

		public int id { get; set; }

		public GodrayModel model2;

		public override void init (Hex h) {
			base.init (h);
			this.id = id;

			model2 = new GameObject ("Godray Model").AddComponent<GodrayModel> ();
			model2.transform.parent = h.transform;
			model2.transform.localScale = new Vector3 (2f, 2f, 1);
			model2.transform.localPosition = new Vector3 (0, 0, Layer.Hex - 0.1f);
			model2.init ();
		}
			
		public override void UnitEnter (Unit u) {
			// POST THE MESSAGE
			UIManager.MakeMessage(messages[this.id]);

			switch (this.id) {
			case 0:
				break;
			case 1:
				UIManager.gc.SetGoal(GameManager.l.HexPixel (new HexLoc(37, 13, -50)));
				UIManager.gc.SetLock (true);
				break;
			default:
				break;
			}

			Destroy (model2.gameObject);
			Suicide();
		}

		public override void UnitLeave (Unit u) {
			
		}

		public override Sprite getSprite () {
			return null;
		}
	}
}

