using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mecha_Jam_III
{
    class AssetManager
    {
        public static SpriteFont MainFont { get; private set; }
        public static Song MusicGameplay { get; private set; }

        public static void Load(ContentManager pContent)
        {
            MainFont = pContent.Load<SpriteFont>("mainfont");
            MusicGameplay = pContent.Load<Song>("techno");
        }
    }
}
