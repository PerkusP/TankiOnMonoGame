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

        public static MapManager Init(string map, string tileset, ContentManager content, Game1 game)
        {
            var tmxMap = new TmxMap(map);
            var tileSet = content.Load<Texture2D>(tileset);
            var tileWidth = tmxMap.Tilesets[0].TileWidth;
            var tileHeight = tmxMap.Tilesets[0].TileHeight;
            var tilesetTilesSize = tileSet.Width / tileWidth;

            game.Collision = new List<Rectangle>();
            game.SlowCollision = new List<Rectangle>();
            game.DieCollision = new List<Rectangle>();

            AddObjectsToList(game.Collision, tmxMap.ObjectGroups["Collision"].Objects);

            if (tmxMap.ObjectGroups.Contains("Slow"))
                AddObjectsToList(game.SlowCollision, tmxMap.ObjectGroups["Slow"].Objects);

            if (tmxMap.ObjectGroups.Contains("Die"))
                AddObjectsToList(game.DieCollision, tmxMap.ObjectGroups["Die"].Objects);


            return new MapManager(tmxMap, tileSet, tilesetTilesSize, tileWidth, tileHeight);
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
