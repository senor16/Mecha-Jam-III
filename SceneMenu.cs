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
    class SceneMenu : Scene
    {
        KeyboardState oldKBState;        
        private Button MyButton;
        private Song music;
        private Texture2D backgrouond;


        public SceneMenu(MainGame pGame) : base(pGame)
        {
            Debug.WriteLine("New SceneMenu");
        }

        public void onClickPlay(Button pSender)
        {
            mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay);
        }

        public override void Load()
        {            
            music = mainGame.Content.Load<Song>("cool");
            MediaPlayer.Play(music);
            MediaPlayer.IsRepeating = true;

            Rectangle Screen = mainGame.Window.ClientBounds;
            MyButton = new Button(mainGame.Content.Load<Texture2D>("button"));
            MyButton.Position = new Vector2(
                (Screen.Width / 2) - MyButton.Texture.Width / 2,
                (Screen.Height / 2) - MyButton.Texture.Height / 2
                );

            MyButton.onClick = onClickPlay;

            listActors.Add(MyButton);

            oldKBState = Keyboard.GetState();
            backgrouond = mainGame.Content.Load<Texture2D>("sky");

            base.Load();
        }

        public override void UnLoad()
        {
            
            MediaPlayer.Stop();
            base.UnLoad();
        }

        public override void Update(GameTime gameTime)
        {
            KeyboardState newKBState = Keyboard.GetState();

            MouseState newMState = Mouse.GetState();
            if (newMState.LeftButton == ButtonState.Pressed)
            {

            }

            if ((newKBState.IsKeyDown(Keys.Enter) == true &&
                oldKBState.IsKeyDown(Keys.Enter) == false))
            {
                mainGame.gameState.ChangeScene(GameState.SceneType.Gameplay);
            }

            oldKBState = newKBState;
            

            base.Update(gameTime);
        }

        public override void Draw(GameTime gameTime)
        {
            mainGame.spriteBatch.DrawString(AssetManager.MainFont,
                "MechaJump", new Vector2(200, 100), Color.White);
            mainGame.spriteBatch.Draw(backgrouond, Vector2.Zero, Color.White);
            base.Draw(gameTime);
        }
    }
}
