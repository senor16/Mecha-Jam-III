using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mecha_Jam_III
{
    public class Sprite : IActor
    {


        // IActor
        public Vector2 Position { get; set; }
        public Rectangle BoundingBox { get; set; }
        public float vx;
        public float vy;
        public bool ToRemove { get; set; }
        public List<Animation> lstAnimation;
        Animation currentAnimation = null;


        // Sprite
        public Texture2D Texture { get; }

        public Sprite(Texture2D pTexture)
        {
            Texture = pTexture;
            ToRemove = false;
            lstAnimation = new List<Animation>();
        }

        public void Move(float pX, float pY)
        {
            Position = new Vector2(Position.X + pX, Position.Y + pY);
        }

        public void AddAnimation(Animation pAnim)
        {
            lstAnimation.Add(pAnim);            
        }

        public void PlayAnimation(Animation.AnimationType pType)
        {
            foreach(Animation anim in lstAnimation)
            {
                if (anim.type.Equals(pType))
                {
                    currentAnimation = anim;
                    currentAnimation.currentFrame = 0;
                    break;                                     
                }
            }
        }

        public virtual void TouchedBy(IActor pBy)
        {

        }

        public virtual void Draw(SpriteBatch pSpriteBatch)
        {
            if (currentAnimation != null )
            {
                pSpriteBatch.Draw(currentAnimation.frames[(int)currentAnimation.currentFrame], Position, Color.White);
                Debug.WriteLine("Current frame : " +currentAnimation.currentFrame);
            }
            else
            {
                pSpriteBatch.Draw(Texture, Position, Color.White);
            }
        }

        public virtual void Update(GameTime pGameTime)
        {
            if (currentAnimation != null)
            {
                if (!currentAnimation.ended)
                    currentAnimation.currentFrame += currentAnimation.speed;
                if (!currentAnimation.ended && currentAnimation.currentFrame >= currentAnimation.frames.Count)
                {
                    if (currentAnimation.repeat)
                        currentAnimation.currentFrame = 0;
                    else
                    {
                        currentAnimation.currentFrame = currentAnimation.frames.Count - 1;
                        currentAnimation.ended = true;
                    }
                }
            }
            Move(vx, vy);
            BoundingBox = new Rectangle(
                (int)Position.X,
                (int)Position.Y,
                Texture.Width,
                Texture.Height
                );
        }
    }
}
