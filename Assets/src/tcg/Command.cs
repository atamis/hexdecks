using game.world;
using game.world.units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace game.tcg {
    abstract class Command {

        public Command() {

        }

        public abstract void Act(WorldMap w);

    }

    class MoveCommand : Command {
        private Hex h;
        private HeroUnit u;

        public MoveCommand(HeroUnit u, Hex h) : base() {
            this.u = u;
            this.h = h;
        }

        public override void Act(WorldMap w) {
            if (h.unit != null && h.unit != u) {
                h.unit.health--;
                h.unit.CheckDeath();
            }

            if (h.unit == null) {
                u.h = h;
            }
        }
    }
}
