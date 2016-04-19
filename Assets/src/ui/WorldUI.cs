using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using game;
using game.tcg;
using game.tcg.cards;
using game.world;
using game.math;
using game.world.units;

namespace game.ui {
    class WorldUI : MonoBehaviour {
		public static Card selected;
		private CursorHelper ch;
		private Player p;
		private WorldMap w;

		private int invincibleCD;
		private int aoeCD;
		private int twoactionCD;

		private CardManager cm;

		private Hex currHex;
		private Hex lastHex;
		private Unit currUnit;
		private Unit lastUnit;

		public void init(WorldMap w, Player p) {
			this.w = w;
			this.p = p;

			ch = new GameObject ("Cursor Helper").AddComponent<CursorHelper> ();
			ch.init (this);

			cm = new GameObject ("Card Manager").AddComponent<CardManager> ();
			cm.init ();
		}

		void Update() {

			//////////
			/*
			if (Input.GetKeyUp(KeyCode.Alpha1) && invincibleCD == 0) {
				invincibleCD = 5;
				p.nextCommand = new InvincibleCommand(w.hero);
			}

			if (Input.GetKeyUp(KeyCode.Alpha2) && aoeCD == 0) {
				aoeCD = 5;
				p.nextCommand = new AOECommand(w.hero);
			}

			if (Input.GetKeyUp(KeyCode.Alpha3) && twoactionCD == 0) {
				twoactionCD = 5;
				p.nextCommand = new DoubleActionCommand(p);
			}
			*/
			HandleEnemyMouseOver();

		}

		void OnGUI() {
			if (p != null) GUI.Label(new Rect(150, 10, 100, 30), "Health: " + p.hero.health);

			GUI.color = Color.yellow;

			//GUI.Label(new Rect(150, 30, 250, 20), "[1] Invincible" + "(" + invincibleCD + ")");

			//GUI.Label(new Rect(150, 50, 250, 20), "[2] 1 damage to surounding hexes" + "(" + aoeCD + ")");

			//GUI.Label(new Rect(150, 70, 250, 20), "[3] Gain 2 actions" + "(" + twoactionCD + ")");

			if (GUI.Button(new Rect(750, 10, 100, 30), "Reset Level")) {
				SceneManager.LoadSceneAsync("Main");
			}
		}

		public void HandleEnemyMouseOver() {
			lastUnit = currUnit;
			currHex = MathLib.GetHexAtMouse();

			if (currHex != null)
			{
				currUnit = currHex.unit;
			}

			if (currUnit != lastUnit && lastUnit != null)
			{
				if (lastUnit.GetType().IsSubclassOf(typeof(EnemyUnit)))
				{
					lastUnit.mouseExit();
				}
			}

			if (currUnit != null)
			{
				if (currUnit.GetType().IsSubclassOf(typeof(EnemyUnit)))
				{
					currUnit.mouseEnter();
				}
			}
		}

		internal void NextTurn() {
			//invincibleCD = Math.Max(0, invincibleCD - 1);
			//aoeCD = Math.Max(0, aoeCD - 1);
			//twoactionCD = Math.Max(0, twoactionCD - 1);
		}

		private class CursorHelper : MonoBehaviour {
			private enum ActionState {
				Default,
				Selected
			};

			private ActionState state;
			private BoxCollider2D coll;
			private WorldUI ui;
			private List<Hex> cache;

			public void init(WorldUI ui) {
				this.ui = ui;
				this.tag = "Cursor";
				this.name = "CursorHelper";

				coll = gameObject.AddComponent<BoxCollider2D> ();
				coll.size = new Vector3 (0.1f, 0.1f, 0);
				coll.isTrigger = true;
			}

			void Update() {
				Hex h = MathLib.GetHexAtMouse ();

				if (cache != null) {
					foreach (Hex h2 in cache) {
						h2.Highlight (Color.white);
					}
				}

				switch (this.state) {
					case ActionState.Default:
						if (Input.GetMouseButtonDown(0)) {
							if (h != null && ui.p.nextCommand == null) {
								ui.p.nextCommand = new MoveCommand (ui.p.hero, h);
							}
						}
						break;

					case ActionState.Selected:
						if (h != null) {
							//cache = selected.PreCast(h, 1); //
							foreach (Hex h2 in cache) {
								h2.Highlight (Color.red);
							}
						}
						break;
				}
			}

			void SetState(int state_id) {
				this.state = (ActionState)state_id;
			}

			void OnTriggerEnter2D(Collider2D coll) {
				if (coll.gameObject.tag == "Hex" && this.state == ActionState.Selected) {
					coll.gameObject.SendMessage ("SetColor", Color.red);
				}
			}

			void OnTriggerExit2D(Collider2D coll) { 
				if (coll.gameObject.tag == "Hex") {
					coll.gameObject.SendMessage ("SetColor", Color.white);
				}
			}
		}
    }
}
