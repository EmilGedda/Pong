using C3.XNA;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Pong.Core;
using Pong.Misc;

namespace Pong.GameObject
{
    public class Player : PhysicalObject
    {
        public int Score { get; set; }
        public Segment[] Segments { get; set; }
        private float lastY;
        public Player(Color color, RectangleF segmentSize, int segments) : base(color, new RectangleF(segmentSize.X, segmentSize.Y, segmentSize.Width,  segmentSize.Height * segments))
        {
            Segments = new Segment[segments];
            for (int i = 0; i < Segments.Length; i++)
            {
                RectangleF tmp = segmentSize;
                tmp.Y = segmentSize.Y + segmentSize.Height * i;
                Segments[i] = new Segment(Color.White, tmp);
            }
            Score = 0;
        }

        public override void Update()
        {
            float lastY = Y;
            Y = MathHelper.Clamp(Mouse.GetState().Y - Height / 2, GameCore.InnerRect.Y, GameCore.InnerRect.Height - Size.Height);
            float currentY = Y - lastY;
            foreach (Segment segment in Segments)
                segment.Y += currentY;

        }

        public override void Draw(SpriteBatch sb, GameTime gameTime)
        {
            foreach (Segment segment in Segments)
                segment.Draw(sb, gameTime);
        }
    }
}