using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework.Storage;
using System.Text.RegularExpressions;


namespace Snake
{

    class GUIHighScore : GUITextElement
    {

        public static bool CzyEnter = false;
        public static bool CzyEnter2 = false;
        public InputHelper helper = new InputHelper();
        HighScore high = new HighScore();
        public List<HighScore> HighList = new List<HighScore>();
        public GUIHighScore(string WpisywanaNazwa, int x, int y, Action metoda, ContentManager content, SpriteBatch spriteBatch, string text) : base(WpisywanaNazwa, x, y, metoda, content, spriteBatch, text)
        {
            this._metoda = metoda;
            RuszanieEl(-395, -240);

        }
        public GUIHighScore(string WpisywanaNazwa, int x, int y, Action metoda, ContentManager content, SpriteBatch spriteBatch, string text, List<HighScore> HighList) : this(WpisywanaNazwa, x, y, metoda, content, spriteBatch, text)
        {
            this.HighList = HighList;
        }
        public override void Rysowanie(SpriteBatch spriteBatch)
        {
            base.Rysowanie(spriteBatch);
            spriteBatch.Draw(GUITexture, GUIRect, Color.White);
        }
        public override void Sprawdzanie()
        {

            helper.Update();
            if (GUIRect.Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y)) && Mouse.GetState().LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                if (_metoda != null)
                    _metoda();
            }
            if (CzyEnter == true && text.Length > 0)
            {

                CzyEnter2 = true;
            }
            else
            {
                CzyEnter = false;
            }
            var zmienna = Keyboard.GetState().GetPressedKeys().FirstOrDefault();
            if (zmienna != Keys.None && Regex.IsMatch(zmienna.ToString(), @"[a-zA-Z]"))
            {
                if (helper.IsNewPress(zmienna))
                {
                    if (zmienna == Keys.Back)
                    {
                        if (text.Length > 0)
                        {
                            text = text.Remove(text.Length - 1);
                        }
                    }
                    else if (zmienna == Keys.Enter)
                    {
                        if (text.Length > 0)
                        {
                            CzyEnter2 = true;
                        }
                    }
                    else if (text.Length < 8 && zmienna.ToString().Length == 1)
                    {
                        text += zmienna.ToString();
                    }
                }
            }
            if (CzyEnter2 == true && text.Length > 0)
            {
                text.ToString();
                FileManager.SaveScoreToFile(HighList, text);

            }
        }
    }
}





