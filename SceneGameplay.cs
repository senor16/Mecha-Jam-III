using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace Mecha_Jam_III
{
    
    class Hero : Sprite
    {
        const int GROUND = 430;
        const int JUMP_HEIGHT = 100;
        public bool isJumping;
        public Hero(Texture2D pTexture) : base(pTexture)
        {

            isJumping = false;
        }

        public void Jump()
        {
            isJumping = true;
            vy = -3;
        }

        public override void Update(GameTime pGameTime)
        {
            float pos = Position.Y + currentAnimation.frames[0].Height;
            
            if (GROUND - JUMP_HEIGHT > pos)
            {
             
                vy += 1;

            }

            if (pos > GROUND)
            {
                Position = new Vector2(Position.X, GROUND - currentAnimation.frames[0].Height);
                isJumping = false;
                vy = 0;
            }
            base.Update(pGameTime);
        }

        public override void TouchedBy(IActor pBy)
        {

        }
    }

    class SceneGameplay : Scene
    {
        const int GROUND = 430;
        private Paralax paralax;
        private Hero myHero;
        private KeyboardState OldKBState;
        private KeyboardState NewKBState;
        public SceneGameplay(MainGame pGame) : base(pGame)
        {
            paralax = new Paralax(mainGame);
            OldKBState = Keyboard.GetState();
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
            myHero = new Hero(mainGame.Content.Load<Texture2D>("Run_000"));
            myHero.Position = new Vector2(100, GROUND - myHero.Texture.Height);
            Debug.WriteLine("Hero position x: "+ myHero.Position.X + ", y : "+  myHero.Position.Y);
            
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
            for (int i = 0; i < 8; i++)
            {
                if (i <= 9)
                    jump.Add(mainGame.Content.Load<Texture2D>("Jump_00" + i));
                else
                    jump.Add(mainGame.Content.Load<Texture2D>("Jump_0" + i));
            }
            myHero.AddAnimation(new Animation(jump, Animation.AnimationType.Jump, 1f/4f,false));
            


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
            myHero.PlayAnimation(Animation.AnimationType.ToRun);
            
            
            base.Load();
        }

        public override void UnLoad()
        {
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            // Paralax
            paralax.Update();

            // Keyboard input
            NewKBState = Keyboard.GetState();
            // Jump
            if (!OldKBState.IsKeyDown(Keys.Up) && NewKBState.IsKeyDown(Keys.Up) && !myHero.isJumping)
            {
                myHero.PlayAnimation(Animation.AnimationType.Jump);
                myHero.Jump();                
            }

            


            if (myHero.currentAnimation.ended)
            {
                myHero.PlayAnimation(Animation.AnimationType.Run);
                
            }
            Debug.WriteLine("Hero vy: " + myHero.vy);
            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            paralax.Draw();
            base.Draw(gameTime);
        }

        
    }
}
