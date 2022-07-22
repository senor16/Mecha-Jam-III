using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Mecha_Jam_III
{

    class Obstacle : Sprite
    {
        public int blocks;
        public Obstacle(Texture2D pTexture, int pBlocks) : base(pTexture)
        {
            blocks = pBlocks;
            vx = -6;
        }

        public Vector2 getDimens()
        {
            return new Vector2(Texture.Width, Texture.Height * blocks / 2);
        }

        public override void Draw(SpriteBatch pSpriteBatch)
        {
            for (int i = 1; i <= blocks; i++)
            {
                pSpriteBatch.Draw(Texture, new Vector2(Position.X, MainGame.GROUND - (i * Texture.Height / 2)), Color.White);
            }

        }

    }

    class Hero : Sprite
    {
        public bool dead;
        public bool isJumping;
        public Hero(Texture2D pTexture) : base(pTexture)
        {
            dead = false;
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

            if (MainGame.GROUND - MainGame.JUMP_HEIGHT > pos)
            {

                vy += 1;

            }

            if (pos > MainGame.GROUND)
            {
                Position = new Vector2(Position.X, MainGame.GROUND - currentAnimation.frames[0].Height);
                isJumping = false;
                vy = 0;
            }
            base.Update(pGameTime);
        }

        public override void TouchedBy(IActor pBy)
        {
            if (pBy is Obstacle)
            {
                PlayAnimation(Animation.AnimationType.Death);
                Move(-50, 0);
                dead = true;
            }

        }
    }

    class SceneGameplay : Scene
    {

        private Paralax paralax;
        private Hero myHero;
        private Song music;
        private SoundEffect hit;

        private int distance;

        private KeyboardState OldKBState;
        private KeyboardState NewKBState;
        public SceneGameplay(MainGame pGame) : base(pGame)
        {
            paralax = new Paralax(mainGame);
            distance = 0;
            OldKBState = Keyboard.GetState();

        }


        public override void Load()
        {
            // Music
            music = mainGame.Content.Load<Song>("techno");
            hit = mainGame.Content.Load<SoundEffect>("explode");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;

            // Paralax setup
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("sky"), 0));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("sun"), 1));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("ruins"), 2));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("houses1"), 3));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("house3"), 4));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("houses2"), 5));
            paralax.AddBackground(new Background(mainGame.Content.Load<Texture2D>("road"), 6));

            // Hero Creation
            myHero = new Hero(mainGame.Content.Load<Texture2D>("Run_000"));
            myHero.Position = new Vector2(150, MainGame.GROUND - myHero.Texture.Height);
            listActors.Add(myHero);

            // Obstacles Creation
            for (int i = 0; i < 2; i++)
            {
                AddObstacle();

            }
            

            /// Adding animation
            // Turn to run
            List<Texture2D> turnToRight = new List<Texture2D>();
            for (int i = 0; i < 4; i++)
                turnToRight.Add(mainGame.Content.Load<Texture2D>("Turn_to_run_00" + i));
            myHero.AddAnimation(new Animation(turnToRight, Animation.AnimationType.ToRun, 1f / 8f, false));

            // Run
            List<Texture2D> run = new List<Texture2D>();
            for (int i = 0; i < 12; i++)
            {
                if (i <= 9)
                    run.Add(mainGame.Content.Load<Texture2D>("Run_00" + i));
                else
                    run.Add(mainGame.Content.Load<Texture2D>("Run_0" + i));
            }
            myHero.AddAnimation(new Animation(run, Animation.AnimationType.Run, 1f / 4f, true));

            // Jump
            List<Texture2D> jump = new List<Texture2D>();
            for (int i = 0; i < 8; i++)
            {
                jump.Add(mainGame.Content.Load<Texture2D>("Jump_00" + i));
            }
            myHero.AddAnimation(new Animation(jump, Animation.AnimationType.Jump, 1f / 4f, false));

            // Death
            List<Texture2D> death = new List<Texture2D>();
            for (int i = 0; i < 10; i++)
            {
                if (i <= 9)
                    death.Add(mainGame.Content.Load<Texture2D>("Death_00" + i));
                else
                    death.Add(mainGame.Content.Load<Texture2D>("Death_0" + i));
            }
            myHero.AddAnimation(new Animation(death, Animation.AnimationType.Death, 1f / 6f, false));

            // Play an animation
            myHero.PlayAnimation(Animation.AnimationType.ToRun);


            base.Load();
        }

        public override void UnLoad()
        {
            MediaPlayer.Stop();
            base.UnLoad();
        }

        private void AddObstacle()
        {
            int blocks = Util.GetInt(4, 8);
            Obstacle myObstacle = new Obstacle(mainGame.Content.Load<Texture2D>("body_top_0002"), blocks);
            int posX = 900 + Util.GetInt(2, 4) * 400;
            int posY = MainGame.GROUND - (myObstacle.Texture.Height / 2) * blocks;
            myObstacle.Position = new Vector2(posX, posY);
            listActors.Add(myObstacle);
        }

        public override void Update(GameTime gameTime)
        {
            if (myHero.dead == false)
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
                distance += 6;

                // Obstacles
                for (int i = 0; i < listActors.Count; i++)
                {
                    IActor actor = listActors[i];
                    if (actor is Obstacle)
                    {
                        if (Util.CollideHero((Obstacle)actor, myHero))
                        {
                            myHero.TouchedBy(actor);
                            hit.Play();
                        }

                        if (actor.Position.X < -100)
                        {
                            AddObstacle();
                            listActors.Remove(actor);
                        }                        
                    }
                }
                base.Update(gameTime);
            }
            else
            {
                myHero.Update(gameTime);
                if (myHero.currentAnimation.ended)
                {
                    mainGame.gameState.ChangeScene(GameState.SceneType.Gameover);
                }
            }
        }

        public override void Draw(GameTime gameTime)
        {
            paralax.Draw();
            mainGame.spriteBatch.DrawString(AssetManager.MainFont, "Distance : " + distance + " m", new Vector2(10, 10), Color.White);
            base.Draw(gameTime);
        }
    }
}
