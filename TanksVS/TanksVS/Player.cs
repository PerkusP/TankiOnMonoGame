using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using System;
using System.Collections.Generic;

namespace TanksVS
{
    public class Player : Game
    {
        public static GraphicsDeviceManager Graphics { get; set; }

        public static Dictionary<Keys, Actions> ControlDictionary;

        public static SpriteBatch SpriteBatch { get; set; }

        private float Rotation;

        public Vector2 Position;

        public readonly Vector2 Origin;

        public readonly Texture2D TankTexture;

        private const float _speed = 200f;

        public Player(Vector2 position, float rotation, Texture2D texture)
        {
            Position = position;
            Rotation = rotation;
            TankTexture = texture;
            Rotation = MathHelper.Clamp(0, 0, 3.14f);
            Origin = new Vector2(TankTexture.Width / 2, TankTexture.Height / 2);

        }

        public void Control(GameTime gameTime)
        {
            var kState = Keyboard.GetState();
            var action = GetAction(kState);
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            var deltaRotation = 3f;
            #region
            if (kState.IsKeyDown(Keys.A))
            {
                Rotation -= deltaRotation * deltaSeconds;
            }
            if (kState.IsKeyDown(Keys.D))
            {
                Rotation += deltaRotation * deltaSeconds;
            }

            if (kState.IsKeyDown(Keys.W))
            {
                Position += ToVector2(Rotation) * deltaSeconds * _speed;
            }

            if (kState.IsKeyDown(Keys.S))
            {
                Position -= ToVector2(Rotation) * deltaSeconds * _speed;
            }

            if (kState.IsKeyDown (Keys.Q))
            {

            }

            #endregion
            Bound();
            //if (action == Actions.Right)
            //{
            //    Rotation += deltaRotation * deltaSeconds;
            //}
            //if (action == Actions.Left)
            //{
            //    Rotation -= deltaRotation * deltaSeconds;
            //}
            //if (action == Actions.Forward)
            //{
            //    Position += Direction * deltaSeconds * _speed;
            //}

            //if (action == Actions.Backward)
            //{
            //    Position -= Direction * deltaSeconds * _speed;
            //}

            
        }

        public double Angle => Math.Atan2(Position.Y, Position.X);

        public Vector2 ToVector2(float degrees) =>
            new Vector2((float)Math.Cos(degrees), (float)Math.Sin(degrees));

        private void Bound()
        {
            if (Position.X > Graphics.PreferredBackBufferWidth - TankTexture.Width)
            {
                Position.X = Graphics.PreferredBackBufferWidth - TankTexture.Width;
            }
            else if (Position.X < TankTexture.Width)
            {
                Position.X = TankTexture.Width;
            }

            if (Position.Y > Graphics.PreferredBackBufferHeight - TankTexture.Height)
            {
                Position.Y = Graphics.PreferredBackBufferHeight - TankTexture.Height;
            }
            else if (Position.Y < TankTexture.Height)
            {
                Position.Y = TankTexture.Height;
            }
        }

        public void Rotate()
        {

        }

        private Actions GetAction(KeyboardState keyState)
        {
            var action = Actions.None;
            if (keyState.IsKeyDown(Keys.A))
                action = ControlDictionary[Keys.A];
            if (keyState.IsKeyDown(Keys.S))
                action = ControlDictionary[Keys.S];
            if (keyState.IsKeyDown(Keys.D))
                action = ControlDictionary[Keys.D];
            if (keyState.IsKeyDown(Keys.W))
                action = ControlDictionary[Keys.W];
            if (keyState.IsKeyDown(Keys.Left))
                action = ControlDictionary[Keys.Left];
            if (keyState.IsKeyDown(Keys.Right))
                action = ControlDictionary[Keys.Right];
            if (keyState.IsKeyDown(Keys.Down))
                action = ControlDictionary[Keys.Down];
            if (keyState.IsKeyDown(Keys.Up))
                action = ControlDictionary[Keys.Up];
            return action;
        }
        public void Draw()
        {
            SpriteBatch.Draw(TankTexture,
                new Rectangle((int)Position.X, (int)Position.Y, TankTexture.Width + 5, TankTexture.Height + 5),
                null,
                Color.Wheat,
                Rotation,
                Origin,
                SpriteEffects.None,
                0f);
        }
        public new void Update(GameTime gameTime)
        {
            Control(gameTime);
        }
    }
}
