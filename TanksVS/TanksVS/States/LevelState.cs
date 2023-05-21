using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

using TanksVS.Scripts;
using TiledSharp;

namespace TanksVS.States
{
    public class LevelState : State
    {
        private readonly List<Button> _buttons;

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

        public LevelState(Game1 game, GraphicsDevice graphics, ContentManager content) : base(game, graphics, content)
        {
            var t_back = content.Load<Texture2D>("ExitLevel");
            var t_level1 = content.Load<Texture2D>("Level1");
            var t_level2 = content.Load<Texture2D>("Level2");
            var t_level3 = content.Load<Texture2D>("Level3");

            var b_back = new Button(t_back, new Vector2(_game.Width / 2 - t_level2.Width / 2, _game.Height / 2 + 100));
            var b_level1 = new Button(t_level1, new Vector2(_game.Width / 2 - t_level1.Width / 2 - 200, _game.Height / 2));
            var b_level2 = new Button(t_level2, new Vector2(_game.Width / 2 - t_level2.Width / 2, _game.Height / 2));
            var b_level3 = new Button(t_level3, new Vector2(_game.Width / 2 - t_level3.Width / 2 + 200, _game.Height / 2));

            b_back.Click += Back;
            b_level1.Click += Level_One;
            b_level2.Click += Level_Two;
            b_level3.Click += Level_Three;


            _buttons = new List<Button>
            {
                b_back,
                b_level1,
                b_level2,
                b_level3
            };
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(_content.Load<Texture2D>("BackGround"), new Rectangle(0, 0, 1600, 800), Color.White);
            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var button in _buttons)
                button.Update(gameTime);
        }

        private void Back(object sender, EventArgs e) => _game.ChangeState(new MainMenuState(_game, _graphics, _content));

        private void Level_Three(object sender, EventArgs e)
        {
            Console.WriteLine("3");
        }

        private void Level_Two(object sender, EventArgs e)
        {
            _game.Players = new Player[2]
            {
                Player.Init(new Vector2(120, 140), 0.5f, "TankOne", 1, _controlDictionary, _content),
                Player.Init(new Vector2(1380, 700), MathHelper.Pi / 2, "TankTwo", 2, _controlDictionary2, _content)
            };

            _game.MapManager = MapManager.Init("Content/Map2.tmx", "DesertTiles", _content);
            _game.Colliders = new List<Rectangle>();
            _game.Slow = new List<Rectangle>();

            Bullet.Speed = 5;
            foreach (var item in _game.MapManager.Map.ObjectGroups["Collision"].Objects)
            {
                _game.Colliders.Add(new Rectangle((int)item.X + 15, (int)item.Y, (int)item.Width + 15, (int)item.Height));
            }

            
            foreach (var item in _game.MapManager.Map.ObjectGroups["Slow"].Objects)
            {
                _game.Slow.Add(new Rectangle((int)item.X + 15, (int)item.Y, (int)item.Width + 15, (int)item.Height));
            }
            
            _game.ChangeState(new GameState(_game, _graphics, _content));
        }

        private void Level_One(object sender, EventArgs e)
        {
            _game.Players = new Player[2]
            {
                Player.Init(new Vector2(120, 140), 0.5f, "TankOne", 1, _controlDictionary, _content),
                Player.Init(new Vector2(1380, 640), MathHelper.Pi / 2, "TankTwo", 2, _controlDictionary2, _content)
            };
            Bullet.Speed = 15;
            _game.MapManager = MapManager.Init("Content/MapTanks.tmx", "GrassPlusWallsTiles", _content);
            _game.Colliders = new List<Rectangle>();
            _game.Slow = new List<Rectangle>();
            foreach (var item in _game.MapManager.Map.ObjectGroups["Collision"].Objects)
            {
                _game.Colliders.Add(new Rectangle((int)item.X + 15, (int)item.Y, (int)item.Width + 15, (int)item.Height));
            }

            _game.ChangeState(new GameState(_game, _graphics, _content));
        }
    }
}
