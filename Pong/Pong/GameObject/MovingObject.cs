using Microsoft.Xna.Framework;
using Pong.Misc;

namespace Pong.GameObject
{
    public abstract class MovingObject : BasicObject
    {
        public Vector2 speed;

        protected MovingObject(Color color, RectangleF size) : base(color, size)
        {
            speed = new Vector2(0, 0);
        }
        protected MovingObject(Color color, RectangleF size, Vector2 speed)
            : base(color, size)
        {
            this.speed = speed;
        }

        public virtual void Update()
        {
            Position += speed;
        }
    }
}