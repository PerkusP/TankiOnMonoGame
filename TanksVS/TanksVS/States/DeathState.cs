﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TanksVS.Scripts;

namespace TanksVS.States
{
    public class DeathState : State
    {
        private List<Button> _buttons;
        private int _whoWin;
        public DeathState(Game1 game, GraphicsDevice graphics, ContentManager content) : base(game, graphics, content)
        {
            var t_return = content.Load<Texture2D>("gButtonExit");
            var b_return = new Button(t_return, new Vector2(_game.Width / 2 - t_return.Width/2, _game.Height / 2 + 100));
            _whoWin = _game.Players.Where(x => x.Points.Count < 10).First().ID;
            Player.Bullets.Clear();
            
            b_return.Click += ReturnToMain;
            _buttons = new List<Button>()
            {
                b_return
            };
        }

        private void ReturnToMain(object sender, EventArgs e)
        {
            _game.ChangeState(new MainMenuState(_game, _graphics, _content));
        }

        public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            GameDraw.Draw(_graphics, spriteBatch, _game);
            spriteBatch.Begin();
            spriteBatch.DrawString(Game1.SpriteFont, $"Player    {_whoWin}    wins!", new Vector2(_game.Width/2 - 175, _game.Height/2), _whoWin == 1 ? Color.SkyBlue : Color.OrangeRed);
            foreach (var button in _buttons)
            {
                button.Draw(spriteBatch);
            }
            spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            foreach(var button in _buttons)
            {
                button.Update(gameTime);
            }
        }
    }
}