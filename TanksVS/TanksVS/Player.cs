using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TanksVS
{
    public class Player : Game
    {
        public float Rotation;

        private const float Speed = 300f;

        public readonly Vector2 Origin;

        public Texture2D TankTexture;

        public Vector2 Position;

        public Player(Vector2 position, Texture2D contentManager)
        {
            Position = position;
            Rotation = 1f;
            TankTexture = contentManager;
            Origin = new Vector2(TankTexture.Width / 2, TankTexture.Height / 2);
        }

        public void Control(GameTime gameTime, GraphicsDeviceManager graphics)
        {
            var kstate = Keyboard.GetState();
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var deltaRotation = 2f;
            if (kstate.IsKeyDown(Keys.D))
            {
                Rotation += deltaRotation * deltaSeconds;
            }
            if (kstate.IsKeyDown(Keys.A))
            {
                Rotation -= deltaRotation * deltaSeconds;
            }
            if (kstate.IsKeyDown(Keys.S))
            {
                Position.Y += Speed * deltaSeconds;
            }

            if (kstate.IsKeyDown(Keys.W))
            {
                Position.Y -= Speed * deltaSeconds;
            }

            if (Position.X > graphics.PreferredBackBufferWidth - TankTexture.Width / 2)
            {
                Position.X = graphics.PreferredBackBufferWidth - TankTexture.Width / 2;
            }
            else if (Position.X < TankTexture.Width / 2)
            {
                Position.X = TankTexture.Width / 2;
            }

            if (Position.Y > graphics.PreferredBackBufferHeight - TankTexture.Height / 2)
            {
                Position.Y = graphics.PreferredBackBufferHeight - TankTexture.Height / 2;
            }
            else if (Position.Y < TankTexture.Height / 2)
            {
                Position.Y = TankTexture.Height / 2;
            }
        }
    }
}
