using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;


namespace TanksVS
{
    public class Player : Game
    {
        public Texture2D TankTexture;
        public Vector2 Position;

        private const float Speed = 200f;

        public Player(Vector2 position)
        {
            Position = position;
        }

        public void Movement(GameTime gameTime)
        {
            var kstate = Keyboard.GetState();
            var deltaSeconds = (float)gameTime.ElapsedGameTime.TotalSeconds;
            if (kstate.IsKeyDown(Keys.Up))
            {
                Position.Y -= Speed * deltaSeconds;
            }

            if (kstate.IsKeyDown(Keys.Down))
            {
                Position.Y += Speed * deltaSeconds;
            }

            if (kstate.IsKeyDown(Keys.Left))
            {
                Position.X -= Speed * deltaSeconds;
            }

            if (kstate.IsKeyDown(Keys.Right))
            {
                Position.X += Speed * deltaSeconds;
            }
        }
    }

    public class Game1 : Game
    {
        private Player _player;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            ChangeResolution(1920, 1080);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _player = new Player(new Vector2(100, 200));

            base.Initialize();
        }

        protected override void LoadContent()
        {
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _player.TankTexture = Content.Load<Texture2D>("ball");

        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _player.Movement(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();

            _spriteBatch.Draw(_player.TankTexture, _player.Position, Color.White);
            _spriteBatch.End();
            base.Draw(gameTime);
        }

        private void ChangeResolution(int width, int height)
        {
            _graphics.PreferredBackBufferHeight = height;
            _graphics.PreferredBackBufferWidth = width;
            _graphics.ApplyChanges();
        }
    }
}