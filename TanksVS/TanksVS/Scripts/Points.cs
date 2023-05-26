using Microsoft.Xna.Framework;

namespace TanksVS.Scripts
{
    public class Points
    {
        public Vector2 Position { get; private set; }
        private int _count;

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
