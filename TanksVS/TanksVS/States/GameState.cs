using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TanksVS.Scripts;

namespace TanksVS.States;

public class GameState : State
{
    private readonly List<Button> _buttons;

    public GameState(Game1 game, GraphicsDevice graphics, ContentManager content) : base(game, graphics, content)
    {
        var t_return = content.Load<Texture2D>("ExitLevel");
        var b_return = new Button(t_return, new Vector2(_game.Width / 2 - t_return.Width/2, 750));
        b_return.Click += ReturnToMain;
        _buttons = new List<Button>
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
        foreach (var button in _buttons)
        {
            button.Draw(spriteBatch);
        }
        spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        GameUpdate.Update(gameTime, _game, _content, _graphics);
        foreach (var button in _buttons)
        {
            button.Update(gameTime);
        }
    }
}