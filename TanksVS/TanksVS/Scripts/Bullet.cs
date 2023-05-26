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
       
        public static new Texture2D Texture;
        public Vector2 Direction;
        public bool Ricocheted;
        public int WhoShooted;
        private DateTime _createdTime;

        public static int Speed { get; set; }

        public Bullet(Vector2 position, Vector2 direction, int whoShooted) : base(position, direction, Texture)
        {
            Position = position;
            Direction = direction;
            Ricocheted = false;
            WhoShooted = whoShooted;
            _createdTime = DateTime.Now;
        }

        public DateTime CreatedTime => _createdTime;

        public bool Alive 
        { 
            get 
            {
                return DateTime.Now.Subtract(_createdTime).TotalSeconds < 5;
            }
        }

        public void Control(GameTime gameTime, Game1 game)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            foreach(var wall in game.Collision)
            {
                if (InWall(wall))
                {
                    _createdTime = new DateTime(1979, 9, 3);
                    return;
                }
            }

            foreach(var wall in game.Collision)
            {
                if (Collide(wall))
                {
                    if (IsTouchingTop(wall) || IsTouchingBottom(wall))
                    {
                        Direction = new Vector2(Direction.X, -Direction.Y);
                    }

                    if (IsTouchingRight(wall) || IsTouchingLeft(wall))
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
