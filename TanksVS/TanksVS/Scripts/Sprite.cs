using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TanksVS.Scripts
{
    public class Sprite
    {
        public Texture2D Texture;
        public Vector2 Position;
        public Vector2 Velocity;
        public Rectangle Rectangle { get { return new Rectangle((int)Position.X, (int)Position.Y, Texture.Width, Texture.Height); } }
        public Sprite(Vector2 position, Vector2 velocity, Texture2D texture)
        {
            Position = position;
            Texture = texture;
            Velocity = velocity;
        }

        public bool Collide(Rectangle rectangle) => Rectangle.Intersects(rectangle);

        public bool InWall(Rectangle wall)
        {
            var coords = new Vector2(Position.X + Texture.Width/2, Position.Y + Texture.Height/2);
            return wall.X <= coords.X &&
                wall.X + wall.Width >= coords.X &&
                wall.Y <= coords.Y &&
                wall.Y + wall.Height >= coords.Y;
        }


        public static bool InWall(Rectangle wall, Vector2 coords) => wall.X <= coords.X &&
            wall.X + wall.Width >= coords.X &&
            wall.Y <= coords.Y &&
            wall.Y + wall.Height >= coords.Y;

        public bool IsTouchingLeft(Rectangle rect)
        {
            return Rectangle.Right > rect.Left &&
                Rectangle.Left < rect.Left &&
                Rectangle.Bottom > rect.Top &&
                Rectangle.Top < rect.Bottom;
        }

        public bool IsTouchingRight(Rectangle rect)
        {
            return Rectangle.Left < rect.Right &&
                Rectangle.Right > rect.Right &&
                Rectangle.Bottom > rect.Top &&
                Rectangle.Top < rect.Bottom;
        }

        public bool IsTouchingBottom(Rectangle rect)
        {
            return Rectangle.Bottom > rect.Top &&
                Rectangle.Top < rect.Top &&
                Rectangle.Right > rect.Left &&
                Rectangle.Left < rect.Right;
        }

        public bool IsTouchingTop(Rectangle rect)
        {
            return Rectangle.Top < rect.Bottom &&
                Rectangle.Bottom > rect.Bottom &&
                Rectangle.Right > rect.Left &&
                Rectangle.Left < rect.Right;
        }
    }
}
