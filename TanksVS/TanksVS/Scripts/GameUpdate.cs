using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using TanksVS.States;

namespace TanksVS.Scripts
{
    public static class GameUpdate
    {
        public static void Update(GameTime gameTime, Keys[] keys, Game1 game, ContentManager content, GraphicsDevice graphics)
        {
            if (game.Players.Any(x => x.Points.Count >= 10)) 
            {
                game.ChangeState(new DeathState(game, graphics, content));
            }

            foreach (var player in game.Players)
            {
                player.Control(gameTime, keys, game);
                for (var i = 0; i < Player.Bullets.Count; i++)
                {
                    Player.Bullets[i].Control(gameTime, game);
                    if (!Player.Bullets[i].Alive)
                        Player.Bullets.RemoveAt(i);
                    else if(player.Collide(Player.Bullets[i].Rectangle) && Player.Bullets[i].Ricocheted ||
                            player.Collide(Player.Bullets[i].Rectangle) && player.ID != Player.Bullets[i].WhoShooted)
                    {
                        
                        player.Points.Count++;

                        Player.Bullets.RemoveAt(i);
                        player.IsAlive = false;
                        player.Respawn(game);
                        
                    }
                }
            }
        }
    }
}
