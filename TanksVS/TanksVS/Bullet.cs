using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace TanksVS
{
    public class Bullet : Sprite
    {
        private readonly DateTime createdTime;
        public Vector2 Direction;
        public bool Ricocheted;
        public int WhoShooted;
        public int Speed { get; private set; }

        public Bullet(Vector2 position, Vector2 direction, int whoShooted) : base(position, direction, Texture)
        {
            Position = position;
            Direction = direction;
            Ricocheted = false;
            createdTime = DateTime.Now;
            Speed = 10;
            WhoShooted = whoShooted;
        }

        public DateTime CreatedTime => createdTime;

        public bool Alive 
        { 
            get 
            {
                return DateTime.Now.Subtract(createdTime).TotalSeconds < 7;
            }
        }

        public void Control(GameTime gameTime)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            foreach(var wall in Game1.colliders)
            {

                Position += Direction * deltaSeconds * Speed;

                if (Collide(wall))
                {
                    if (IsTouchingBottom(wall) || IsTouchingTop(wall))
                    {
                        Direction = new Vector2(Direction.X, -Direction.Y);
                        Ricocheted = true;
                    }

                    if (IsTouchingRight(wall) || IsTouchingLeft(wall))
                    {
                        Direction = new Vector2(-Direction.X, Direction.Y);
                       
                        Ricocheted = true;
                    }
                }
            }
        }

       
    }
}
