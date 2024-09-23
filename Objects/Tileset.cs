using System.Xml;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Transformations;

namespace Polka
{
    public class Tile
    {
        public int index;
        public bool solid;
        public Rectangle rect;
    }

    public class Tileset
    {
        public SortedList<int, Tile> tileList;
        public Texture2D spriteSheet;

        public int tileWidth;
        public int tileHeight;
        int _tileCount;
        int _tileColumns;

        public Tileset(string filePath, string spriteSheetPath)
        {
            // Textura
            spriteSheet = Texture2D.Load(spriteSheetPath);

            // Informações base
            XmlDocument document = new XmlDocument();
            document.Load(filePath);

            XmlNode root = document.DocumentElement;

            tileWidth = int.Parse(root.Attributes["tilewidth"].Value);
            tileHeight = int.Parse(root.Attributes["tileheight"].Value);
            _tileCount = int.Parse(root.Attributes["tilecount"].Value);
            _tileColumns = int.Parse(root.Attributes["columns"].Value);

            // Informações de cada tile
            tileList = new SortedList<int, Tile>();

            foreach (XmlNode childNode in root.ChildNodes)
            {
                if ( childNode.Name == "tile" )
                {
                    int _index = int.Parse(childNode.Attributes["id"].Value);
                    
                    XmlNode propertyNode = childNode.SelectSingleNode("properties").SelectSingleNode("property");
                    bool _solid = bool.Parse(propertyNode.Attributes["value"].Value);

                    Tile tile = new Tile();

                    tile.index = _index;
                    tile.solid = _solid;

                    int _col = _index % _tileColumns;
                    int _row = _index / _tileColumns;
                    tile.rect = new Rectangle(
                        _col * tileWidth,
                        _row * tileHeight,
                        tileWidth,
                        tileHeight
                    );

                    tileList.Add(_index, tile);
                }
            }
        }
    }
}