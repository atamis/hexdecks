using game.tcg.cards;
using game.world;
using game.world.units;
using UnityEngine;

namespace game.tcg {
    abstract class Command {
		public Command() {}

		public abstract void Act(WorldMap w);
	}

	class MoveCommand : Command {
		private Hex h;
		private HeroUnit u;
        private AudioManager am;

		public MoveCommand(HeroUnit u, Hex h) : base() {
			this.u = u;
			this.h = h;
		}

		public override void Act(WorldMap w) {
			var path = WorldPathfinding.Pathfind(w, u.h, h);

            if (path == null) {
                return;
            }

			if (path.Count < 2) {
				return;
			}
			var next = path.First.Next.Value;


			if (next.unit != null && next.unit != u) {
                //am = h.w.gm.audiom;
				//am.audioS.PlayOneShot(heroAttackSound);
				next.unit.ApplyDamage(1, w.hero);
			}

			if (next.unit == null) {
				u.h = next;
			}
		}
	}

    class CardCommand : Command {
        private TCGCard c;
        private Hex h;

        public CardCommand(TCGCard c, Hex h) {
            this.c = c;
            this.h = h;
        }

        public override void Act(WorldMap w) {
            c.OnPlay(w, h);
        }
    }
}

