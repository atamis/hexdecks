using UnityEngine;
using System.Collections.Generic;
using game.ui;

namespace game {
    class GameManager : MonoBehaviour {
        private WorldUI ui;

        void Awake() {
            ui = new GameObject("UI").AddComponent<WorldUI>();
        }

        void Update() {

        }
    }
}
