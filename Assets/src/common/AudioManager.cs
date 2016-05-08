using UnityEngine;
using System.Collections;

namespace game {
	class AudioManager : MonoBehaviour {
        public static AudioSource audioS;
        public static AudioSource waterS;
        private AudioClip startTrack;
        private AudioClip loopTrack;
        private AudioClip waterTrack;
		public static AudioClip drawSound = Resources.Load<AudioClip>("Audio/UI/DrawCard");
		public static AudioClip shuffleSound = Resources.Load<AudioClip> ("Audio/UI/Shuffle");
		public static AudioClip unlockSound = Resources.Load<AudioClip>("Audio/World/OpenLock");
		public static AudioClip meleeSound = Resources.Load<AudioClip>("Audio/World/MeleeDamage");
		public static AudioClip arrowSound = Resources.Load<AudioClip>("Audio/World/RangedDamage");
        public static AudioClip aggroSound = Resources.Load<AudioClip>("Audio/World/Aggro Sound");
        //public static AudioClip[] water = new AudioClip[] {

        //};
        public static AudioClip waterLoop;
        //private bool looping = false;

        void Start() {
            startTrack = Resources.Load<AudioClip>("Audio/Soundtrack/Track 1 part 1");
            loopTrack = Resources.Load<AudioClip>("Audio/Soundtrack/Track 1 part 2");
            waterLoop = Resources.Load<AudioClip>("Audio/World/water/water sound good for loop");

            audioS = gameObject.AddComponent<AudioSource>();

            //soundtrack.PlayOneShot(startTrack);
            audioS.clip = loopTrack;
            audioS.loop = true;
            audioS.time = 1;
            audioS.Play();

            waterS = gameObject.AddComponent<AudioSource>();

            waterS.clip = waterLoop;
            waterS.loop = true;
            //waterS.Play();
        }

        void Update() {
            //if (!looping && !soundtrack.isPlaying) {
            //    print("Beginning to loop track");
            //    looping = true;
            //    soundtrack.clip = loopTrack;
            //    soundtrack.loop = true;
            //    soundtrack.Play();
            //}
        }

        void Awake() {

		}
	}
}

