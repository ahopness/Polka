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
        public static int screenWidth = 144; //256;
        public static int screenHeight = 128;//144;

        public static string version = "0.1a";
        public static string windowTitle = "POLKA :: v" + version;
        public static int windowScale = 4;

        public static int targetFPS = 60;

        public static Vector2 gravity = new Vector2( 0, -9.8f );
        #endregion

        public static Scene currentScene;

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
            currentScene = new Map();

            // Game Loop
            currentScene.Create();

            while (!Window.ShouldClose())
            {
                currentScene.Update( Time.GetFrameTime() );

                Graphics.BeginDrawing();
                Graphics.ClearBackground( Color.Black );

                    Graphics.BeginMode2D(viewport);

                    currentScene.Draw();

                    Graphics.EndMode2D();

                Graphics.EndDrawing();
            }
        }

        public void Dispose() 
        {
            // Finalização
            currentScene.Dispose();
            Window.Close();
        }

        public void ChangeScene(Scene scene)
        {
            currentScene.Dispose();
            currentScene = scene;
            currentScene.Create();
        }
    }
}