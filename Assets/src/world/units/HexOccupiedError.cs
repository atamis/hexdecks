using System;
using System.Runtime.Serialization;

namespace game.world.units {
    [Serializable]
    internal class HexOccupiedError : Exception {
        private Hex h;

        public HexOccupiedError() {
        }
        

        public HexOccupiedError(Hex h) : base("Couldn't move to " + h + ", unit already there") {
            this.h = h;
        }

        public HexOccupiedError(string message, Exception innerException) : base(message, innerException) {
        }

        protected HexOccupiedError(SerializationInfo info, StreamingContext context) : base(info, context) {
        }
    }
}