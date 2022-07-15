using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mecha_Jam_III
{
    class SceneGameplay : Scene
    {
        private Paralax paralax;
        public SceneGameplay(MainGame pGame) : base(pGame)
        {
            paralax = new Paralax(mainGame);
        }


        public override void Load()
        {
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("sky"),0));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("sun"), 1));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("ruins"), 2));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("houses1"), 3));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("houses2"), 4));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("house3"), 5));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("road"), 6));
            base.Load();
        }

        public override void UnLoad()
        {
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            paralax.Update();
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            paralax.Draw();
            base.Draw(gameTime);
        }

        
    }
}
