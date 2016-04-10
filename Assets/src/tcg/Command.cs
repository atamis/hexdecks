using game.world;
using game.world.units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
}
