using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mecha_Jam_III
{
    class Util
    {
        static Random RandomGen = new Random();

        public static void SetRandomSeed(int pSeed)
        {
            RandomGen = new Random(pSeed);
        }
        
        public static int GetInt(int pMin, int pMax)
        {
            return RandomGen.Next(pMin, pMax + 1);
        }

        public static bool CollideHero(Obstacle obs, Hero hero)
        {
            
            return  obs.Position.X < hero.Position.X+hero.Texture.Width && hero.Position.X < obs.Position.X+obs.getDimens().X
                && obs.Position.Y < hero.Position.Y + hero.currentAnimation.frames[0].Height/2 && 
                hero.Position.Y < obs.Position.Y + obs.getDimens().Y;
        }
    }
}
