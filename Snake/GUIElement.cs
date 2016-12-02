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
        public readonly string WpisywanaNazwa;
        private Action _metoda;
        public SpriteFont font;
        protected int ElementHeight;
        protected int ElementWidth;
        //public delegate void KliknietyElement(string element);
        //public event KliknietyElement klawisz;
        public GUIElement(string WpisywanaNazwa,int x, int y, Action metoda, ContentManager content/*, int screenWidth, int screenHeight, */)
        {
            
            
            this.WpisywanaNazwa = WpisywanaNazwa;
            this.x = x;
            this.y = y;
            this._metoda = metoda;
            if (WpisywanaNazwa != null)
            {
                GUITexture = content.Load<Texture2D>(WpisywanaNazwa);
                ElementWidth = GUITexture.Width;
                ElementHeight = GUITexture.Height;
            }
            GUIRect = new Rectangle(0, 0, ElementWidth, ElementHeight);
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
            return new Rectangle((width / 2) - (ElementWidth / 2), (height / 2) - (ElementHeight / 2), ElementWidth, ElementHeight);
        }

        public void RuszanieEl(int x, int y)
        {
            GUIRect = new Rectangle(GUIRect.X += x, GUIRect.Y += y, GUIRect.Width, GUIRect.Height);
        }
      


    }
}