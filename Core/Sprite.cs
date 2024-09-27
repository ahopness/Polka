using System.Numerics;
using Raylib_CSharp.Colors;
using Raylib_CSharp.Rendering;
using Raylib_CSharp.Textures;
using Raylib_CSharp.Transformations;

namespace Polka.Core
{
    public class Sprite : Object
    {
        #region Sprite
        public Texture2D sprite;
        public Vector2 origin;
        public Vector2 position;
        public float scale = 1f;
        public float rotation = 0f;
        public Color tint = Color.White;
        public bool flipped = false;
        public bool visible = true;
        #endregion

        #region Animation
        public bool animated = false;
        public int anmSlices = 1;
        public int anmSpeed = 30;
        public int anmFrame = 0;
        public int[] anm = {};
        private int _anmCounter = 0;
        private int _anmIdx = 0;
        #endregion

        public override void Update( float deltaTime )
        {
            if ( animated )
            {
                _anmCounter++;
                if ( _anmCounter > anmSpeed-1 )
                {
                    _anmCounter = 0;
                    
                    if ( anm.Length > 0 )
                    {
                        _anmIdx++;
                        if ( _anmIdx > anm.Length-1 ) _anmIdx = 0;
                        anmFrame = anm[_anmIdx];
                    }
                    else
                    {
                        anmFrame++;
                    }
                    
                    if ( anmFrame == anmSlices ) 
                    {
                        anmFrame = 0; 
                    }
                }
            }

            base.Update(deltaTime);
        }

        public override void Draw()
        {
            if ( visible )
            {
                // Funciona melhor em resoluçoes baixas :p
                //TODO: Criar tipo Vec2i
                position.X = MathF.Round(position.X);
                position.Y = MathF.Round(position.Y);

                Rectangle src = new Rectangle(
                    0,
                    0,
                    sprite.Width,
                    sprite.Height
                );
                Rectangle dest = new Rectangle(
                    position.X,
                    position.Y,
                    sprite.Width,
                    sprite.Height
                );

                if ( animated )
                {
                    int _sliceWidth = sprite.Width / anmSlices;

                    src = new Rectangle(
                        anmFrame * _sliceWidth,
                        0,
                        _sliceWidth,
                        sprite.Height
                    );
                    dest = new Rectangle(
                        position.X,
                        position.Y,
                        _sliceWidth,
                        sprite.Height
                    );
                }

                if ( flipped ) { src.Width = -src.Width; }
                
                dest.Size *= scale;

                Graphics.DrawTexturePro(
                    sprite,
                    src,
                    dest,
                    origin,
                    rotation,
                    tint
                );
            }

            base.Draw();
        }
    }
}