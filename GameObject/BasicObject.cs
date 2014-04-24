using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Misc;

namespace Pong.GameObject
{
    public class BasicObject
    {
        public virtual RectangleF Size { get { return size; } set { size = value; } }
        private RectangleF size;
        protected Color color;

        public float X { get { return size.X; } set { size.X = value; } }
        public virtual float  Y { get { return size.Y; } set { size.Y = value; } }
        public float Top { get { return size.Top; } }
        public float Bottom { get { return size.Bottom; } }
        public float Right { get { return size.Right; } }
        public float Left { get { return size.Left; } }

        public float Width { get { return size.Width; } }
        public float Height { get { return size.Height; } }

        public Vector2 Position
        {
            get
            {
                return new Vector2(X, Y);
            }
            protected set
            {
                X = value.X;
                Y = value.Y;
            }
        }

        public Vector2 Center { get { return new Vector2(X + Width / 2, Y + Height / 2); } }

        public BasicObject(Color color, RectangleF size)
        {
            this.color = color;
            this.size = size;
        }
        public virtual void Draw(SpriteBatch sb, GameTime gameTime)
        {
            sb.FillRectangle(size, color);
        }

    }

}
