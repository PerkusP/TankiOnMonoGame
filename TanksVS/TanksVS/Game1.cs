using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using TanksVS.States;
using TiledSharp;

namespace TanksVS.Scripts
{
    public class Game1 : Game
    {
        private State _currentState;
        private State _nextState;

        public static SpriteFont SpriteFont;

        public Player[] Players;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public List<Rectangle> Colliders;
        public List<Rectangle> Slow;

        public MapManager MapManager;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            ChangeResolution(1600, 800);
            //_graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            
            Width = _graphics.PreferredBackBufferWidth;
            Height = _graphics.PreferredBackBufferHeight;

        }

        protected override void Initialize()
        {
            IsMouseVisible = true;

            Bullet.Texture = Content.Load<Texture2D>("Fire");

            SpriteFont = Content.Load<SpriteFont>("Pixel");
            
            
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MapManager.SpriteBatch = _spriteBatch;

            _currentState = new MainMenuState(this, _graphics.GraphicsDevice, Content);
        }

        
        protected override void Update(GameTime gameTime)
        {
            if (_nextState != null)
            {
                _currentState = _nextState;
                _nextState = null;
            }
            _currentState.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            
            _currentState.Draw(gameTime, _spriteBatch);
           
            base.Draw(gameTime);
        }

        private void ChangeResolution(int width, int height)
        {
            _graphics.PreferredBackBufferHeight = height;
            _graphics.PreferredBackBufferWidth = width;
            _graphics.ApplyChanges();
        }

        public void ChangeState(State state)
        {
            _nextState = state;
        }
    }
}