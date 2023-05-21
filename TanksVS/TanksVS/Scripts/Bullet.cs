using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;

namespace TanksVS.Scripts
{
    public class Bullet : Sprite
    {
        private DateTime createdTime;
        public static new Texture2D Texture;
        public Vector2 Direction;
        public bool Ricocheted;
        public int WhoShooted;

        public static int Speed { get; set; }

        public Bullet(Vector2 position, Vector2 direction, int whoShooted) : base(position, direction, Texture)
        {
            Position = position;
            Direction = direction;
            Ricocheted = false;
            createdTime = DateTime.Now;
            WhoShooted = whoShooted;
        }

        public DateTime CreatedTime => createdTime;

        public bool Alive 
        { 
            get 
            {
                return DateTime.Now.Subtract(createdTime).TotalSeconds < 5;
            }
        }

        public void Control(GameTime gameTime, Game1 game)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            foreach(var wall in game.Colliders)
            {
                if (InWall(wall))
                    createdTime = new DateTime(1979, 9, 3);
                
                if (Collide(wall))
                {
                    if (IsTouchingBottom(wall) || IsTouchingTop(wall))
                    {
                        Direction = new Vector2(Direction.X, -Direction.Y);
                    }

                    else if (IsTouchingRight(wall) || IsTouchingLeft(wall))
                    {
                        Direction = new Vector2(-Direction.X, Direction.Y);
                    }

                    Ricocheted = true;
                }

                Position += Direction * deltaSeconds * Speed;
            }
        }
    }
}
