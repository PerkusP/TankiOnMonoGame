using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TanksVS
{
    class Bullet : Game
    {

        public Vector2 Position;
        public Vector2 Direction;
        private const int _speed = 200;

        public Bullet(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
        }

        public void Update()
        {
            if (Position.X <= Player.Graphics.PreferredBackBufferWidth)
            {
                Position.X += Direction.X;
            }
            if (Position.Y <= Player.Graphics.PreferredBackBufferHeight)
            {
                Position.Y += Direction.Y;
            }
        }
    }
}
