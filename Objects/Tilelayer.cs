using System.Numerics;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;

namespace Polka
{
    public class Tilelayer : Object
    {
        public Tileset tileset;
        public int[,] mapGrid;

        public override void Draw()
        {
            for ( int i = 0; i < mapGrid.GetLength(0); i++ )
            {
                for (int j = 0; j < mapGrid.GetLength(1); j++)
                {
                    int tileIndex = mapGrid[i, j] - 1;
                    if ( tileIndex == -1 ) continue; // Vazio

                    Tile tile = tileset.tileList[tileIndex];

                    Vector2 position = new Vector2(
                        i * tileset.tileWidth,
                        j * tileset.tileHeight
                        );

                    Graphics.DrawTextureRec(
                        tileset.spriteSheet,
                        tile.rect,
                        position,
                        Color.White
                        );
                }
            }

            base.Draw();
        }
    }
}