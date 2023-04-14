using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TanksVS
{
    public class Game1 : Game
    {
        private Dictionary<Keys, Actions> _controlDictionary = new Dictionary<Keys, Actions>()
        {
            { Keys.Left, Actions.Left},
            { Keys.Right, Actions.Right },
            { Keys.Up, Actions.Forward },
            { Keys.Down, Actions.Backward },
            { Keys.A, Actions.Left },
            { Keys.D, Actions.Right },
            { Keys.W, Actions.Forward },
            { Keys.S, Actions.Backward },
            { Keys.None, Actions.None }
        };
        private Player _player1;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            ChangeResolution(1600, 800);
            //_graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
            
        }

        protected override void Initialize()
        {

            _player1 = new Player(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), 
                0.5f, 
                Content.Load<Texture2D>("tank1"));

            Player.ControlDictionary = _controlDictionary;
            Player.Graphics = _graphics;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Player.SpriteBatch = _spriteBatch;
        }

        protected override void Update(GameTime gameTime)
        {
            var keyState = Keyboard.GetState();
            var b = Keyboard.GetState().GetPressedKeys();
            if (keyState.IsKeyDown(Keys.Escape))
                Exit();
            

            _player1.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();

            _player1.Draw();
           
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void ChangeResolution(int width, int height)
        {
            _graphics.PreferredBackBufferHeight = height;
            _graphics.PreferredBackBufferWidth = width;
            _graphics.ApplyChanges();
        }
    }
}