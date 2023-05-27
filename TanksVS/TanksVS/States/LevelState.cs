using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TanksVS.Scripts;

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

            b_back.Click += BackToMain;
            b_level1.Click += LoadLevelOne;
            b_level2.Click += LoadLevelTwo;
            b_level3.Click += LoadLevelThree;


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
            var instuctions1 = _content.Load<Texture2D>("Instructions1");
            var instuctions2 = _content.Load<Texture2D>("Instructions2");

            spriteBatch.Draw(instuctions1, new Vector2(_game.Width / 2 - instuctions1.Width - 150, 250), Color.White);
            spriteBatch.Draw(instuctions2, new Vector2(_game.Width / 2 + instuctions2.Width, 250), Color.White);

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

        private void BackToMain(object sender, EventArgs e) => _game.ChangeState(new MainMenuState(_game, _graphics, _content));

        private void LoadLevelOne(object sender, EventArgs e)
        {
            LevelInit("MapTanks", "GrassPlusWallsTiles",
                new Vector2(120, 140), 0.5f,
                new Vector2(1380, 640), -2f,
                15);
        }
        
        private void LoadLevelTwo(object sender, EventArgs e)
        {
            LevelInit("Map2", "DesertTiles", 
                new Vector2(120, 140), 0.5f,
                new Vector2(1380, 740), 3.14f, 
                5);

        }

        private void LoadLevelThree(object sender, EventArgs e)
        {
            LevelInit("Map3", "CityTiles",
                new Vector2(120, 140), 0.5f,
                new Vector2(1500, 640), -1.57f,
                7);
        }

        private void LevelInit(string map, string tiles, 
            Vector2 firstPlayerPosition, float firstPlayerRotation, 
            Vector2 secondPlayerPosition, float secondPlayerRotation,
            int bulletSpeed)
        {
            _game.Players = new Player[2]
            {
                Player.Init(firstPlayerPosition, firstPlayerRotation, "TankOne", 1, _controlDictionary, _content),
                Player.Init(secondPlayerPosition, secondPlayerRotation, "TankTwo", 2, _controlDictionary2, _content)
            };
            Bullet.Speed = bulletSpeed;
            _game.MapManager = MapManager.Init($"Content/{map}.tmx", tiles, _content, _game);
            _game.ChangeState(new GameState(_game, _graphics, _content));
        }
    }
}
