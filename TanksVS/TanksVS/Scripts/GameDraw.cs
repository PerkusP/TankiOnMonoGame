using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace TanksVS.Scripts
{
    public static class GameDraw
    {
        
        public static void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch, Game1 game)
        {
            
            graphics.Clear(Color.White);
            spriteBatch.Begin();
            game.MapManager.Draw();

            foreach (var bullet in Player.Bullets)
            {
                spriteBatch.Draw(Bullet.Texture, bullet.Position, Color.Wheat);
            }

            foreach (var player in game.Players)
            {
                spriteBatch.DrawString(Game1.SpriteFont, player.Points.Count.ToString(), player.Points.Position, player.ID == 1 ? Color.OrangeRed : Color.SkyBlue);
                if (player.IsAlive)
                {
                    spriteBatch.Draw(player.TankTexture,
                                    new Rectangle((int)player.Position.X, (int)player.Position.Y, player.TankTexture.Width, player.TankTexture.Height),
                                    null,
                                    Color.Wheat,
                                    player.Rotation,
                                    player.Origin,
                                    SpriteEffects.None,
                                    0f);
                }
            }

            spriteBatch.End();
        }
    }
}
