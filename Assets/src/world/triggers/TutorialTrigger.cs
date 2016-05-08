using UnityEngine;
using System.Collections;
using game.world.units;
using game.ui;
using game.math;

namespace game.world.triggers {
	class TutorialTrigger : Trigger {
		private string[] messages = new string[] {
			"Greetings Summoner! To move around the world click on and of the tiles!", 					// 0
			"Whenver you move, your enemies move as well, they get to attack too!",						// 3
			"A vicious enemies stand in your way! Navigate to them to attack!",							// 2
			"The enemy has seen you! Now everytime you move, they will move as well!",
			"You also have special cards that you can play! To see where you can use it hover over it",	// 4
			"Scattered around the world are some chests filled with ancient magicks. Collect them.", 	// 5
			"Your objective is to reach purple portal!",
			"It was a favorite prank of the merfolk to drop a boulder on an unsuspecting friend",		// 6
			"Now that you've harnessed the power of the efreet, you can cast fireballs",				// 7
			"You're as agile as a fish! You can jump around more!",										// 8
			"",																// 9		
			"Remenants of the previous crusades are littered about the crypt",							// 10
			"",
			"You can't travel through some terrain.",
		};
			
		public GodrayModel model2;
		public int id { get; set; }

		public override void init (Hex h) {
			base.init (h);
			this.id = id;

			model2 = new GameObject ("Godray Model").AddComponent<GodrayModel> ();
			model2.transform.parent = h.transform;
			model2.transform.localScale = new Vector3 (2f, 2f, 1);
			model2.transform.localPosition = new Vector3 (0, 0, Layer.HUD);
			model2.init ();
		}
			
		public override void UnitEnter (Unit u) {
			// POST THE MESSAGE
			UIManager.MakeMessage(messages[this.id]);

			switch (this.id) {
			case 7:
				UIManager.gc.SetGoal(GameManager.l.HexPixel (new HexLoc(37, 13, -50)));
				UIManager.gc.SetLock (true);
				break;
			default:
				break;
			}

			Destroy (model2.gameObject);
			Suicide();
		}

		public override void UnitLeave (Unit u) { }

		public override Sprite getSprite () { return null; }
	}
}

