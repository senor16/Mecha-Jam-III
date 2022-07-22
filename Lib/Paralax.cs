using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mecha_Jam_III
{
    class Paralax
    {
        private List<Background> lstBackgrounds;
        private MainGame maingGame;
        public Paralax(MainGame pGame)
        {
            maingGame = pGame;
            lstBackgrounds = new List<Background>();
        }

        public void AddBackground(Background back)
        {
            lstBackgrounds.Add(back);
        }

        public void Update()
        {
            foreach(Background back in lstBackgrounds)
            {
                back.Update();
            }
        }

        public void Draw()
        {
            foreach (Background back in lstBackgrounds)
            {
                maingGame.spriteBatch.Draw(back.Image, back.Position, Color.White);
                if (back.Position.X <0)
                {
                    maingGame.spriteBatch.Draw(back.Image, back.Position+new Vector2(back.Image.Width,0), Color.White);
                }                
            }
        }


    }
}
