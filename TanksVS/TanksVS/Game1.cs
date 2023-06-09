﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TanksVS.Scripts;
using TanksVS.States;

namespace TanksVS
{
    public class Game1 : Game
    {
        public SpriteFont SpriteFont;
        public Player[] Players;
        public List<Rectangle> Collision;
        public List<Rectangle> SlowCollision;
        public List<Rectangle> DieCollision;
        public MapManager MapManager;
        private readonly GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private State _currentState;
        private State _nextState;
        public int Width { get; }
        public int Height { get; }

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
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            Bullet.Texture = Content.Load<Texture2D>("Fire");
            SpriteFont = Content.Load<SpriteFont>("Pixel");
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
            _currentState.Draw(_spriteBatch);
            base.Draw(gameTime);
        }

        public void ChangeState(State state) => _nextState = state;

        private void ChangeResolution(int width, int height)
        {
            _graphics.PreferredBackBufferHeight = height;
            _graphics.PreferredBackBufferWidth = width;
            _graphics.ApplyChanges();
        }
    }
}