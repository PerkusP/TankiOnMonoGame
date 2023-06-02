using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TanksVS.Scripts;

namespace TanksVS.States;

public class MainMenuState : State
{
    private readonly List<Button> _buttons;
    public MainMenuState(Game1 game, GraphicsDevice graphics, ContentManager content) : base(game, graphics, content)
    {
        var newGameTexture = content.Load<Texture2D>("gButtonStart");
        var exitTexture = content.Load<Texture2D>("gButtonExit");
        var b_newGame = new Button(newGameTexture, new Vector2(_game.Width / 2 - newGameTexture.Width/2, _game.Height / 2 - newGameTexture.Height - 40));
        var b_Exit = new Button(exitTexture, new Vector2(_game.Width / 2 - exitTexture.Width/2, _game.Height / 2 + exitTexture.Height / 2));

        b_newGame.Click += NewGameButton_Click;
        b_Exit.Click += ExitButton_Click;


        _buttons = new List<Button>
        {
            b_newGame,
            b_Exit
        };
    }

    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Begin();
        spriteBatch.Draw(_content.Load<Texture2D>("BackGround"), new Rectangle(0,0,1600,800), Color.White);
        foreach (var button in _buttons)
        {
            button.Draw(spriteBatch);
        }
        spriteBatch.End();
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var button in _buttons)
        {
            button.Update(gameTime);
        }
    }

    private void ExitButton_Click(object sender, EventArgs e) => _game.Exit();

    private void NewGameButton_Click(object sender, EventArgs e) =>
        _game.ChangeState(new LevelState(_game, _graphics, _content));
}