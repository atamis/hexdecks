using UnityEngine;
using System.Collections;

namespace game {
	class AudioManager : MonoBehaviour {

        private AudioSource soundtrack;
        private AudioClip startTrack;
        private AudioClip loopTrack;
        //private bool looping = false;

        void Start()
        {
            startTrack = Resources.Load<AudioClip>("Audio/Soundtrack/Track 1 part 1");
            loopTrack = Resources.Load<AudioClip>("Audio/Soundtrack/Track 1 part 2");

            soundtrack = gameObject.AddComponent<AudioSource>();

            //soundtrack.PlayOneShot(startTrack);
            soundtrack.clip = loopTrack;
            soundtrack.loop = true;
            soundtrack.time = 1;
            soundtrack.Play();
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

