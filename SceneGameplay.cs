using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Mecha_Jam_III
{
    class Hero : Sprite
    {

        public Hero(Texture2D pTexture) : base(pTexture)
        {

        }

        public override void TouchedBy(IActor pBy)
        {

        }
    }

    class SceneGameplay : Scene
    {
        private Paralax paralax;
        private Hero myHero;
        public SceneGameplay(MainGame pGame) : base(pGame)
        {
            paralax = new Paralax(mainGame);

        }


        public override void Load()
        {
            // Paralax setup
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("sky"),0));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("sun"), 1));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("ruins"), 2));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("houses1"), 3));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("house3"), 4));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("houses2"), 5));            
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("road"), 6));

            // Hero Creation
            myHero = new Hero(mainGame.Content.Load<Texture2D>("Sit_010"));
            myHero.Position = new Vector2(100, mainGame.graphics.PreferredBackBufferHeight - myHero.Texture.Height - 20);
            listActors.Add(myHero);
            
            /// Adding animation
            // Turn to run
            List<Texture2D> turnToRight = new List<Texture2D>();            
            for (int i=0; i<4; i++)
                turnToRight.Add(mainGame.Content.Load<Texture2D>("Turn_to_run_00"+i));
            myHero.AddAnimation(new Animation(turnToRight, Animation.AnimationType.ToRun,1f/8f, false));

            // Run
            List<Texture2D> run = new List<Texture2D>();
            for (int i = 0; i < 12; i++)
            {
                if (i<=9)
                run.Add(mainGame.Content.Load<Texture2D>("Run_00" + i));
                else
                    run.Add(mainGame.Content.Load<Texture2D>("Run_0" + i));
            }
            myHero.AddAnimation(new Animation(run, Animation.AnimationType.Run, 1f / 4f,true));

            // Jump
            List<Texture2D> jump = new List<Texture2D>();
            for (int i = 0; i < 13; i++)
            {
                if (i <= 9)
                    jump.Add(mainGame.Content.Load<Texture2D>("Jump_00" + i));
                else
                    jump.Add(mainGame.Content.Load<Texture2D>("Jump_0" + i));
            }
            myHero.AddAnimation(new Animation(jump, Animation.AnimationType.Jump, 1f/6f,false));

            // Sit
            List<Texture2D> sit = new List<Texture2D>();
            for (int i = 0; i < 11; i++)
            {
                if (i <= 9)
                    sit.Add(mainGame.Content.Load<Texture2D>("Sit_00" + i));
                else
                    jump.Add(mainGame.Content.Load<Texture2D>("Sit_0" + i));
            }
            myHero.AddAnimation(new Animation(sit, Animation.AnimationType.Sit, 1f / 6f,false));


            // Death
            List<Texture2D> death = new List<Texture2D>();
            for (int i = 0; i < 10; i++)
            {
                if (i <= 9)
                    death.Add(mainGame.Content.Load<Texture2D>("Death_00" + i));
                else
                    death.Add(mainGame.Content.Load<Texture2D>("Death_0" + i));
            }
            myHero.AddAnimation(new Animation(death, Animation.AnimationType.Death, 1f / 6f,false));


            // Play an animation
            myHero.PlayAnimation(Animation.AnimationType.Run);

            
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
