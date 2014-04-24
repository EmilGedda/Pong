using System;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.GameObject;
using Pong.Misc;

namespace Pong.Core
{
    static class GameCore
    {
        public enum State
        {
            Play,
            MainMenu,
            Pause,
            Options,
            Multiplayer,
            Quit
        };

        public const int WallVisibleWidth = 10;
        public const int WallHiddenWidth = 500;
        public const int WallHeight = 200;

        private static PhysicalObject[] Walls;
        public static Random Rng = new Random();
        public static Rectangle InnerRect;
        private static Player human;
        private static Player computer;
        private static Ball ball;
        public static GameWindow Window;

        public static State CurrentState = State.Play;

        public static void Initialize(GraphicsDeviceManager graphics, GameWindow window)
        {
            Window = window;
            Walls = new PhysicalObject[6]
            {
                new PhysicalObject(Color.White, new RectangleF(0, WallVisibleWidth - WallHiddenWidth, window.ClientBounds.Width, WallHiddenWidth)), //Top
                new PhysicalObject(Color.White, new RectangleF(0, window.ClientBounds.Height - WallVisibleWidth, window.ClientBounds.Width, WallHiddenWidth)), //Bottom
                new PhysicalObject(Color.White, new RectangleF(WallVisibleWidth - WallHiddenWidth, 0, WallHiddenWidth, WallHeight)), //Left Top
                new PhysicalObject(Color.White, new RectangleF(WallVisibleWidth - WallHiddenWidth, window.ClientBounds.Height - WallHeight, WallHiddenWidth, WallHeight)), //Left Bottom
                new PhysicalObject(Color.White, new RectangleF(window.ClientBounds.Width - WallVisibleWidth, 0, WallHiddenWidth, WallHeight)), //Right Top
                new PhysicalObject(Color.White, new RectangleF(window.ClientBounds.Width - WallVisibleWidth, window.ClientBounds.Height - WallHeight, WallHiddenWidth, WallHeight)) //Right Bottom
            };
            InnerRect = new Rectangle(WallVisibleWidth, WallVisibleWidth, window.ClientBounds.Width - 2 * WallVisibleWidth, window.ClientBounds.Height - WallVisibleWidth);
            human = new Player(Color.White, new RectangleF(100, 100, 20, 20), 10);
            computer = new Player(Color.White, new RectangleF(window.ClientBounds.Width - 100, 100, 20, 20), 10);
            ball = new Ball(Color.White, new RectangleF(10, 30, 15, 15), new Vector2(5F,5F));
        }
        
        public static State UpdateGame(GameTime gameTime, GameWindow Window)
        {
            human.Update();
            float currentY = MathHelper.Clamp(ball.Center.Y - computer.Center.Y, -4, 4);
            if (ball.X > -20 && ball.X < Window.ClientBounds.Width + 20)
            {
                computer.Y += currentY;
                foreach (Segment segment in computer.Segments)
                    segment.Y += currentY;
            }
            else
            {
                ball.Spawn();
            }
            ball.Update();
            if (ball.IsColliding(human))
                ball.UpdateDirection(ball.CollidingSide(human));
            if (ball.IsColliding(computer))
                ball.UpdateDirection(ball.CollidingSide(computer));
            foreach (PhysicalObject wall in Walls.Where(wall => ball.IsColliding(wall)))
                ball.UpdateDirection(ball.CollidingSide(wall));
            return CurrentState;
        }

        public static State UpdateMenu(GameTime gameTime, GameWindow Window)
        {
            return CurrentState;
        }

        public static void DrawGame(GameTime gameTime, SpriteBatch spriteBatch, GameWindow window)
        {
            human.Draw(spriteBatch, gameTime);
            computer.Draw(spriteBatch, gameTime);
            ball.Draw(spriteBatch,gameTime);
            foreach (PhysicalObject wall in Walls)
                wall.Draw(spriteBatch, gameTime);
            
        }

        public static void DrawMenu(GameTime gameTime, SpriteBatch spriteBatch)
        {

        }

        public static void LoadContent(GraphicsDevice graphicsDevice)
        {
        }
    }
}
