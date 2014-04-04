using System;
using Microsoft.Xna.Framework;
using Pong.Core;
using Pong.Misc;

namespace Pong.GameObject
{
    public class Ball : PhysicalObject
    {
        public Ball(Color color, RectangleF size, Vector2 speed) : base(color, size, speed)
        {
            Spawn();
        }

        public void Spawn()
        {
            Position = new Vector2(GameCore.Window.ClientBounds.Width / 2, GameCore.Window.ClientBounds.Height / 2);
            speed = new Vector2(new Random().Next(-4, 4), new Random().Next(-3, 3));
        }
        public void UpdateDirection(Side side)
        {
            switch (side)
            {
                case Side.Left:
                case Side.Right:
                    speed.X *= -1.05F;
                    break;
                case Side.Up:
                case Side.Down:
                    speed.Y *= -1.05F;
                    break;
            }
        }
    }

}