using System;
using System.Runtime.Serialization;

namespace game.world.units {
    [Serializable]
    internal class HexNotPassableError : Exception {
        private Hex h;

        public HexNotPassableError() {
        }
        

        public HexNotPassableError(Hex h) : base("Couldn't move to " + h + ", isn't passable") {
            this.h = h;
        }

        public HexNotPassableError(string message, Exception innerException) : base(message, innerException) {
        }

        protected HexNotPassableError(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}