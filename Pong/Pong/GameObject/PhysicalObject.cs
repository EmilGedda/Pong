using System;
using System.CodeDom;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Pong.Misc;

namespace Pong.GameObject
{
    public class PhysicalObject : MovingObject
    {
        protected Vector2 spawnpoint;
        public enum Side
        {
            Left,
            Right,
            Up,
            Down
        }
        public bool IsAlive { get; set; }

        public PhysicalObject(Color color, RectangleF size)
            : base(color, size)
        {
        }

        public PhysicalObject(Color color, RectangleF size, Vector2 speed)
            : base(color, size, speed)
        {

        }

        public virtual bool IsColliding(PhysicalObject other)
        {
            return Size.Intersects(other.Size);
        }

        public Side CollidingSide(PhysicalObject other)
        {
            float distanceTop = Math.Abs(Y - other.Bottom);
            float distanceBottom = Math.Abs(Bottom - other.Y);
            float distanceLeft = Math.Abs(other.Right - X);
            float distanceRight = Math.Abs(other.Size.Left - Size.Right);
            float min = Math.Min(Math.Min(distanceTop, distanceBottom), Math.Min(distanceLeft, distanceRight));

            if (min == distanceRight)
                return Side.Right;
            if (min == distanceLeft)
                return Side.Left;
            if (min == distanceTop)
                return Side.Up;
            if (min == distanceBottom)
                return Side.Down;

            throw new InvalidEnumArgumentException("Side");
        }
    }
}