using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Snake;

namespace Snake
{
    class Pause
    {
        public List<GUIElement> PauseMain = new List<GUIElement>();
        public GameEngine silnik;
        ContentManager content;

        public Pause(GameEngine silnik, List<GUIElement> elementsList)
        {
            PauseMain = elementsList;
            this.silnik = silnik;
        }
        public void Wypisywanie(ContentManager content)
        {
            foreach (GUIElement element in PauseMain)
            {
                //element.Wypisywanie(content);
                element.Centrowanie(768, 1360);
            }
        }

        public void Update()
        {
            foreach (GUIElement element in PauseMain)
            {
                element.Sprawdzanie();
            }
        }
        public void Rysowanie(SpriteBatch spriteBatch)
        {
            foreach (GUIElement element in PauseMain)
            {
                element.Rysowanie(spriteBatch);
            }
        }
    }
}
