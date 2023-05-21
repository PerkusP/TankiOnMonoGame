using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using TiledSharp;
namespace TanksVS.Scripts
{
    public class MapManager
    {
        public static SpriteBatch SpriteBatch { get; set; }

        public readonly TmxMap Map;
        private readonly Texture2D _tileSet;
        private readonly int _tileSetTileWide;
        private readonly int _tileWidth;
        private readonly int _tileHeight;

        public static MapManager Init(string map, string tileset,ContentManager content)
        {
            var _map = new TmxMap(map);
            var tileSet = content.Load<Texture2D>(tileset);
            var tileWidth = _map.Tilesets[0].TileWidth;
            var tileHeight = _map.Tilesets[0].TileHeight;
            var tilesetTilesSize = tileSet.Width / tileWidth;
            return new MapManager(_map, tileSet, tilesetTilesSize, tileWidth, tileHeight);
        }

        public MapManager(TmxMap map, Texture2D tileSet, int tileSetTileWide, int tileWidth, int tileHeight)
        {
            Map = map;
            _tileSet = tileSet;
            _tileSetTileWide = tileSetTileWide;
            _tileWidth = tileWidth;
            _tileHeight = tileHeight;
        }

        public void Draw()
        {

            for (int i = 0; i < Map.Layers.Count; i++)
            {
                for (var j = 0; j < Map.Layers[i].Tiles.Count; j++)
                {
                    int gid = Map.Layers[i].Tiles[j].Gid;
                    if (gid != 0)
                    {
                        int tileFrame = gid - 1;
                        int col = tileFrame % _tileSetTileWide;
                        int row = (int)Math.Floor((double)tileFrame / _tileSetTileWide);
                        float x = (j % Map.Width) * Map.TileWidth;
                        float y = (float)Math.Floor(j / (double)Map.Width) * Map.TileHeight;
                        var rect = new Rectangle(_tileWidth * col, _tileHeight * row, _tileWidth, _tileHeight);
                        SpriteBatch.Draw(_tileSet, new Rectangle((int)x, (int)y, _tileWidth, _tileHeight), rect, Color.Wheat);
                    }
                }
            }
        }
    }
}
