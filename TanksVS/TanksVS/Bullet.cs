using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Drawing;

namespace TanksVS
{
    public class Bullet
    {
        public static Texture2D Texture { get; set; }

        private readonly DateTime createdTime;
        public Vector2 Position;
        public Vector2 Direction;
        public bool Ricocheted;
        public int Speed { get; private set; }

        public Bullet(Vector2 position, Vector2 direction)
        {
            Position = position;
            Direction = direction;
            Ricocheted = false;
            createdTime = DateTime.Now;
            Speed = 150;
        }

        public DateTime CreatedTime => createdTime;

        public bool Alive 
        { 
            get 
            {
                return DateTime.Now.Subtract(createdTime).TotalSeconds < 4;
            }
        }

        public void Control(GameTime gameTime)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (Position.X < Game1.Width)
            {
                Position.X += Direction.X * deltaSeconds * Speed;
            }
            if (Position.Y < Game1.Height)
            {
                Position.Y += Direction.Y * deltaSeconds * Speed;
            }

            if (Position.X > Game1.Width - Bullet.Texture.Width || Position.X < Bullet.Texture.Width)
            {
                Direction = new Vector2(-Direction.X, Direction.Y);
                Ricocheted = true;
            }

            if (Position.Y > Game1.Height - Bullet.Texture.Height || Position.Y < Bullet.Texture.Height)
            {
                Direction = new Vector2(Direction.X, -Direction.Y);
                Ricocheted = true;
            }


            
        }

    }
}
