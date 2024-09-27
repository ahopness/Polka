using Polka.Core;
using Raylib_CSharp;
using Raylib_CSharp.Camera.Cam2D;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Logging;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Windowing;
using System.Numerics;

namespace Polka
{
    public class Game : IDisposable
    {
        public static Game Instance { get; private set; }

        #region App Settings
        public static int screenWidth = 128;
        public static int screenHeight = 144;

        public static string version = "0.1a";
        public static string windowTitle = "POLKA :: v" + version;
        public static int windowScale = 4;

        public static int targetFPS = 60;

        public static Vector2 gravity = new Vector2( 0, -9.8f );
        #endregion

        public static Map currentMap;

        public void Run() 
        {
            // Inicialização
            int windowWidth = screenWidth * windowScale;
            int windowHeight = screenHeight * windowScale;

            Logger.SetTraceLogLevel( TraceLogLevel.Error );
            Raylib.SetConfigFlags( ConfigFlags.VSyncHint );
            Window.Init( windowWidth, windowHeight, windowTitle );
            
            Time.SetTargetFPS( targetFPS );

            Camera2D viewport = new Camera2D();
            viewport.Zoom = windowScale;

            // Criação da cena
            Tileset tileset = new Tileset(
                "Content/Maps/tileset.xml",
                "Content/Sprites/spritesheet.png"
                );
            
            currentMap = new Tilemap(
                "Content/Maps/t1.tmx", // Mapa inicial
                tileset
                );

            // Game Loop
            currentMap.Create();

            while (!Window.ShouldClose())
            {
                currentMap.Update( Time.GetFrameTime() );

                Graphics.BeginDrawing();
                Graphics.ClearBackground( Color.Black );

                    Graphics.BeginMode2D(viewport);

                    currentMap.Draw();

                    Graphics.EndMode2D();

                Graphics.EndDrawing();
            }
        }

        public void Dispose() 
        {
            // Finalização
            currentMap.Dispose();
            Window.Close();
        }

        public void ChangeMap(Map scene)
        {
            currentMap.Dispose();
            currentMap = scene;
            currentMap.Create();
        }
    }
}