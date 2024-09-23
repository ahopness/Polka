using System.Xml;

namespace Polka
{
    public class Tilemap
    {
        public SortedList<string, Tilelayer> layerList;
        public SortedList<string, Object> objectList;

        public int width;
        public int height;

        public Tilemap(string filePath, Tileset tileset)
        {
            XmlDocument document = new XmlDocument();
            document.Load( filePath );

            XmlNode root = document.DocumentElement;

            int _mapWidth = int.Parse(root.Attributes["width"].Value);
            int _mapHeight = int.Parse(root.Attributes["height"].Value);

            layerList = new SortedList<string, Tilelayer>();
            objectList = new SortedList<string, Object>();
            foreach ( XmlNode childNode in root.ChildNodes )
            {
                if ( childNode.Name == "layer" )
                {
                    Tilelayer layer = new Tilelayer();

                    XmlNode dataNode = childNode.SelectSingleNode( "data" );
                    string csvData = dataNode.InnerText.Trim();
                    string[] rows = csvData.Split(
                        new char[] { '\n', '\r' },
                        StringSplitOptions.RemoveEmptyEntries
                        );
                    
                    layer.tileset = tileset;
                    layer.mapGrid = new int[_mapWidth, _mapHeight];

                    for( int rowIndex = 0; rowIndex < rows.Length; rowIndex++ )
                    {
                        string[] tileIds = rows[rowIndex].Split(',', StringSplitOptions.RemoveEmptyEntries);

                        for ( int colIndex = 0; colIndex < tileIds.Length; colIndex++ )
                        {
                            layer.mapGrid[colIndex, rowIndex] = int.Parse( tileIds[colIndex] );
                        }
                    }

                    string name = childNode.Attributes["name"].Value;
                    layerList.Add( name, layer );
                }
                else if ( childNode.Name == "objectgroup" )
                {
                    foreach (XmlNode objectNode in childNode.ChildNodes)
                    {
                        if ( objectNode.Attributes["name"].Value == "player" )
                        {
                            Player player = new Player();
                            player.position.X = int.Parse(objectNode.Attributes["x"].Value);
                            player.position.Y = int.Parse(objectNode.Attributes["y"].Value);

                            objectList.Add("player", player);
                        }
                    }
                    
                }
            }

        }
    }
}