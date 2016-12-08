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
using System.IO;

namespace Snake
{
    public class GameEngine
    {
        bool isPause = true;
        bool isMusic = true;
        bool isGameOver = false;
        public SpriteBatch spriteBatch;
        private TimeSpan czas;
        private Menu menu;
        private bool czyMenu;
        private ContentManager contentManager;
        public SpriteFont font;
        private Snake waz;
        private Apple jablko;
        private Texture2D tlo_Gry;
        int kierunek;
        public InputHelper helper = new InputHelper();
        public static int score = 0;
        bool IsPaused = true;
        bool IsMusic = true;
        private int kierunekTeraz;
        private int kierunekNowy;
        string test;
        string testint;
        private GraphicsDeviceManager graphics;
        private Texture2D jablkoTextura;
        private StringBuilder sb;
        private List<HighScore> HighList;

        string ListLoad;

        //--------------------------------------------------------------------------------------------
        #region metody statyczne
        static public bool CzyJablko(Vector2 waz, Vector2 jablko)
        {
            if (waz.X == jablko.X && jablko.Y == waz.Y)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

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
        //private GUIElement getGUIHighScore(int Xpos, int Ypos, Action action)
        //{
        //    return new GUIHighScore(null, Xpos, Ypos, action, contentManager, spriteBatch, string.Empty, HighList);
        //}
        private GUIElement getGUIHighScore(string ButtonName, int Xpos, int Ypos, Action action)
        {
            return new GUIHighScore(ButtonName, Xpos, Ypos, action, contentManager, spriteBatch, string.Empty, HighList);
        }

        private List<GUIElement> GetGame()
        {
            return new List<GUIElement>()
            {
               getGUIElement("GameBackGround", 0, 0, null),
            };
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
                getGUITextElement(800,200,null,"Score:"),
                getGUITextElement(360,300,null,ListLoad),
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
                getGUIElement("resume",0,20,()=>Resume()),
                getGUIElement("mainmenu",0,-125,()=>PauseToMenu())
            };
        }
        private List<GUIElement> GetGameOver()
        {

            return new List<GUIElement>()
            {
                getGUIElement("gamebackground", 0, 0, null),
                getGUIElement("GameOverBack",0,0,null),
                getGUIElement("NewHigh",0,0,null),
                //getGUIElement("SAVE",0,150,()=>HSToGameOver()),
                getGUIHighScore("Save",440,375,()=>HSToGameOver())//440,375
            };
        }
        private List<GUIElement> GetOver()
        {

            return new List<GUIElement>()
            {
                getGUIElement("gamebackground", 0, 0, null),
                getGUIElement("GameOverBack",0,0,null),
                getGUIElement("return",-250,20,()=>wroc()),
                getGUIElement("retry",250,20,()=>ZmEnuDoGry()),
                getGUITextElement(750,225,null,score.ToString()),
            };
        }
        private List<GUIElement> GetScore()
        {
            return new List<GUIElement>()
            {
                getGUITextElement(10,0,null,"Score:"),
                getGUITextElement(220, 0, null, score.ToString())
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
                if(isPause==true && isGameOver == false)
                {
                    kierunek = SnakeControl();
                    if (waz.Kierunek != kierunek)
                    {
                        if (czas.Milliseconds % 1500 == 0)
                        {
                            if (waz.ChangeDirection(kierunek) == true /*|| waz.CzyWSiebie() == true*/)
                            {
                                isGameOver = true;
                            }
                        }
                    }
                    else
                    {
                        if (czas.Milliseconds % 1500 == 0)
                        {
                            if (waz.Move() == true /*|| waz.CzyWSiebie() == true*/)
                            {
                                isGameOver = true;
                            }
                        }
                    }

                }
                if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    isPause = false;
                }
                if (isGameOver == true)
                {
                    int LowestScore = HighList.Min(z => z.points);
                    if (score > LowestScore)
                    {
                        czyMenu = true;
                        menu = new Menu(this, GetGameOver());


                    }
                    else
                    {
                        czyMenu = true;
                        menu = new Menu(this, GetOver());
                    }
                }
                if (CzyJablko(waz.GlowaPolozenie, jablko.ApplePosition) == true)
                {
                    waz.Add();
                    score += 10;
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
            return kierunek;
        }

        public void Draw()
        {
            if (czyMenu == true)
            {
                menu.Rysowanie(spriteBatch);
            }
            else
            {
                foreach (GUIElement element in GetGame())
                {
                    element.Rysowanie(spriteBatch);
                }
                foreach (GUIElement element in GetScore())
                {
                    element.Rysowanie(spriteBatch);
                    element.Sprawdzanie();
                }
                waz.Draw(spriteBatch);
                jablko.Draw(spriteBatch);
            }
            if (isPause == false)
            {
                waz.Draw(spriteBatch);
                foreach (GUIElement element in GetPauseMenu())
                {
                    element.Rysowanie(spriteBatch);
                    element.Sprawdzanie();
                }
            }
            if (GUIHighScore.CzyEnter2 == true)
            {
                czyMenu = true;
                menu = new Menu(this, GetOver());
                GUIHighScore.CzyEnter2 = false;
                GUIHighScore.CzyEnter = false;

            }
        }

        #endregion

        //--------------------------------------------------------------------------------------------

        #region puaza w grze
        public void HSToGameOver()
        {

            GUIHighScore.CzyEnter = true;
        }
        public void PauseToMenu()
        {
            czyMenu = true;
            isPause = true;
        }
        public void Resume()
        {
            czyMenu = false;
            isPause = true;
        }
        public void ZmEnuDoGry()
        {
            czyMenu = false;
            waz = new Snake(contentManager);
            waz.Initialize();
            jablko = new Apple(contentManager);
            jablko.Inittialize(waz.Poleznia());
            kierunek = 3;
            isGameOver = false;
        }

        public void ZMenuDoOpcji()
        {
            if (isMusic == true)
            {
                menu = new Menu(this, GetOptionMenu());
            }
            if(isMusic == false)
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
            isMusic = true;
            menu = new Menu(this, GetOptionMenu());
        }
        public void TurnOff()
        {
            Thread.Sleep(200);
            MediaPlayer.Pause();
            isMusic = false;
            menu = new Menu(this, GetMusicOffMenu());
        }

        public void ZMenuDoHigh()
        {
            ListLoad = File.ReadAllText("HighScore.txt");
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
            HighList = new List<HighScore>();
            string path = "HighScore.txt";
            string tmp;
            string line;
            string[] posplicie;
            if (!File.Exists(path) || new FileInfo("Highscore.txt").Length == 0)
            {

                StreamWriter sw = new System.IO.StreamWriter("HighScore.txt");
                HighList.Add(new HighScore { nick = "PLAYER1", points = 0 });
                HighList.Add(new HighScore { nick = "PLAYER2", points = 2 });
                HighList.Add(new HighScore { nick = "PLAYER3", points = 5 });
                HighList.Add(new HighScore { nick = "PLAYER4", points = 7 });
                HighList.Add(new HighScore { nick = "PLAYER5", points = 15 });
                HighList = HighList.OrderByDescending(z => z.points).ToList();
                foreach (HighScore element in HighList)
                {
                    tmp = element.nick + "                  " + element.points.ToString();
                    sw.WriteLine(tmp);

                }
                sw.Close();
                //ListLoad = File.ReadAllText(path);
            }
            else
            {
                StreamReader sr = new StreamReader(path);
                //while ((line = sr.ReadLine().ToString()) != null)
                for (int i = 0; i < 5; i++)
                {
                    line = sr.ReadLine().ToString();
                    posplicie = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    HighList.Add(new HighScore { nick = posplicie[0].ToString(), points = Int32.Parse(posplicie[1]) });
                }
                sr.Close();

            }
            menu = new Menu(this, GetMainMenu());
            this.spriteBatch = spriteBatch;
            
        }
        //---------------------------------------------------------------------

        #endregion
    }
}
