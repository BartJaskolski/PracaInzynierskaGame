﻿using System;
using GameRunningCube.Source.GameEngine;
using GameRunningCube.Source.GameEngine.Input;
using GameRunningCube.Source.Helpers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace GameRunningCube
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        public GameBoard Board { get; set; }
        public Object2DEngine Object2DEngine { get; set; }
        public GameSettings Settings { get; set; }

        public MainGame()
        {
            SetDefaultVariables();
        }

        public MainGame(GameSettings gameSettings)
        {
            Settings = gameSettings;
            SetDefaultVariables();
        }

        private void SetDefaultVariables()
        {
            GlobalVariables.Graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            GlobalVariables.Random = new Random(10);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            GlobalVariables.Graphics.PreferredBackBufferWidth = GlobalVariables.Graphics.PreferredBackBufferHeight = Settings.PreferredBackBufferWidth;
            GlobalVariables.Graphics.ApplyChanges();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            GlobalVariables.Content = this.Content;
            GlobalVariables.SpriteBatch = new SpriteBatch(GraphicsDevice);
            GlobalVariables.KeyboardController = new KeyboardController();
            GlobalVariables.ObjectGenerator = new ObjectGenerator();

            GlobalVariables.SpriteFont = Content.Load<SpriteFont>("FontArial");

            Board = new GameBoard(Settings);
            Object2DEngine = new Object2DEngine();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad0))
                Board.StopGame = false;
            if (Keyboard.GetState().IsKeyDown(Keys.NumPad1))
                Board.StopGame = true;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            this.TargetElapsedTime = TimeSpan.FromSeconds(1.0f / Settings.SzybkoscGry);
            //TargetElapsedTime ?? change frequency of update 
            if (!Board.StopGame)
            { 
                GlobalVariables.KeyboardController.Update();

                Object2DEngine.Update(Board);
                GlobalVariables.KeyboardController.UpdateOld();

                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            GlobalVariables.SpriteBatch.Begin(SpriteSortMode.Deferred,BlendState.AlphaBlend);
            
                Board.Draw();
            
            GlobalVariables.SpriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
