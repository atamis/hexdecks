using System.Text;
using UnityEngine;
using game.world;
using game.world.units;

namespace game.tcg {
    abstract class Command {

        public Command() {

        }

        public abstract void Act(WorldMap w);

    }

    class MoveCommand : Command {
        private Hex h;
        private HeroUnit u;
        private AudioSource audioS;
        private AudioClip attackSound;

        public MoveCommand(HeroUnit u, Hex h) : base() {
            this.u = u;
            this.h = h;
            audioS = u.w.gm.GetComponent<AudioSource>();
            attackSound = Resources.Load<AudioClip>("Sounds/take melee damage edited");
        }

        public override void Act(WorldMap w) {
            var path = WorldPathfinding.Pathfind(w, u.h, h);

            if (path.First.Next == null) {
                return;
            }
            var next = path.First.Next.Value;


            if (next.unit != null && next.unit != u) {
                audioS.PlayOneShot(attackSound);
                next.unit.ApplyDamage(1);
            }

            if (next.unit == null) {
                u.h = next;
            }
        }
    }

    class AOECommand : Command {
        private HeroUnit u;

        public AOECommand(HeroUnit u) : base() {
            this.u = u;
        }

        public override void Act(WorldMap w) {
            foreach(Hex h in u.h.Neighbors()) {
                if (h.unit != null) {
                    // WARNING: friendly fire
                    h.unit.ApplyDamage(1);
                }
            }
        }
    }

    class InvincibleCommand : Command {
        private HeroUnit u;

        public InvincibleCommand(HeroUnit u) : base() {
            this.u = u;
        }

        public override void Act(WorldMap w) {
            u.invincible.duration = 2;
        }
    }

    class DoubleActionCommand : Command {
        private Player p;

        public DoubleActionCommand(Player p) : base() {
            this.p = p;
        }

        public override void Act(WorldMap w) {
            p.turns = 2;
        }
    }
}
