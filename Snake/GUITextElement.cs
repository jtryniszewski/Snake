using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    class GUITextElement : GUIElement
    {


        //w konstruktorze referencja do listy
        protected string text;
        public GUITextElement(string WpisywanaNazwa, int x, int y, Action metoda, ContentManager content, SpriteBatch spriteBatch, string text) : base(WpisywanaNazwa, x, y, metoda, content)
        {
            this.text = text;
            this.font = content.Load<SpriteFont>("HighScore");
        }
        public void pisanie(SpriteBatch spriteBatch, string text)
        {
            pisanie(spriteBatch, text);
        }
        public override void Rysowanie(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, new Vector2(x, y), Color.White);
        }
    }
}
