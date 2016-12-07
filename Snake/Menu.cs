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
    class Menu
    {
        public List<GUIElement> main = new List<GUIElement>();
        public GameEngine silnik;

        public Menu(GameEngine silnik, List<GUIElement>elementsList)
        {
            main = elementsList;
            this.silnik = silnik;
        }
        public void Wypisywanie(ContentManager content)
        {
            foreach (GUIElement element in main)
            {
                //element.Wypisywanie(content);
                element.Centrowanie(768, 1360);
            }
        }

        public void Update()
        {
            foreach (GUIElement element in main)
            {
                element.Sprawdzanie();
            }
        }
        public void Rysowanie(SpriteBatch spriteBatch)
        {
            foreach (GUIElement element in main)
            {
                element.Rysowanie(spriteBatch);
            }
        }
        
    }
}