using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TanksVS
{
    public class Wall
    {
        public Rectangle Rectangle;
        public Wall(Rectangle rectangle) 
        {
            Rectangle = rectangle;
        }
    };
    
    public class Walls
    {
        private string map = 
@"WWWWWWWWWWWWWWWWWWWWWWWWWWW
W.W.......................W
W........W................W
W.W.......................W
W.W.......................W
W.W.......................W
W.W................W......W
W.W...W...................W
W.W............W..........W
W.W......W................W
W.W...............W.......W
W.W.......................W
W.W.......................W
WWWWWWWWWWWWWWWWWWWWWWWWWWW";
                                            //private string map =
                                            //@".WWWW...W.
                                            //......W.W. 
                                            //.WWWW.W...
                                            //......W.W.
                                            //.W......W.
                                            //.W.W.WW...
                                            //...W..W.W.
                                            //.W.W..W.W.
                                            //.W.WW...W.
                                            //.W......W."
                                            
        

        public static Texture2D Texture { get; set; }

        public List<Wall> Positions;

        private readonly int width = Texture.Width / 2;
        private readonly int height = Texture.Height / 2;
        
        
        public Walls()
        {
            Positions = new List<Wall>();
            //Texture = new Texture2D(Graphics.GraphicsDevice, 1, 1);
            //Texture.SetData(new[] { Color.Black });
        }

        public void CreateWalls() 
        {
            var currentMap = map.Split("\r\n", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < currentMap[0].Length; i++)
            {
                for (int j = 0; j < currentMap.Length; j++)
                {
                    if (currentMap[j][i] == 'W')
                        Positions.Add(new Wall(new Rectangle(i * width, j * height, width, height)));
                } 
            }
        }


        public void Update()
        {

        }
    }
}
