using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;


namespace TanksVS.Scripts;

public class Player : Sprite
{
    public static readonly List<Bullet> Bullets = new();
    public Vector2 Origin { get; }
    public Points Points { get; }
    public int Id { get; }
    public Texture2D TankTexture { get; }
    public bool IsAlive { get; set; }
    public float Rotation { get; private set; }
    private static Vector2 _velocity;
    private bool _isSlowed;
    private DateTime _fireTime;
    private readonly Dictionary<Keys, Actions> _сontrolDictionary;
    
    public static Player Init(Vector2 position, float rotation, string textureName, int id,
        Dictionary<Keys, Actions> controlDictionary, ContentManager content) => new Player(position,
        rotation,
        content.Load<Texture2D>(textureName), id, controlDictionary);
    
    public void Respawn(IEnumerable<Rectangle> collision)
    {
        Position = RandomCoordinates(collision);
        IsAlive = true;
    }

    public void Control(GameTime gameTime, IEnumerable<Keys> keys, IEnumerable<Rectangle> collision,
        IEnumerable<Rectangle> dieCollision, IEnumerable<Rectangle> slowCollision)
    {
        if (!IsAlive) return;
        Bound(collision,dieCollision, slowCollision);
        var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
        const float deltaRotation = 3f;

        foreach (var key in keys)
        {
            if (_сontrolDictionary.ContainsKey(key))
            {
                switch (_сontrolDictionary[key])
                {
                    case Actions.Left:
                        Rotation -= deltaRotation * deltaSeconds;
                        break;
                    case Actions.Right:
                        Rotation += deltaRotation * deltaSeconds;
                        break;
                    case Actions.Forward:
                        Position += GetChangedRotation(Rotation) * deltaSeconds * _velocity;
                        break;
                    case Actions.Backward:
                        Position -= GetChangedRotation(Rotation) * deltaSeconds * _velocity;
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
    
    private Player(Vector2 position, float rotation, Texture2D texture, int id,
        Dictionary<Keys, Actions> controlDictionary) : base(position, _velocity, texture)
    {
        _velocity = new Vector2(150, 150);
        _сontrolDictionary = controlDictionary;
        Position = position;
        Rotation = rotation;
        TankTexture = texture;
        _fireTime = new DateTime(1999, 2, 3, 20, 25, 10, 200);
        Origin = new Vector2(TankTexture.Width / 2, TankTexture.Height / 2);
        IsAlive = true;
        Id = id;
        Points = new Points(0, new Vector2(id == 1 ? 1550 : 20, 10));
    }
    
    private static Vector2 RandomCoordinates(IEnumerable<Rectangle> collision)
    {
        var rand = new Random();
        var coords = new Vector2(rand.Next(120, 1500), rand.Next(140, 700));
        foreach (var wall in collision)
            if (InWall(wall, coords))
                coords = new Vector2(rand.Next(120, 1500), rand.Next(140, 700));
        return coords;
    }
    
    private void Bound(IEnumerable<Rectangle> collision, IEnumerable<Rectangle> dieCollision,
        IEnumerable<Rectangle> slowCollision)
    {
        if (dieCollision.Any(Collide))
        {
            Points.Count++;
            Respawn(collision);
            return;
        }

        foreach (var wall in collision.Where(Collide))
        {
            if (IsTouchingLeft(wall))
                Position.X = wall.Left - TankTexture.Width / 2 - 23;
            else if (IsTouchingTop(wall))
                Position.Y = wall.Top - TankTexture.Height / 2 - 13;
            if (IsTouchingRight(wall))
                Position.X = wall.Right;
            else if (IsTouchingBottom(wall))
                Position.Y = wall.Bottom;
        }

        foreach (var wall in slowCollision)
        {
            if (Collide(wall))
            {
                _isSlowed = true;
                break;
            }
            _isSlowed = false;
        }

        _velocity = _isSlowed ? new Vector2(70, 70) : new Vector2(150, 150);
    }

    private void Fire()
    {
        _fireTime = DateTime.Now;
        var bullet = new Bullet(GetPositionForFire, GetChangedRotation(Rotation), Id);
        Bullets.Add(bullet);
    }

    private Vector2 GetPositionForFire => Position + GetChangedRotation(Rotation) * 30;
}