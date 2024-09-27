using Polka.Core;
using System.Numerics;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Transformations;

using Object = Polka.Core.Object;

namespace Polka
{
    public class Tilelayer : Object
    {
        public Tileset tileset;
        public int[,] mapGrid;

        public override void Create()
        {
            for ( int i = 0; i < mapGrid.GetLength(0); i++ )
            {
                for (int j = 0; j < mapGrid.GetLength(1); j++)
                {
                    int tileIndex = mapGrid[i, j] - 1;
                    if ( tileIndex == -1 ) continue; // Vazio

                    Tile tile = tileset.tileList[tileIndex];

                    Rectangle collisionRect = new Rectangle(
                        i * tileset.tileWidth,
                        j * tileset.tileHeight,
                        tileset.tileWidth,
                        tileset.tileHeight
                        );

                    Game.currentMap.collisionList.Add( collisionRect );
                }
            }

            base.Draw();
        }

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