using game.tcg;
using game.world;
using game.world.math;
using game.world.units;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace game.ui {
	class UIManager : MonoBehaviour {
		GameObject uiFolder;

		void Awake() {
			uiFolder = new GameObject ("UI Folder");

			/*
			// back panel
			var obj = GameObject.CreatePrimitive (PrimitiveType.Quad);
			obj.transform.parent = uiFolder.transform;

			Material mat = obj.GetComponent<Renderer>().material;
			mat.shader = Shader.Find ("Sprites/Default");
			mat.mainTexture = Resources.Load<Texture2D> ("Sprites/Square");
			*/

		}

		public Hex GetHexAtMouse() {
			Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			HexLoc l = GameManager.l.PixelHex(worldPos);
			if (GameManager.world.map.ContainsKey(l)) {
				Hex h = GameManager.world.map[l];
				return h;
			}
			return null;
		}

		void Update() {
			// Update card positions

			if (Input.GetMouseButtonUp(0)) {
				//Hex h = GetHexAtMouse();

				//GameManager.p.nextCommand = new MoveCommand(p.hero, h);
			}
		}
	}
}
