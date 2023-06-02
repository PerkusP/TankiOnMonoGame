using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TanksVS.States;

public abstract class State
{
    protected ContentManager _content;
    protected GraphicsDevice _graphics;
    protected Game1 _game;

    public abstract void Draw(GameTime gameTime, SpriteBatch spriteBatch);

    protected State(Game1 game, GraphicsDevice graphics, ContentManager content)
    {
        _game = game;
        _graphics = graphics;
        _content = content;
    }

    public abstract void Update(GameTime gameTime);
}