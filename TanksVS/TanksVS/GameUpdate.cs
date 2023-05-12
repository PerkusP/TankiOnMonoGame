using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;

namespace TanksVS
{
    public static class GameUpdate
    {
        public static GraphicsDeviceManager Graphics { get; set; }

        public static SpriteBatch SpriteBatch { get; set; }

        public static void Update(GameTime gameTime, Keys[] keys)
        {
            foreach (var player in Game1.Players)
            {
                player.Control(gameTime, keys);
                for (var i = 0; i < Player.Bullets.Count; i++)
                {
                    Player.Bullets[i].Control(gameTime);
                    if (!Player.Bullets[i].Alive)
                    {
                        Player.Bullets.RemoveAt(i);
                    }
                    if (Player.Bullets.Count > 0)
                    {
                        if (player.Collide(Player.Bullets[i].Rectangle) && Player.Bullets[i].Ricocheted ||
                            player.Collide(Player.Bullets[i].Rectangle) && player.ID != Player.Bullets[i].WhoShooted)
                        {
                           
                            player.Points.Count++;

                            Player.Bullets.RemoveAt(i);
                            player.IsAlive = false;
                            player.Respawn();
                        }
                    }
                    
                }
            }
            
        }
    }
}
