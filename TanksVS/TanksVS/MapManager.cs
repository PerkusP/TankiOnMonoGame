using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TiledSharp;
namespace Project2
{
    public class MapManager
    {
        public static SpriteBatch SpriteBatch { get; set; }
        private readonly TmxMap _map;
        private readonly Texture2D _tileSet;
        private readonly int _tileSetTileWide;
        private readonly int _tileWidth;
        private readonly int _tileHeight;

        public MapManager(TmxMap map, Texture2D tileSet, int tileSetTileWide, int tileWidth, int tileHeight)
        {
            _map = map;
            _tileSet = tileSet;
            _tileSetTileWide = tileSetTileWide;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }

        public void Draw()
        {

            for (int i = 0; i < _map.Layers.Count; i++)
            {
                for (var j = 0; j < _map.Layers[i].Tiles.Count; j++)
                {
                    int gid = _map.Layers[i].Tiles[j].Gid;
                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int col = tileFrame % _tileSetTileWide;
                        int row = (int)Math.Floor((double)tileFrame / _tileSetTileWide);
                        float x = (j % _map.Width) * _map.TileWidth;
                        float y = (float)Math.Floor(j / (double)_map.Width) * _map.TileHeight;
                        var rect = new Rectangle(_tileWidth * col, _tileHeight * row, _tileWidth, _tileHeight);
                        SpriteBatch.Draw(_tileSet, new Rectangle((int)x, (int)y, _tileWidth, _tileHeight), rect, Color.Wheat);
                    }
                }
            }
        }
    }
}
