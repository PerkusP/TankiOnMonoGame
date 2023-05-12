using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace TanksVS
{
    public class Player : Sprite
    {

        public Dictionary<Keys, Actions> ControlDictionary { get; set; }

        public static List<Bullet> Bullets = new();


        public Points Points { get; set; }

        public int ID { get; private set; }

        public Vector2 Origin { get; }

        public Texture2D TankTexture { get; private set; }

        public float Rotation { get; private set; }

        public bool IsAlive { get; set; }

        private DateTime _fireTime;



        public Player(Vector2 position, Vector2 velocity, float rotation, Texture2D texture, int id) : base(position, velocity, Texture)
        {
            Velocity = velocity;
            Position = position;
            Rotation = rotation;
            TankTexture = texture;
            Rotation = MathHelper.Clamp(0, 0, 3.14f);
            _fireTime = DateTime.Now;
            Origin = new Vector2(TankTexture.Width / 2, TankTexture.Height / 2);
            IsAlive = true;
            ID = id;
            Points = new Points(0, new Vector2(id == 1 ? 1560 : 20, 10));
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
                                Position += GetChangedRotation(Rotation) * deltaSeconds * Velocity;
                                break;
                            case Actions.Backward:
                                Position -= GetChangedRotation(Rotation) * deltaSeconds * Velocity;
                                break;
                            case Actions.Fire:
                                if (DateTime.Now.Subtract(_fireTime).TotalSeconds > 1)
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


        private void Fire()
        {
            _fireTime = DateTime.Now;
            Bullets.Add(new Bullet(GetPositionForFire, GetChangedRotation(Rotation), ID));
        }

        private void Bound()
        {
            foreach (var wall in Game1.colliders)
            {
                if (Collide(wall))
                {
                    if (IsTouchingLeft(wall))
                        Position.X = wall.Left - 20;
                    else if (IsTouchingRight(wall))
                        Position.X = wall.Right;
                    if (IsTouchingBottom(wall))
                        Position.Y = wall.Top - 15;
                    else if (IsTouchingTop(wall))
                        Position.Y = wall.Bottom;
                }
            }
        }

        public void Respawn()
        {
            var randomPos = new[] { new Vector2(400, 120), new Vector2(860, 140), new Vector2(680, 560), new Vector2(360, 660), new Vector2(200, 160), new Vector2(1380, 640), new Vector2(1100, 300) };
            var rand = new Random();
            Position = randomPos[rand.Next(0, randomPos.Length)];
            IsAlive = true;

        }

        private Vector2 GetPositionForFire => Position + GetChangedRotation(Rotation) * 30;

        private static Vector2 GetChangedRotation(float degrees) => new((float)Math.Cos(degrees), (float)Math.Sin(degrees));
    }
}
