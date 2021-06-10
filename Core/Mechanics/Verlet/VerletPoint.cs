using Microsoft.Xna.Framework;

namespace TrelamiumTwo.Core.Mechanics.Verlet
{
    public class VerletPoint
    {
        public Vector2 Position;

        public Vector2 OldPosition;

        public Vector2 Velocity;

        public bool Pinned;

        public VerletPoint(Vector2 position) => Position = position;

        public VerletPoint(Vector2 position, Vector2 oldPosition) : this(position) => OldPosition = oldPosition;

        public VerletPoint(Vector2 position, Vector2 oldPosition, bool pinned) : this(position, oldPosition) => Pinned = pinned;

        public VerletPoint(Vector2 position, bool pinned) : this(position) => Pinned = pinned;
    }
}
