using UnityEngine;
using System.Collections;

namespace game.ui {
	enum GUIType {
		Intro,
		World,
		Library,
	}

	class GUIBase : MonoBehaviour {
		public virtual void Delete() {}
	}
}
