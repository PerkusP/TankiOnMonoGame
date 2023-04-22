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

        public static void Update(GameTime gameTime, Player[] players, Keys[] keys)
        {
            foreach (var player in players)
            {
                player.Control(gameTime, keys);
                for (var i = 0; i < Player.Bullets.Count; i++)
                {
                    Player.Bullets[i].Control(gameTime);
                    if (player.Collide(Player.Bullets[i]) && Player.Bullets[i].Ricocheted)
                        player.IsAlive = false;
                    if (!Player.Bullets[i].Alive)
                    {
                        Player.Bullets.RemoveAt(i);
                    }
                }
            }
            
        }
    }
}
