using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace TanksVS.Scripts;

public static class GameDraw
{
    public static void Draw(GraphicsDevice graphics, SpriteBatch spriteBatch, Game1 game)
    {
        graphics.Clear(Color.White);
        spriteBatch.Begin();

        foreach (var layer in game.MapManager.Map.Layers)
        {
            for (var j = 0; j < layer.Tiles.Count; j++)
            {
                var gid = layer.Tiles[j].Gid;
                if (gid == 0) continue;
                    
                var tileFrame = gid - 1;
                var col = tileFrame % game.MapManager.TileSetTileWide;
                var row = (int)Math.Floor((double)tileFrame / game.MapManager.TileSetTileWide);
                var x = j % game.MapManager.Map.Width * game.MapManager.Map.TileWidth;
                var y = (int)Math.Floor(j / (double)game.MapManager.Map.Width) * game.MapManager.Map.TileHeight;
                var rect = new Rectangle(game.MapManager.TileWidth * col, game.MapManager.TileHeight * row,
                    game.MapManager.TileWidth, game.MapManager.TileHeight);
                spriteBatch.Draw(game.MapManager.TileSet, 
                    new Rectangle(x, y, game.MapManager.TileWidth, 
                        game.MapManager.TileHeight), rect, Color.Wheat);
            }
        }

        foreach (var bullet in Player.Bullets)
        {
            spriteBatch.Draw(Bullet.Texture, bullet.Position, Color.Wheat);
        }

        foreach (var player in game.Players)
        {
            spriteBatch.DrawString(game.SpriteFont, player.Points.Count.ToString(), player.Points.Position,
                player.Id == 1 ? Color.OrangeRed : Color.SkyBlue);

            spriteBatch.Draw(player.TankTexture,
                new Rectangle((int)player.Position.X, (int)player.Position.Y, player.TankTexture.Width,
                    player.TankTexture.Height),
                null,
                Color.Wheat,
                player.Rotation,
                player.Origin,
                SpriteEffects.None,
                0f);
        }

        spriteBatch.End();
    }
}