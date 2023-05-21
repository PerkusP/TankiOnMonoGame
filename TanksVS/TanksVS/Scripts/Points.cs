using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace TanksVS.Scripts
{
    public class Points
    {
        private int _count;
        public Vector2 Position { get; private set; }
        public Points(int points, Vector2 position) 
        {
            _count = points;
            Position = position;
        }

        public int Count 
        { 
            get 
            {  
                return _count;
            }
            set
            {
                 _count += 1;

            }
        }

    }
}
