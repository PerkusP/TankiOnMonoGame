using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace TanksVS
{
    public class Player
    {

        public Dictionary<Keys, Actions> ControlDictionary { get; set; }

        public readonly static List<Bullet> Bullets = new();

        public Vector2 _position;

        public Vector2 Origin { get; }

        public Texture2D TankTexture { get; }

        public float Rotation { get; private set; }

        public Vector2 Position => _position;

        public bool IsAlive { get; set; }

        private DateTime _fireTime;
        private const float _speed = 200f;


        public Player(Vector2 position, float rotation, Texture2D texture)
        {
            _position = position;
            Rotation = rotation;
            TankTexture = texture;
            Rotation = MathHelper.Clamp(0, 0, 3.14f);
            _fireTime = DateTime.Now;
            Origin = new Vector2(TankTexture.Width / 2, TankTexture.Height / 2);
            IsAlive = true;
        }

        public void Control(GameTime gameTime, Keys[] keys)
        {
            if (IsAlive) 
            {
                var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
                var deltaRotation = 3f;
            
                foreach (var key in keys)
                {
                    if (ControlDictionary.ContainsKey(key)) 
                    { 
                        switch (ControlDictionary[key])
                        {
                            case Actions.Left:
                                Rotation -= deltaRotation * deltaSeconds;
                                break;
                            case Actions.Right:
                                Rotation += deltaRotation * deltaSeconds;
                                break;
                            case Actions.Forward:
                                _position += GetChangedRotation(Rotation) * deltaSeconds * _speed;
                                break;
                            case Actions.Backward:
                                _position -= GetChangedRotation(Rotation) * deltaSeconds * _speed;
                                break;
                            case Actions.Fire:
                                if(DateTime.Now.Subtract(_fireTime).TotalSeconds > 1)
                                    Fire();
                                break;
                            default: 
                                continue;
                        }
                    }
                }

                Bound();
            }
        }

        public bool Collide(Bullet bullet)
        {
            var playerRect = new Rectangle((int)_position.X,(int)_position.Y, TankTexture.Width, TankTexture.Height);
            var bulletRect = new Rectangle((int)bullet.Position.X, (int)bullet.Position.Y, Bullet.Texture.Width, Bullet.Texture.Height);
            return playerRect.Intersects(bulletRect);
        }

        private void Fire()
        {
            _fireTime = DateTime.Now;
            Bullets.Add(new Bullet(GetPositionForFire, GetChangedRotation(Rotation)));
        }

        private void Bound()
        {
            if (_position.X > Game1.Width - TankTexture.Width)
            {
                _position.X = Game1.Width - TankTexture.Width;
            }
            else if (_position.X < TankTexture.Width)
            {
                _position.X = TankTexture.Width;
            }

            if (_position.Y > Game1.Height - TankTexture.Height)
            {
                _position.Y = Game1.Height - TankTexture.Height;
            }
            else if (_position.Y < TankTexture.Height)
            {
                _position.Y = TankTexture.Height;
            }
        }

        private Vector2 GetPositionForFire => _position + GetChangedRotation(Rotation) * 50;

        private static Vector2 GetChangedRotation(float degrees) => new((float)Math.Cos(degrees), (float)Math.Sin(degrees));
    }
}
