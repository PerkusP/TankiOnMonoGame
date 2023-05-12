using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TanksVS
{
    public class Game1 : Game
    {
        private readonly Dictionary<Keys, Actions> _controlDictionary = new()
        {
            { Keys.A, Actions.Left },
            { Keys.D, Actions.Right },
            { Keys.W, Actions.Forward },
            { Keys.S, Actions.Backward },
            { Keys.Q, Actions.Fire },
        };

        private readonly Dictionary<Keys, Actions> _controlDictionary2 = new()
        {
            { Keys.Left, Actions.Left},
            { Keys.Right, Actions.Right },
            { Keys.Up, Actions.Forward },
            { Keys.Down, Actions.Backward },
            { Keys.M, Actions.Fire },
        };
        private Player _player1;
        private Player _player2;
        private Player[] _players;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public static int Width;
        public static int Height;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            ChangeResolution(1600, 800);
            //_graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            Width = _graphics.PreferredBackBufferWidth;
            Height = _graphics.PreferredBackBufferHeight;

        }

        protected override void Initialize()
        {
            Bullet.Texture = Content.Load<Texture2D>("vistrel");
            _player1 = new Player(new Vector2(_graphics.PreferredBackBufferWidth / 2, _graphics.PreferredBackBufferHeight / 2), 
                0.5f, 
                Content.Load<Texture2D>("tank1"));
            _player2 = new Player(new Vector2(_graphics.PreferredBackBufferWidth / 2 + 50, _graphics.PreferredBackBufferHeight / 2 + 50),
                0.5f,
                Content.Load<Texture2D>("tank2"));
            

            _player1.ControlDictionary = _controlDictionary;
            _player2.ControlDictionary = _controlDictionary2;
            _players = new Player[2] {_player1, _player2 };

            GameDraw.Graphics = GameUpdate.Graphics = _graphics;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            GameDraw.SpriteBatch = GameUpdate.SpriteBatch = _spriteBatch;
        }

        protected override void Update(GameTime gameTime)
        {
            var keys = Keyboard.GetState().GetPressedKeys();
            if (keys.Any(x => x == Keys.Escape))
                Exit();
            GameUpdate.Update(gameTime, _players, keys);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {

            GameDraw.Draw(_players);
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