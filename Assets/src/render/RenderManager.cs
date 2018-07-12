using UnityEngine;
using System.Collections;

/*
 * handles particle effects
 * 
 * 
 */
namespace game.render {
	class RenderManager : MonoBehaviour {
		public static RenderManager instance;
        public ParticleSystem dust;
        public ParticleSystem bubbles;
        public ParticleSystem smoke;

        private void loadResources() {
            dust = Resources.Load<ParticleSystem>("Particle/DustParticle");
            bubbles = Resources.Load<ParticleSystem>("Particle/BubbleParticle");
            smoke = Resources.Load<ParticleSystem>("Particle/SmokeParticle");
        }

		void Awake() {
            loadResources();
			if (instance != null) {
				
			}
			instance = this;
		}

		private ParticleSystem MakeParticleSystem(ParticleSystem prefab, Vector3 pos) {
			ParticleSystem ps = Instantiate (prefab, pos, Quaternion.identity) as ParticleSystem;
			Destroy (ps.gameObject, ps.startLifetime);
			return ps;
		}

		public void DustCloud(Vector3 pos) {
			MakeParticleSystem (dust, pos);
		}

		public void Explosion() {
			
			//ParticleSystem ps = Instantiate
		}

		public void Bubbles() {

		}
	}
} 

