using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TanksVS.Scripts
{
    public class Bullet : Sprite
    {
        public bool Ricocheted;
        public int WhoShooted;
        public static Texture2D Texture;
        private Vector2 _direction;
        private DateTime _createdTime;
        public static int Speed { get; set; }

        public Bullet(Vector2 position, Vector2 direction, int whoShooted) : base(position, direction, Texture)
        {
            Position = position;
            _direction = direction;
            Ricocheted = false;
            WhoShooted = whoShooted;
            _createdTime = DateTime.Now;
        }

        public void Control(GameTime gameTime, Game1 game)
        {
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            
            foreach(var wall in game.Collision)
            {
                if (!InWall(wall)) continue;
                _createdTime = new DateTime(1979, 9, 3);
                return;
            }

            foreach(var wall in game.Collision)
            {
                if (Collide(wall))
                {
                    if (IsTouchingTop(wall) || IsTouchingBottom(wall))
                    {
                        _direction = new Vector2(_direction.X, -_direction.Y);
                    }

                    if (IsTouchingRight(wall) || IsTouchingLeft(wall))
                    {
                        _direction = new Vector2(-_direction.X, _direction.Y);
                    }
                    
                    Ricocheted = true;
                }
                Position += _direction * deltaSeconds * Speed;
            }
        }
        
        public bool Alive => DateTime.Now.Subtract(_createdTime).TotalSeconds < 5;
    }
}
