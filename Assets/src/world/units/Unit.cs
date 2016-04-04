/*
 * Andrew Amis, Nick Care, Robert Tomcik (2016)
 * The Unit class
 *
 */

using UnityEngine;
using System.Collections;
using game.world.math;
using game.tcg;

namespace game.world.units {
	class Unit {
		public Hex h { get; set; }
		public HexLoc loc { get; set; }
		public int health { get; set; }
		public string name { get; set; }
		public Actor owner;

		public Unit() {
			
		}

		public void Destroy() {
			//Object.Destroy (this.model);

		}
	}
}

