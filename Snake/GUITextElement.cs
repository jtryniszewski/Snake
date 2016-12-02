using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class GUITextElement : GUIElement
    {
        private string text;
        public GUITextElement(string WpisywanaNazwa, int x, int y, Action metoda, ContentManager content, SpriteBatch spriteBatch, string text,int elementheight=100,int elementwidth=100) : base(WpisywanaNazwa, x, y, metoda, content)
        {
            this.text = text;
            this.ElementHeight = elementheight;
            this.ElementWidth = elementwidth;
            this.font = content.Load<SpriteFont>("HighScore");
        }
        public void pisanie(SpriteBatch spriteBatch, string tekst)
        {
            spriteBatch.DrawString(font, tekst, new Vector2(x, y), Color.White);
        }
        public override void Rysowanie(SpriteBatch spriteBatch)
        {
            pisanie(spriteBatch, text);
        }

    }
}
