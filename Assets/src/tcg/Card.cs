using UnityEngine;
using System.Collections;
using game.world;
using game.world.math;
using System.Collections.Generic;

namespace game.tcg {
	public enum Targets {
		Empty,
		Minion,
		Hero,
	};

	abstract class Card {
		public string name { get; set; }
        public string bodyText { get; set; };
		public int cost { get; set; }
        
        abstract public bool CanPlay(WorldMap w, Hex h);

        public abstract void OnPlay(WorldMap w, Hex h);

        public abstract List<Hex> ValidTargets(WorldMap w, Hex h); 
        
        
	}
}
