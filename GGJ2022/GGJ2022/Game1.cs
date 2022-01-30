using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.ViewportAdapters;
using System.Collections.Generic;
using tainicom.Aether.Physics2D.Dynamics;

namespace GGJ2022
{
    public class Game1 : Game
    {
        //Setup variables
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;
        private CameraHandler cam;

        //Image Variables (Testing)

        //Physics "stuff"
        public World world = new World(new tainicom.Aether.Physics2D.Common.Vector2(0, 200));

        //Fields
        public SpriteFont font;

        //Constructor
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            _graphics.PreferredBackBufferWidth = 960;
            _graphics.PreferredBackBufferHeight = 540;
            _graphics.ApplyChanges();
        }

        //Initialize all variables
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            cam = new CameraHandler(Window, GraphicsDevice);            
        }

        //Load graphics
        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            font = Content.Load<SpriteFont>("BitScript");

            GameStateManager.GiveItems(world, _spriteBatch, this);
            GameStateManager.Begin();
        }

        /// <summary>
        /// Update loop for game variables
        /// </summary>
        /// <param name="gameTime">Time passed</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            world.Step(1 / 60f);

            GameStateManager.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            base.Update(gameTime);
        }

        /// <summary>
        /// Draw loop for all visuals
        /// </summary>
        /// <param name="gameTime">Time passed</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // Drawing using cam's matrix
            _spriteBatch.Begin(transformMatrix: cam.GetCameraViewMatrix());
            GameStateManager.Draw();
            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
