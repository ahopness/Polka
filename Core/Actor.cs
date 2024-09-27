using System.Numerics;
using Raylib_CSharp.Collision;
using Raylib_CSharp.Transformations;

namespace Polka.Core
{
    public class Actor : Sprite
    {
        public Vector2 velocity;
        public Rectangle collider;

        public override void Update(float deltaTime)
        {
            Move( velocity * deltaTime );

            base.Update(deltaTime);
        }

        #region Physics
        private Vector2 remainder; 
        public void Move(Vector2 value)
        {
            remainder += value;
            Vector2 move = remainder;
            remainder -= move;
            
            while (move.X != 0)
            {
                var sign = Math.Sign(move.X);
                if (!MovePixel(Vector2.UnitX * sign))
                {
                    Stop( StopModes.HORIZONTAL );
                    break;
                }
                else
                {
                    move.X -= sign;
                }
            }

            while (move.Y != 0)
            {
                var sign = Math.Sign(move.Y);
                if (!MovePixel(Vector2.UnitY * sign))
                {
                    Stop( StopModes.VERTICAL );
                    break;
                }
                else
                {
                    move.Y -= sign;
                }
            }
        }
        public bool MovePixel( Vector2 sign )
        {
            sign.X = Math.Sign(sign.X);
            sign.Y = Math.Sign(sign.Y);

            if (OverlapsAny(sign))
                return false;

            if (sign.Y > 0 && IsGrounded())
                return false;

            position += sign;
            return true;
        }

        public enum StopModes { HORIZONTAL, VERTICAL, BOTH }
        public void Stop( StopModes stopMode )
        {
            switch ( stopMode )
            {
                case StopModes.HORIZONTAL:
                    velocity.X = 0;
                    remainder.X = 0;
                    break;
                case StopModes.VERTICAL:
                    velocity.Y = 0;
                    remainder.Y = 0;
                    break;
                case StopModes.BOTH:
                    velocity = Vector2.Zero;
                    remainder = Vector2.Zero;
                    break;
            }
        }

        public bool OverlapsAny() => OverlapsAny( Vector2.Zero );
        public bool OverlapsAny( Vector2 offset )
        {
            Rectangle colliderRect = collider;
            colliderRect.Position += offset;

            foreach ( Rectangle scene_collider in Game.currentMap.collisionList )
            {
                if ( ShapeHelper.CheckCollisionRecs( colliderRect, scene_collider ) )
                    return true;
            }

            return false;
        }

        public bool IsGrounded()
        {
            foreach ( Rectangle scene_collider in Game.currentMap.collisionList )
            {
                if ( ShapeHelper.CheckCollisionPointRec( position, scene_collider ) )
                    return true;
            }

            return false;
        }
    }
    #endregion
}