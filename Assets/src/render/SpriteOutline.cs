using UnityEngine;

namespace game.render {
	[ExecuteInEditMode]
	public class SpriteOutline : MonoBehaviour {
		private SpriteRenderer sr;
		public Color col = Color.red;

		void OnEnable() {
			sr = GetComponent<SpriteRenderer>();
			UpdateOutline (true);
		}

		void OnDisable() {
			UpdateOutline (false);
		}

		void Update() {
			UpdateOutline (true);
		}

		void UpdateOutline(bool show) {
			MaterialPropertyBlock mpb = new MaterialPropertyBlock ();
			sr.GetPropertyBlock (mpb);
			mpb.SetFloat ("_Outline", show ? 1f : 0);
			mpb.SetColor ("_OutlineColor", col);
			sr.SetPropertyBlock (mpb);
		}
	}
}


