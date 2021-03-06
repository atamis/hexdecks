﻿using UnityEngine;
using System.Collections;
using game.world.units;
using game.ui;
using game.math;

namespace game.world.triggers {
	class TutorialTrigger : Trigger {
		private string[] messages = new string[] {
			"Greetings! Click on any hex and your player will take a step in that direction!",
			"Use WASD to move the camera, or press shift to lock the camera on you (the hero).", 					// 0
			"The enemy has seen you! Click on an adjacent enemy to attack them!",						// 3
			"The enemies attack too! Mousing over an enemy shows their health and attack pattern.",							// 2
			"Sleeping enemies don't move, but if you're close enough they wake up!",
			"Play cards to use special attacks! Hovering over a card highlights where you can use it.",	// 4
			"Open chests to unlock new cards! Chests add cards to the top of your deck.", 	// 5
			"Your goal each level is to reach purple portal!",
			"It was a favorite prank of the merfolk to drop a boulder on an unsuspecting friend",		// 6
			"Now that you've harnessed the power of the efreet, you can cast fireballs",				// 7
			"You're as agile as a fish! You can jump around more!",										// 8
			"",																// 9
			"Remenants of the previous crusades are littered about the crypt",							// 10
			"",
		};

		public GodrayModel model2;
		public int id { get; set; }

		public override void init (Hex h) {
			base.init (h);
			this.id = id;

			model2 = new GameObject ("Godray Model").AddComponent<GodrayModel> ();
			model2.transform.parent = h.transform;
			model2.transform.localScale = new Vector3 (2f, 2f, 1);
			model2.transform.localPosition = new Vector3 (0, 0, Layer.UnitsFX);
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
