using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mecha_Jam_III
{
    class SceneGameover : Scene
    {
        public SceneGameover(MainGame pGame) : base(pGame)
        {

        }

        public override void Load()
        {
            base.Load();
        }

        public override void UnLoad()
        {
            // My code here

            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            // My code here

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            // My code here
            mainGame.spriteBatch.DrawString(AssetManager.MainFont, "GAME OVER!", new Vector2(1, 1), Color.White);

            base.Draw(gameTime);
        }
    }
}
