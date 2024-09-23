namespace Polka
{
    public class Map : Scene
    {
        public Map()
        {
            Tileset tileset = new Tileset(
                "Content/Maps/tileset_1.tsx",
                "Content/Sprites/tiles.png"
                );
            
            Tilemap tilemap = new Tilemap(
                "Content/Maps/map_1.tmx",
                tileset
                );

            // Ordem de desenho
            objectList.Add(tilemap.layerList["bg"]);
            objectList.Add(tilemap.objectList["player"]);
            objectList.Add(tilemap.layerList["deco"]);
            objectList.Add(tilemap.layerList["solid"]);
        }
    }
}