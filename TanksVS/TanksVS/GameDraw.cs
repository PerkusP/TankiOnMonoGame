using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;


namespace TanksVS
{
    public static class GameDraw
    {
        public static GraphicsDeviceManager Graphics { get; set; }

        public static SpriteBatch SpriteBatch { get; set; }
        
        public static void Draw(Player[] players)
        {
            Graphics.GraphicsDevice.Clear(Color.White);
            SpriteBatch.Begin();
            foreach (var player in players)
            {
                SpriteBatch.Draw(player.TankTexture,
                                new Rectangle((int)player.Position.X, (int)player.Position.Y, player.TankTexture.Width + 5, player.TankTexture.Height + 5),
                                null,
                                Color.Wheat,
                                player.Rotation,
                                player.Origin,
                                SpriteEffects.None,
                                0f);
            }
            foreach (var bullet in Player.Bullets)
            {
                SpriteBatch.Draw(Bullet.Texture, bullet.Position, Color.Wheat);
            }



            SpriteBatch.End();

        }

    }
}
