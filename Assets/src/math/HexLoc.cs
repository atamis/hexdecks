using System;

namespace game.math {
	struct HexLoc {
		public static HexLoc[] hex_directions = {
			new HexLoc(1, 0, -1), new HexLoc(1, -1, 0), new HexLoc(0, -1, 1),
			new HexLoc(-1, 0, 1), new HexLoc(-1, 1, 0), new HexLoc(0, 1, -1)
		};

		public static HexLoc[] hex_diagonals = {
			new HexLoc(2, -1, -1), new HexLoc(1, 1, -2), new HexLoc(-1, 2, -1),
			new HexLoc(-2, 1, 1), new HexLoc(-1, -1, 2), new HexLoc(1, -2, 1)
		};

		public int q, r, s;

		public HexLoc(int q, int r, int s) {
			this.q = q;
			this.r = r;
			this.s = s;

			if (q + r + s != 0) {
				throw new Exception("Invalid Hex Coordinates");
			}
		}

		public HexLoc(int x, int y) {
			this.q = x - (y - (y & 1)) / 2;
			this.r = y;
			this.s = -q - r;

			if (q + r + s != 0) {
				throw new Exception("Invalid Hex Coordinates");
			}
		}

		public override bool Equals(Object o) {
			if (o.GetType() != typeof(HexLoc)) {
				return false;
			}
			HexLoc h = (HexLoc) o;
			return this == h;
		}

        private int normalizeInt(int i) {
            if (i > 0) {
                return 1;
            }

            if (i < 0) {
                return -1;
            }
            
            return 0;

        }

        public HexLoc Normalize() {
            // This probably works.

            int qp = normalizeInt(q);
            int rp = normalizeInt(r);

            return new HexLoc(qp, rp, -(qp + rp));
        }

		public override int GetHashCode() {
			return q ^ r ^ s;
		}

		public static bool operator ==(HexLoc a, HexLoc b) {
			return a.q == b.q && a.r == b.r && a.s == b.s;
		}

		public static bool operator !=(HexLoc a, HexLoc b) {
			return !(a == b);
		}

		public static HexLoc operator +(HexLoc a, HexLoc b) {
			return new HexLoc(a.q + b.q, a.r + b.r, a.s + b.s);
		}

		public static HexLoc operator -(HexLoc a, HexLoc b) {
			return new HexLoc(a.q - b.q, a.r - b.r, a.s - b.s);
		}

		public int Length() {
			return (int) (Math.Abs(q) + Math.Abs(r) + Math.Abs(s)) / 2;
		}

		public int Distance(HexLoc b) {
			return (this - b).Length();
		}

		public static HexLoc Direction(int direction) {
			return hex_directions[direction % 6];
		}

		public HexLoc Neighbor(int direction) {
			return this + Direction(direction);
		}

		public HexLoc Diagonal(int direction) {
			return this + hex_diagonals [direction % 6];
		}

		public HexLoc Rotate60() {
			return new HexLoc (-s, -q, -r);
		}

		public HexLoc Rotate(int steps) {
			HexLoc l = this;
			for (int i = 0; i < steps; i++) {
				l = l.Rotate60 ();
			}
			return l;
		}

		public override String ToString() {
			return String.Format("HexLoc {0}, {1}, {2}", q, r, s);
		}
	}
}

