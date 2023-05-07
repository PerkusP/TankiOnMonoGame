using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace TanksVS
{
    public static class GameDraw
    {
        public static GraphicsDeviceManager Graphics { get; set; }
        public static SpriteBatch SpriteBatch { get; set; }
        
        public static void Draw()
        {
            Graphics.GraphicsDevice.Clear(Color.White);
            SpriteBatch.Begin();
            Game1.MapManager.Draw();
            foreach (var player in Game1.Players)
            {
                SpriteBatch.DrawString(Game1.SpriteFont, player.Points.Count.ToString(), player.Points.Position, Color.White);
                if (player.IsAlive)
                {
                    SpriteBatch.Draw(player.TankTexture,
                                    new Rectangle((int)player.Position.X, (int)player.Position.Y, player.TankTexture.Width, player.TankTexture.Height),
                                    null,
                                    Color.Wheat,
                                    player.Rotation,
                                    player.Origin,
                                    SpriteEffects.None,
                                    0f);
                }
            }

            foreach (var bullet in Player.Bullets)
            {
                SpriteBatch.Draw(Bullet.Texture, bullet.Position, Color.Wheat);
            }

            

            SpriteBatch.End();

        }

    }
}
