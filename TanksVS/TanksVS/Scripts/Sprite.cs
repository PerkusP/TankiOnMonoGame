using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TanksVS.Scripts;

public class Sprite
{
    private readonly Texture2D _texture;
    public Vector2 Position;
    private readonly Vector2 _velocity;
    public Rectangle Rectangle => new((int)Position.X, (int)Position.Y, _texture.Width, _texture.Height);

    protected Sprite(Vector2 position, Vector2 velocity, Texture2D texture)
    {
        Position = position;
        _texture = texture;
        _velocity = velocity;
    }

    public bool Collide(Rectangle rectangle) => Rectangle.Intersects(rectangle);

    protected bool InWall(Rectangle wall)
    {
        var coords = new Vector2(Position.X + _texture.Width / 2, Position.Y + _texture.Height / 2);
        return wall.X <= coords.X &&
               wall.X + wall.Width >= coords.X &&
               wall.Y <= coords.Y &&
               wall.Y + wall.Height >= coords.Y;
    }

    protected static bool InWall(Rectangle wall, Vector2 coords) => wall.X <= coords.X &&
                                                                    wall.X + wall.Width >= coords.X &&
                                                                    wall.Y <= coords.Y &&
                                                                    wall.Y + wall.Height >= coords.Y;

    protected bool IsTouchingLeft(Rectangle rect)
    {
        return Rectangle.Right > rect.Left &&
               Rectangle.Left < rect.Left &&
               Rectangle.Bottom > rect.Top &&
               Rectangle.Top < rect.Bottom;
    }

    protected bool IsTouchingRight(Rectangle rect)
    {
        return Rectangle.Left < rect.Right &&
               Rectangle.Right > rect.Right &&
               Rectangle.Bottom > rect.Top &&
               Rectangle.Top < rect.Bottom;
    }

    protected bool IsTouchingTop(Rectangle rect)
    {
        return Rectangle.Bottom > rect.Top &&
               Rectangle.Top < rect.Top &&
               Rectangle.Right > rect.Left &&
               Rectangle.Left < rect.Right;
    }

    protected bool IsTouchingBottom(Rectangle rect)
    {
        return Rectangle.Top < rect.Bottom &&
               Rectangle.Bottom > rect.Bottom &&
               Rectangle.Right > rect.Left &&
               Rectangle.Left < rect.Right;
    }

    public static Vector2 GetChangedRotation(float degrees) => new((float)Math.Cos(degrees), (float)Math.Sin(degrees));
}