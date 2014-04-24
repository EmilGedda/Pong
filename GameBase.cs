using System;
using System.ComponentModel;
using System.Security.Principal;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Pong.Core;

namespace Pong
{
    public class GameBase : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public GameBase()
        {
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = true,
                PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height,
                PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width,
                PreferMultiSampling = true,
                
            };
            TargetElapsedTime = TimeSpan.FromSeconds(1.0f/100.0f);
            Content.RootDirectory = "Content";
        }

        protected override void Initialize()
        {
            GameCore.Initialize(graphics, Window);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            GameCore.LoadContent(GraphicsDevice);
            spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        protected override void UnloadContent()
        {
        }

        protected override void Update(GameTime gameTime)
        {
            switch (GameCore.CurrentState)
            {
                case GameCore.State.Play:
                    GameCore.CurrentState = GameCore.UpdateGame(gameTime, Window);
                    break;
                case GameCore.State.Multiplayer:
                case GameCore.State.Options:
                case GameCore.State.Pause:
                case GameCore.State.MainMenu:
                    GameCore.CurrentState = GameCore.UpdateMenu(gameTime, Window);
                    break;
                case GameCore.State.Quit:
                    Exit();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("State");
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            spriteBatch.Begin();

            switch (GameCore.CurrentState)
            {
                case GameCore.State.Play:
                    GameCore.DrawGame(gameTime, spriteBatch, Window);
                    break;
                case GameCore.State.Multiplayer:
                case GameCore.State.Options:
                case GameCore.State.Pause:
                case GameCore.State.MainMenu:
                    GameCore.DrawMenu(gameTime, spriteBatch);
                    break;
                case GameCore.State.Quit:
                    Exit();
                    break;
                default:
                    throw new InvalidEnumArgumentException("State");

            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
