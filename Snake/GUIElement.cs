using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace Snake
{
    class GUIElement
    {
        protected int x, y;
        private Texture2D GUITexture;
        private Rectangle GUIRect;
        public readonly string wpisywanaNazwa;
        private Action _metoda;
        public SpriteFont font;
        protected int elementHeight;
        protected int elementWidth;
        //public delegate void KliknietyElement(string element);
        //public event KliknietyElement klawisz;
        public GUIElement(string wpisywanaNazwa,int x, int y, Action metoda, ContentManager content/*, int screenWidth, int screenHeight, */)
        {
            
            
            this.wpisywanaNazwa = wpisywanaNazwa;
            this.x = x;
            this.y = y;
            this._metoda = metoda;
            if (wpisywanaNazwa != null)
            {
                GUITexture = content.Load<Texture2D>(wpisywanaNazwa);
                elementWidth = GUITexture.Width;
                elementHeight = GUITexture.Height;
            }
            GUIRect = new Rectangle(0, 0, elementWidth, elementHeight);
            Centrowanie(760, 1360);
            //ScaleToFitScreen(screenHeight, screenWidth);
            RuszanieEl(x, y);
        }

        public void Sprawdzanie()
        {
            if (GUIRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                if (_metoda != null)
                    _metoda();
            }
        }

        public virtual void Rysowanie(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(GUITexture, GUIRect, Color.White);
        }

        public void Centrowanie(int height, int width)
        {
            GUIRect = GetRect(height, width);
        }

        private Rectangle GetRect(int height, int width)
        {
            return new Rectangle((width / 2) - (elementWidth / 2), (height / 2) - (elementHeight / 2), elementWidth, elementHeight);
        }

        public void RuszanieEl(int x, int y)
        {
            GUIRect = new Rectangle(GUIRect.X += x, GUIRect.Y += y, GUIRect.Width, GUIRect.Height);
        }
      


    }
}