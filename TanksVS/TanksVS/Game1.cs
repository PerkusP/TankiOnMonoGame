using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;



namespace TanksVS
{
    public class Game1 : Game
    {
        private Player _player1;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            ChangeResolution(1920, 1080);
            _graphics.ToggleFullScreen();
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _player1 = new Player(new Vector2(100, 200), Content.Load<Texture2D>("tank1"));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            _player1.Control(gameTime, _graphics);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _graphics.GraphicsDevice.Clear(Color.White);
            _spriteBatch.Begin();

            _spriteBatch.Draw(_player1.TankTexture,
                new Rectangle((int)_player1.Position.X,(int)_player1.Position.Y, _player1.TankTexture.Width + 5, _player1.TankTexture.Height + 5), 
                null,
                Color.Wheat,
                _player1.Rotation, 
                _player1.Origin,
                SpriteEffects.None,
                0f);
           
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