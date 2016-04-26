using UnityEngine;
using System.Collections;

namespace game {
	class AudioManager : MonoBehaviour {

        private AudioSource soundtrack;
        private AudioClip track;

        void Start()
        {
            soundtrack = gameObject.AddComponent<AudioSource>();
            track = Resources.Load<AudioClip>("Audio/Soundtrack/Track 1 Video game 2 Draft");
            soundtrack.PlayOneShot(track);
            soundtrack.loop = true;
        }

        void Awake() {

		}
	}
}

