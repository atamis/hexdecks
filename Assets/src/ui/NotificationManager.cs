using UnityEngine;

namespace game {
	public class NotificationManager : MonoBehaviour {
		public void AddText(Vector3 loc, string text) {
			var obj = new GameObject("Floating Combat Text [" + text + "]");

			obj.transform.parent = gameObject.transform;
			obj.transform.position = loc;


			var ft = obj.AddComponent<FloatingText>();
			ft.init(text);
		}

		private class FloatingText : MonoBehaviour {
			private string text;
			private TextMesh tm;
			private float start;

			public void init(string text) {
				this.text = text;
				tm = gameObject.AddComponent<TextMesh>();

				var font = Resources.Load<Font>("Fonts/LeagueSpartan-Bold");

				tm.text = text;
				tm.fontSize = 148;
				tm.characterSize = 0.04f;
				tm.color = Color.black;
				tm.font = font;

				tm.GetComponent<Renderer>().material = font.material;

				start = Time.time;
			}

			void Update() {

				transform.position = transform.position + new Vector3(0, 3, 0) * Time.deltaTime;
				transform.position = new Vector3(transform.position.x, transform.position.y, Layer.PseudoUI);

				var c = tm.color;
				c.a -= Time.deltaTime/2;
				tm.color = c;

				if (Time.time - start > 1) {
					Destroy(gameObject);
				}
			}
		}
	}
}