using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;


namespace TanksVS.Scripts
{
    public class Player : Sprite
    {
        bool IsSlowed = false;
        private Dictionary<Keys, Actions> ControlDictionary;

        public static List<Bullet> Bullets = new();

        public Points Points { get; set; }

        public int ID { get; private set; }

        public Vector2 Origin { get; }

        public Texture2D TankTexture { get; private set; }

        public float Rotation { get; private set; }

        public bool IsAlive { get; set; }

        public static new Vector2 Velocity { get; private set; }

        private DateTime _fireTime;



        public Player(Vector2 position, float rotation, Texture2D texture, int id, Dictionary<Keys, Actions> controlDictionary) : base(position, Velocity, texture)
        {
            Velocity = new(150,150);
            ControlDictionary = controlDictionary;
            Position = position;
            Rotation = rotation;
            TankTexture = texture;
            Rotation = MathHelper.Clamp(0, 0, 3.14f);
            _fireTime = DateTime.Now;
            Origin = new Vector2(TankTexture.Width / 2, TankTexture.Height / 2);
            IsAlive = true;
            ID = id;
            Points = new Points(0, new Vector2(id == 1 ? 1550 : 20, 10));
        }

        public void Control(GameTime gameTime, Keys[] keys, Game1 game)
        {
            if (IsAlive)
            {
                Bound(game);
                var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
                var deltaRotation = 3f;
                
                foreach (var key in keys)
                {
                    if (key == Keys.R)
                    {
                        Respawn(game);
                    }
                       
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
            }
        }


        private void Fire()
        {
            _fireTime = DateTime.Now;
            Bullets.Add(new Bullet(GetPositionForFire, GetChangedRotation(Rotation), ID));
        }

        private void Bound(Game1 game)
        {
            foreach (var wall in game.Colliders)
            {
                if (Collide(wall))
                {
                    if (IsTouchingLeft(wall))
                        Position.X = wall.Left - TankTexture.Width / 2 - 23;
                    else if (IsTouchingRight(wall))
                        Position.X = wall.Right;
                    else if (IsTouchingBottom(wall))
                        Position.Y = wall.Top - TankTexture.Height / 2 - 15;
                    else if (IsTouchingTop(wall))
                        Position.Y = wall.Bottom;
                }
            }

            foreach (var wall in game.Slow)
            {
                if (Collide(wall))
                {
                    IsSlowed = true;
                    break;
                }
                IsSlowed = false;
            }

            if (IsSlowed)
                Velocity = new(70, 70);
            else
                Velocity = new(150, 150);

        }

        public void Respawn(Game1 game)
        {
            Position = RandomCoordinates(game.Colliders);
            IsAlive = true;
        }

        private Vector2 RandomCoordinates(List<Rectangle> colliders)
        {
            var rand = new Random();

            var randomCoordinates = new Vector2(400, 120);
            Vector2 coords = new Vector2(rand.Next(120, 1500), rand.Next(140, 700));

            foreach (var wall in colliders)
            {
                if (InWall(wall, coords))
                {
                    coords = new Vector2(rand.Next(120, 1500), rand.Next(140, 700));
                }
            }

            return coords;
        }

        


        public static Player Init(Vector2 position, float rotation,string textureName, int id, Dictionary<Keys, Actions> controlDictionary, ContentManager content)
        {
            return new Player(position,
                rotation,
                content.Load<Texture2D>(textureName), id, controlDictionary);
        }

        private Vector2 GetPositionForFire => Position + GetChangedRotation(Rotation) * 30;

        private static Vector2 GetChangedRotation(float degrees) => new((float)Math.Cos(degrees), (float)Math.Sin(degrees));
    }
}
