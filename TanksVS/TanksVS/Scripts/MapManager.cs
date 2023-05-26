using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TiledSharp;
namespace TanksVS.Scripts
{
    public class MapManager
    {
        public readonly TmxMap Map;
        public readonly Texture2D TileSet;
        public readonly int TileSetTileWide;
        public readonly int TileWidth;
        public readonly int TileHeight;

        public static MapManager Init(string map, string tileset,ContentManager content, Game1 game)
        {
            var _map = new TmxMap(map);
            var tileSet = content.Load<Texture2D>(tileset);
            var tileWidth = _map.Tilesets[0].TileWidth;
            var tileHeight = _map.Tilesets[0].TileHeight;
            var tilesetTilesSize = tileSet.Width / tileWidth;

            game.Collision = new();
            game.SlowCollision = new();
            game.DieCollision = new();

            AddObjectsToList(game.Collision, _map.ObjectGroups["Collision"].Objects);

            if (_map.ObjectGroups.Contains("Slow"))
                AddObjectsToList(game.SlowCollision, _map.ObjectGroups["Slow"].Objects);

            if (_map.ObjectGroups.Contains("Die"))
                AddObjectsToList(game.DieCollision, _map.ObjectGroups["Die"].Objects);


            return new MapManager(_map, tileSet, tilesetTilesSize, tileWidth, tileHeight);
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            
        }

        private MapManager(TmxMap map, Texture2D tileSet, int tileSetTileWide, int tileWidth, int tileHeight)
        {
            Map = map;
            TileSet = tileSet;
            TileSetTileWide = tileSetTileWide;
            TileWidth = tileWidth;
            TileHeight = tileHeight;
        }

        private static void AddObjectsToList(List<Rectangle> list, TmxList<TmxObject> objects)
        {
            foreach (var item in objects)
                list.Add(new Rectangle((int)item.X + 15, (int)item.Y + 10, (int)item.Width + 15, (int)item.Height + 10));
        }
    }
}
