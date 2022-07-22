using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Mecha_Jam_III.Sprite;

namespace Mecha_Jam_III
{
    public class Animation
    {

        public enum AnimationType
        {
            ToRun,
            Run,
            Jump,
            Death
        }
        public List<Texture2D> frames;

        public float currentFrame;
        public float speed;
        public bool repeat;
        public AnimationType type;
        public bool ended;
        

        public Animation(List<Texture2D> pframes, AnimationType pType, float pSpeed, bool pRepeat)
        {
            frames = pframes;
            speed = pSpeed;
            repeat = pRepeat;
            type = pType;
            ended = false;
            currentFrame = 0;
            speed = pSpeed;
        }
    }
}
