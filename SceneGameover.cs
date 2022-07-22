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
            if (Keyboard.GetState().IsKeyDown(Keys.Enter))
            {
                mainGame.gameState.ChangeScene(GameState.SceneType.Menu);
            }
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {

            // My code here
            float ypos = 30;
            float xpos = (mainGame.TargetWidth - AssetManager.MainFont.MeasureString("GAME OVER").X) / 2;

            mainGame.spriteBatch.DrawString(AssetManager.MainFont, "GAME OVER!", new Vector2(xpos, ypos), Color.Wheat);

            ypos +=70+ AssetManager.MainFont.MeasureString("GAME OVER!").Y;
            xpos = (mainGame.TargetWidth - AssetManager.MainFont.MeasureString("THANKS FOR PLAYING").X) / 2;
            mainGame.spriteBatch.DrawString(AssetManager.MainFont, "THANKS FOR PLAYING", new Vector2(xpos, ypos),Color.Wheat);

            ypos += AssetManager.MainFont.MeasureString("THANKS FOR PLAYING").Y;
            xpos = (mainGame.TargetWidth - AssetManager.MainFont.MeasureString("BY SESSO KOSGA").X) / 2;
            mainGame.spriteBatch.DrawString(AssetManager.MainFont, "BY SESSO KOSGA", new Vector2(xpos, ypos), Color.Wheat);

            ypos += AssetManager.MainFont.MeasureString("BY SESSO KOSGA").Y;
            xpos = (mainGame.TargetWidth - AssetManager.MainFont.MeasureString("kosgasesso@gmail.com").X) / 2;
            mainGame.spriteBatch.DrawString(AssetManager.MainFont, "kosgasesso@gmail.com", new Vector2(xpos, ypos), Color.Wheat);

            ypos += AssetManager.MainFont.MeasureString("kosgasesso@gmail.com").Y+120;
            xpos = (mainGame.TargetWidth - AssetManager.MainFont.MeasureString("PRESS `ENTER` TO HEAD BACK TO THE MAIN SCREEN").X) / 2;
            mainGame.spriteBatch.DrawString(AssetManager.MainFont, "PRESS `ENTER` TO HEAD BACK TO THE MAIN SCREEN", new Vector2(xpos, ypos), Color.Wheat);


            base.Draw(gameTime);
        }
    }
}
