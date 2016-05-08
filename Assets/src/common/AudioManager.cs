using UnityEngine;
using System.Collections;

namespace game {
	class AudioManager : MonoBehaviour {
        public static AudioSource t1;
        public static AudioSource t2;
        public static AudioSource effects;
        public static AudioSource waterS;
        public static AudioSource victoryS;
        private AudioClip startTrack;
        public static AudioClip track1;
        public static AudioClip track2;
        public static AudioClip waterTrack;
		public static AudioClip drawSound = Resources.Load<AudioClip>("Audio/UI/DrawCard");
		public static AudioClip shuffleSound = Resources.Load<AudioClip> ("Audio/UI/Shuffle");
		public static AudioClip unlockSound = Resources.Load<AudioClip>("Audio/World/OpenLock");
		public static AudioClip meleeSound = Resources.Load<AudioClip>("Audio/World/MeleeDamage");
		public static AudioClip arrowSound = Resources.Load<AudioClip>("Audio/World/RangedDamage");
        public static AudioClip aggroSound = Resources.Load<AudioClip>("Audio/World/Aggro Sound");
        public static AudioClip deathSound = Resources.Load<AudioClip>("Audio/World/PlayerDies");
        public static AudioClip victorySound = Resources.Load<AudioClip>("Audio/World/VictoryMusic");
        public static AudioClip boulderDamage = Resources.Load<AudioClip>("Audio/World/enemy damage sounds/boulder damage");
        public static AudioClip[] enemySounds = new AudioClip[5]
        {
            Resources.Load<AudioClip>("Audio/World/enemy damage sounds/goblin 1"),
            Resources.Load<AudioClip>("Audio/World/enemy damage sounds/goblin 2"),
            Resources.Load<AudioClip>("Audio/World/enemy damage sounds/goblin 3"),
            Resources.Load<AudioClip>("Audio/World/enemy damage sounds/goblin 4"),
            Resources.Load<AudioClip>("Audio/World/enemy damage sounds/goblin 5")
        };
        public static int i;
        public static bool vfade;
        public static bool t1fade;
        public static bool t2fade;
        public static float vtimer;
        //public static AudioClip[] water = new AudioClip[] {

        //};
        public static AudioClip waterLoop;
        //private bool looping = false;

        void Start() {
            track1 = Resources.Load<AudioClip>("Audio/Soundtrack/Track 2");
            track2 = Resources.Load<AudioClip>("Audio/Soundtrack/Track 1 part 2");
            waterLoop = Resources.Load<AudioClip>("Audio/World/water/water sound good for loop");

            t1 = gameObject.AddComponent<AudioSource>();
            t1.clip = track1;
            t1.loop = true;
            t2 = gameObject.AddComponent<AudioSource>();
            t2.clip = track2;
            t2.loop = true;
            waterS = gameObject.AddComponent<AudioSource>();
            victoryS = gameObject.AddComponent<AudioSource>();
            victoryS.clip = victorySound;
            victoryS.loop = true;
            effects = gameObject.AddComponent<AudioSource>();
            vfade = false;
            t1fade = false;
            t2fade = false;

            playTrack1();

   
        }

        public static void playTrack1()
        {
            if (!t1.isPlaying)
            {
                vfade = true;
                t2fade = true;
                t1.volume = 1;
                t1.Play();
            }
        }

        public static void playTrack2()
        {
            if (!t2.isPlaying)
            {
                vfade = true;
                t1fade = true;
                t2.time = 1;
                t2.volume = 1;
                t2.Play();
            }
        }

        public static void playWaterLoop()
        {
            waterS.clip = waterLoop;
            waterS.loop = true;
            waterS.Play();
        }

        public static void playerDeath()
        {
            t1.Stop();
            t2.Stop();
            effects.PlayOneShot(deathSound, 2f);
        }

        public static void playerVictory()
        {
            t1.Stop();
            t2.Stop();
            victoryS.loop = true;
            victoryS.volume = 1;
            victoryS.Play();
        }

        public static void enemyDamage()
        {
            System.Random rng = new System.Random();
            i = rng.Next(5);
            effects.PlayOneShot(enemySounds[i]);
        }

        void Update() {
            if (vfade)
            {
                victoryS.volume -= Time.deltaTime * .33f;
                if(victoryS.volume <= 0)
                { 
                    vfade = false;
                    victoryS.Stop();
                }
            }
            if (t1fade)
            {
                t1.volume -= Time.deltaTime * .33f;
                if(t1.volume <= 0)
                {
                    t1fade = false;
                    t1.Stop();
                }
            }
            if (t2fade)
            {
                t2.volume -= Time.deltaTime * .33f;
                if (t2.volume <= 0)
                {
                    t2fade = false;
                    t2.Stop();
                }
            }

        }

        void Awake() {

		}
	}
}

