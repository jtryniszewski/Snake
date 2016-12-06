using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Storage;
using System.Threading;

namespace Snake
{
    public class GameEngine
    {
        bool IsPaused = true;
        bool IsMusic = true;
        public SpriteBatch spriteBatch;
        private TimeSpan czas;
        private Menu menu;
        private bool czyMenu;
        private ContentManager contentManager;
        public SpriteFont font;
        private Snake waz;
        private Apple jablko;
        private int ii = 0;

        //--------------------------------------------------------------------------------------------

        #region Menu Listy

        private GUIElement getGUIElement(string ButtonName, int Xpos, int Ypos, Action action)
        {
            //var screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            //var screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            return new GUIElement(ButtonName, Xpos, Ypos, action, contentManager);/*screenWidth, screenHeight*/
        }
        private GUIElement getGUITextElement(int Xpos, int Ypos, Action action, string text)
        {
            return new GUITextElement(null, Xpos, Ypos, action, contentManager, spriteBatch, text);/*screenWidth, screenHeight*/
        }


        private List<GUIElement> GetMainMenu()
        {
            return new List<GUIElement>()
            {
                getGUIElement("background", 0, 0, null),
                getGUIElement("New",-235,150,()=>ZmEnuDoGry()),
                getGUIElement("Exit",270,300,()=>Exit()),
                getGUIElement("Options",-270,300,()=>ZMenuDoOpcji()),
                getGUIElement("High",235,150,()=>ZMenuDoHigh()),
                getGUIElement("Snake1360",60,-100,null), 
            };
        }

        private List<GUIElement> GetOptionMenu()
        {
            return new List<GUIElement>()
            {
                getGUIElement("background",0,0,null),
                getGUIElement("MuzykaWl",-300,-100,()=>TurnOff()),
                getGUIElement("return",-200,-200,()=>wroc())
            };
        }
        private List<GUIElement> GetHighMenu()
        {
            return new List<GUIElement>()
            {
                getGUIElement("background",0,0,null),
                getGUIElement("return",-200,-300,()=>wroc()),
                getGUITextElement(200,200,null,"Player Name:"),
                getGUITextElement(800,200,null,"High Score:")
            };

        }
        private List<GUIElement> GetMusicOffMenu()
        {

            return new List<GUIElement>()
            {
                getGUIElement("background",0,0,null),
                getGUIElement("MuzykaWyl",-300,-100,()=>TurnOn()),
                getGUIElement("return",-200,-200,()=>wroc())
            };
        }
        private List<GUIElement> GetPauseMenu()
        {

            return new List<GUIElement>()
            {
                getGUIElement("PausedBackground",0,0,null),
                getGUIElement("resume",200,0,()=>Resume()),
                getGUIElement("mainmenu",-200,0,()=>PauseToMenu())
            };
        }
        #endregion

        //--------------------------------------------------------------------------------------------
        #region Update
        public void Update(TimeSpan czas)
        {
            this.czas = czas;
            if (czyMenu == true)
            {
                menu.Update();
            }
            else
            {
                if(IsPaused==true && Zdarzenie.CzyWSciane(waz.GlowaPolozenie) == false && Zdarzenie.CzyWSiebie(waz.Poleznia(),waz.GlowaPolozenie) == false)
                {
                    int kierunek = SnakeControl();
                    waz.Move();
                    if (waz.Kierunek != kierunek)
                    {
                        waz.ChangeDirection(kierunek);
                    }
                    //    if(ii%2==0)
                    //    {
                    //        waz.ChangeDirection(1);
                    //    }
                    //    else
                    //    {
                    //        waz.ChangeDirection(3);
                    //    }
                    //ii++;
                    //waz.Move();
                }
                if (Zdarzenie.CzyWSciane(waz.GlowaPolozenie) == true || Zdarzenie.CzyWSiebie(waz.Poleznia(),waz.GlowaPolozenie) == true)
                {
                    czyMenu = false;
                }
                if (Zdarzenie.CzyJablko(waz.GlowaPolozenie, jablko.ApplePosition) == true)
                {
                    waz.Add();
                    jablko.RandomPositionApple(waz.Poleznia());
                }
            }
        }

        public int SnakeControl()
        {
            ////1-gora 2-dol 3-prawo 4-lewo
            if (Keyboard.GetState().IsKeyDown(Keys.Up) && waz.Kierunek != 1 && waz.Kierunek != 2)
            {
                return 1;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down) && waz.Kierunek != 1 && waz.Kierunek != 2)
            {
                return 2;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Right) && waz.Kierunek != 3 && waz.Kierunek != 4)
            {
                return 3;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Left) && waz.Kierunek != 4 && waz.Kierunek != 3)
            {
                return 4;
            }
            return waz.Kierunek;
        }

        public void Draw()
        {
            if (czyMenu == true)
            {
                menu.Rysowanie(spriteBatch);
            }
            else
            {
                waz.Draw(spriteBatch);
                jablko.Draw(spriteBatch);
            }
            if (czyMenu == false)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {

                    IsPaused = false;
                }
            }
            if (IsPaused == false)
            {
                waz.Draw(spriteBatch);
                foreach (GUIElement element in GetPauseMenu())
                {
                    element.Rysowanie(spriteBatch);
                    element.Sprawdzanie();
                }
            }
        }

        #endregion

        //--------------------------------------------------------------------------------------------

        #region puaza w grze
        public void PauseToMenu()
        {
            czyMenu = true;
            IsPaused = true;
        }
        public void Resume()
        {
            czyMenu = false;
            IsPaused = true;
        }
        public void ZmEnuDoGry()
        {
            czyMenu = false;
            waz = new Snake(contentManager);
            waz.Initialize();
            jablko = new Apple(contentManager);
            jablko.Inittialize(waz.Poleznia());
        }

        public void ZMenuDoOpcji()
        {
            if (IsMusic == true)
            {
                menu = new Menu(this, GetOptionMenu());
            }
            if(IsMusic == false)
            {
                menu = new Menu(this, GetMusicOffMenu());
            }
        }

        public void Exit()
        {
            Environment.Exit(1);
        }
        public void TurnOn()
        {
            Thread.Sleep(200);
            MediaPlayer.Resume();
            IsMusic = true;
            menu = new Menu(this, GetOptionMenu());
        }
        public void TurnOff()
        {
            Thread.Sleep(200);
            MediaPlayer.Pause();
            IsMusic = false;
            menu = new Menu(this, GetMusicOffMenu());
        }

        public void ZMenuDoHigh()
        {
            menu = new Menu(this, GetHighMenu());
        }
        public void wroc()
        {
            menu = new Menu(this, GetMainMenu());
        }

        #endregion

        //--------------------------------------------------------------------------------------------

        #region Initalize GameEngine
        //-------------------------------------------------------------------
        public void Initialize(ContentManager content,SpriteBatch spriteBatch)
        {
            contentManager = content;
            czyMenu = true;
            menu = new Menu(this, GetMainMenu());
            this.spriteBatch = spriteBatch;
        }
        //---------------------------------------------------------------------

        #endregion
    }
}
