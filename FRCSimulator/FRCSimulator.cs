/****************************** Header ******************************\
Class Name:
Summary:
Project:     FRC2017
Copyright (c) BroncBotz.
All rights reserved.

Author(s): Ryan Cooper
Email: cooper.ryan@centaurisoft.org
\********************************************************************/

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FRCSimulator
{
    /// <summary>
    ///     Simulator for testing
    /// </summary>
    public class FrcSimulator : Game
    {
        #region Public Constructors

        /// <summary>
        ///     Default constructor
        /// </summary>
        public FrcSimulator()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        #endregion Public Constructors

        #region Private Fields

        private GraphicsDeviceManager graphics;

        private SpriteBatch spriteBatch;

        #endregion Private Fields

        #region Protected Methods

        /// <summary>
        ///     This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            base.Draw(gameTime);
        }

        /// <summary>
        ///     LoadContent will be called once per game and is the place to load all of your content.
        /// </summary>
        protected override void LoadContent() => spriteBatch = new SpriteBatch(GraphicsDevice);

        /// <summary>
        ///     UnloadContent will be called once per game and is the place to unload game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
        }

        /// <summary>
        ///     Allows the game to run logic such as updating the world, checking for collisions,
        ///     gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if ((GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed) ||
                Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            base.Update(gameTime);
        }

        #endregion Protected Methods
    }
}