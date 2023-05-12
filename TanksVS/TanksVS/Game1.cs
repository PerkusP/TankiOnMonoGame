using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended.Tiled;
using Project2;
using System;
using System.Collections.Generic;
using System.Linq;
using TiledSharp;

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

        public static SpriteFont SpriteFont;

        private Player _player1;
        private Player _player2;
        public static Player[] Players { get; private set; }
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public static List<Rectangle> colliders;

        public static MapManager MapManager;

        public static Walls Walls;
        public static int Width { get; private set; }
        public static int Height { get; private set; }

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
            Sprite.Texture = Content.Load<Texture2D>("vistrel");
            Walls.Texture = Content.Load<Texture2D>("Wall");
            _player1 = new Player(new Vector2(120,140),
                new Vector2(150,150), 
                0.5f, 
                Content.Load<Texture2D>("Tank1S"), 1);
            _player2 = new Player(new Vector2(1380,640),
                new Vector2(150,150),
                MathHelper.Pi / 2,
                Content.Load<Texture2D>("Tank2S"), 2);

            //Walls = new Walls();
            //Walls.CreateWalls();

            SpriteFont = Content.Load<SpriteFont>("Arial");


            _player1.ControlDictionary = _controlDictionary;
            _player2.ControlDictionary = _controlDictionary2;
            Players = new Player[2] { _player1, _player2 };

            GameDraw.Graphics = GameUpdate.Graphics = _graphics;
            base.Initialize();
        }

        protected override void LoadContent()
        {

            _spriteBatch = new SpriteBatch(GraphicsDevice);
            MapManager.SpriteBatch = GameDraw.SpriteBatch = GameUpdate.SpriteBatch = _spriteBatch;

            var _map = new TmxMap("Content/MapTanks.tmx");
            var tileSet = Content.Load<Texture2D>("GrassPlusWallsTiles");
            var tileWidth = _map.Tilesets[0].TileWidth;
            var tileHeight = _map.Tilesets[0].TileHeight;
            var tilesetTilesSize = tileSet.Width / tileWidth;
            
            MapManager = new MapManager(_map, tileSet, tilesetTilesSize, tileWidth, tileHeight);
            colliders = new List<Rectangle>();
            foreach (var o in _map.ObjectGroups["Collision"].Objects)
            {
                colliders.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }
        }

        protected override void Update(GameTime gameTime)
        {
            var keys = Keyboard.GetState().GetPressedKeys();
            if (keys.Any(x => x == Keys.Escape))
                Exit();
            
            GameUpdate.Update(gameTime, keys);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GameDraw.Draw();
           
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