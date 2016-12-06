using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

namespace Snake
{
    class Snake
    {
        #region pola klasy
        private ContentManager content;
        private Texture2D head;
        private Texture2D ogon;
        private Texture2D srodek;
        private Texture2D zakret;
        private Vector2 polozenieGlowy;
        private Vector2 polozenieOgona;
        private List<Vector2> polozenieZakretow;
        static public float Pi = (float)Math.PI;
        private int kierunek;////1-gora 2-dol 3-prawo 4-lewo
        private int kierunekOgona;
        private Vector2 obrotOgona;
        private float rotacjaOgona;
        private Vector2 obrotGlowy;
        private float rotacjaGlowy;
        #endregion
        
        //----------------------------------------------------------------------------

        public int Kierunek
        {
            get
            {
                return kierunek;
            }
        }

        //--------------------------------------------------------------------------

        public List<Vector2> Poleznia()
        {
            List<Vector2> zwracnie = new List<Vector2>();
            zwracnie.Add(polozenieOgona);
            for(int i = polozenieZakretow.Count -1;i>=0;i--)
            {
                zwracnie.Add(polozenieZakretow[i]);
            }
            return zwracnie;
        }

        //------------------------------------------------------------------------

        public Vector2 GlowaPolozenie
        {
            get
            {
                return polozenieGlowy;
            }
        }
        public Snake(ContentManager content)
        {
            this.content = content;
            kierunek = 3;
            kierunekOgona = kierunek;
        }
        //-----------------------------------------------------------------------

        public void Initialize()
        {
            head = content.Load<Texture2D>("glowa");
            ogon = content.Load<Texture2D>("ogon");
            srodek = content.Load<Texture2D>("srodek1");
            zakret = content.Load<Texture2D>("zakret1");
            polozenieZakretow = new List<Vector2>();
            polozenieGlowy = new Vector2(512f, 428f);
            polozenieOgona = new Vector2(384f, 428f);
            rotacjaGlowy = 0f;
            rotacjaOgona = 0f;
            obrotGlowy = new Vector2(0f, 0f);
            obrotOgona = new Vector2(0f, 0f);
        }

        //------------------------------------------------------------------------
        private void DrawMiddleRightLeft(float first, float second, float y, SpriteBatch spriteBatch)
        {
            for (int i = (int)first; i <= second; i++)
            {
                spriteBatch.Draw(srodek, new Vector2(i, y), Color.White);
            }
        }

        //-------------------------------------------------------------------------

        private void DrawMiddleUpDown(float first, float second, float x, SpriteBatch spriteBatch)
        {
            for (int i = (int)first; i <= second; i++)
            {
                spriteBatch.Draw(srodek, new Vector2(x, i), null, Color.White, 1.5f * Pi, new Vector2(), 1f, SpriteEffects.None, 0f);
            }
        }

        //-------------------------------------------------------------------------

        private void DrawBettwenCorner(Vector2 first, Vector2 second, SpriteBatch spriteBatch)
        {
            if (first.Y == second.Y)//jesli zakret i ogon sa w jednym poziomie 
            {
                if (first.X < second.X)//ogon ma kierunek w prawa strone
                {
                    DrawMiddleRightLeft(first.X + 64, second.X, first.Y, spriteBatch);
                }
                else
                {
                    DrawMiddleRightLeft(second.X + 64, first.X, first.Y, spriteBatch);
                }
            }
            else//jesli zakret i ogon sa w pionie
            {
                if (first.Y > second.Y)
                {
                    DrawMiddleUpDown(second.Y + 64, first.Y, first.X, spriteBatch);
                }
                else
                {
                    DrawMiddleUpDown(first.Y + 64, second.Y, first.X, spriteBatch);
                }
            }
        }

        //-------------------------------------------------------------------------

        private void DrawTurns(Vector2 first, Vector2 second, Vector2 third, SpriteBatch spriteBatch)
        {
            if (first.Y == second.Y)
            {
                if (first.X < second.X)
                {
                    if (second.Y > third.Y)
                    {
                        spriteBatch.Draw(zakret, second, null, Color.White, 1f * Pi, new Vector2(64,64), 1f, SpriteEffects.None, 0f);//tick
                    }
                    else
                    {
                        spriteBatch.Draw(zakret, second, null, Color.White, 0.5f * Pi, new Vector2(0,64), 1f, SpriteEffects.None, 0f);//tick
                    }
                }
                else
                {
                    if (second.Y > third.Y)
                    {
                        spriteBatch.Draw(zakret, second, null, Color.White, 1.5f * Pi, new Vector2(64, 0), 1f, SpriteEffects.None, 0f);//tick
                    }
                    else
                    {
                        spriteBatch.Draw(zakret, second, null, Color.White);//tick
                    }
                }
            }
            else
            {
                if (first.Y < second.Y)
                {
                    if (second.X < third.X)
                    {
                        spriteBatch.Draw(zakret, second, null, Color.White, 1.5f * Pi, new Vector2(64, 0), 1f, SpriteEffects.None, 0f);
                    }
                    else
                    {
                        spriteBatch.Draw(zakret, second, null, Color.White, 1f * Pi, new Vector2(64, 64), 1f, SpriteEffects.None, 0f);//tick
                    }
                }
                else
                {
                    if (second.X < third.X)
                    {
                        spriteBatch.Draw(zakret, second, null, Color.White);//tick
                    }
                    else
                    {
                        spriteBatch.Draw(zakret, second, null, Color.White, 0.5f * Pi, new Vector2(0, 64), 1f, SpriteEffects.None, 0f);//tick
                    }
                }
            }
        }

        //-------------------------------------------------------------------------

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(head, polozenieGlowy, null, Color.White, rotacjaGlowy, obrotGlowy, 1f, SpriteEffects.None, 0f);//rysowanie glowy
            spriteBatch.Draw(ogon, polozenieOgona, null, Color.White, rotacjaOgona, obrotOgona, 1f, SpriteEffects.None, 0f);//rysowanie ogona
            #region Rysowanie snake bez zakretow
            if (polozenieZakretow.Count == 0)
            {
                if (polozenieGlowy.Y == polozenieOgona.Y)
                {
                    if (polozenieGlowy.X > polozenieOgona.X)
                    {
                        DrawMiddleRightLeft(polozenieOgona.X + 64, polozenieGlowy.X, polozenieGlowy.Y, spriteBatch);
                    }
                    else
                    {
                        DrawMiddleRightLeft(polozenieGlowy.X + 64, polozenieOgona.X, polozenieGlowy.Y, spriteBatch);
                    }
                }
                else
                {
                    if (polozenieGlowy.Y < polozenieOgona.Y)
                    {
                        DrawMiddleUpDown(polozenieGlowy.Y + 64, polozenieOgona.Y, polozenieGlowy.X, spriteBatch);
                    }
                    else
                    {
                        DrawMiddleUpDown(polozenieOgona.Y + 64, polozenieGlowy.Y, polozenieGlowy.X, spriteBatch);
                    }
                }
            }
            #endregion
            #region rysowanie snaka z zakretami
            else
            {
                for (int i = polozenieZakretow.Count - 1; i >= 0; i--)
                {
                    if (i == polozenieZakretow.Count - 1)
                    {
                        DrawBettwenCorner(polozenieOgona, polozenieZakretow[i], spriteBatch);
                        if (i - 1 < 0)
                        {
                            DrawTurns(polozenieOgona, polozenieZakretow[i], polozenieGlowy, spriteBatch);
                        }
                        else
                        {
                            DrawTurns(polozenieOgona, polozenieZakretow[i], polozenieZakretow[i - 1], spriteBatch);
                        }
                    }
                    else
                    {
                        DrawBettwenCorner(polozenieZakretow[i + 1], polozenieZakretow[i], spriteBatch);
                        if (i - 1 < 0)
                        {
                            DrawTurns(polozenieZakretow[i + 1], polozenieZakretow[i], polozenieGlowy, spriteBatch);
                        }
                        else
                        {
                            DrawTurns(polozenieZakretow[i + 1], polozenieZakretow[i], polozenieZakretow[i - 1], spriteBatch);
                        }
                    }
                }
                DrawBettwenCorner(polozenieZakretow[0], polozenieGlowy, spriteBatch);
            }
            #endregion
        }

        //-----------------------------------------------------------------------

        public void Move()//1-gora 2-dol 3-prawo 4-lewo
        {
            #region zmiana polozenia glowy
            switch (kierunek)
            {
                case 1:
                    rotacjaGlowy = 1.5f * Pi;
                    obrotGlowy = new Vector2(64, 0);
                    polozenieGlowy.Y--;
                    break;
                case 2:
                    rotacjaGlowy = 0.5f * Pi;
                    obrotGlowy = new Vector2(0, 64);
                    polozenieGlowy.Y++;
                    break;
                case 3:
                    rotacjaGlowy = 0f;
                    obrotGlowy = new Vector2(0, 0);
                    polozenieGlowy.X++;
                    break;
                case 4:
                    rotacjaGlowy = Pi;
                    obrotGlowy = new Vector2(64, 64);
                    polozenieGlowy.X--;
                    break;
            }
            #endregion
            #region zmiana polozenia ogona jesli nie ma zakretow
            if (polozenieZakretow.Count == 0)
            {
                switch (kierunek)
                {
                    case 1:
                        polozenieOgona.Y--;
                        break;
                    case 2:
                        polozenieOgona.Y++;
                        break;
                    case 3:
                        polozenieOgona.X++;
                        break;
                    case 4:
                        polozenieOgona.X--;
                        break;
                }
                obrotOgona = obrotGlowy;
                rotacjaOgona = rotacjaGlowy;
                kierunekOgona = kierunek;
            }
            #endregion
            #region zmiana kierunku ogona plus modifikacja listy zakretow
            else
            {
                switch (kierunekOgona)
                {
                    case 1:
                        #region 
                        if (polozenieZakretow[polozenieZakretow.Count - 1].Y == polozenieOgona.Y - 64)
                        {
                            polozenieZakretow.RemoveAt(polozenieZakretow.Count - 1);
                            polozenieOgona.Y -= 64;
                            if (polozenieZakretow.Count == 0)
                            {
                                obrotOgona = obrotGlowy;
                                rotacjaOgona = rotacjaGlowy;
                                kierunekOgona = kierunek;
                            }
                            else
                            {
                                if (polozenieZakretow[polozenieZakretow.Count - 1].X < polozenieOgona.X)//zakert po prawej stronie ogona
                                {
                                    rotacjaOgona = Pi;
                                    obrotOgona = new Vector2(64, 64);
                                    kierunekOgona = 4;
                                }
                                else                                                                 //zakret po lewej stronie ogona
                                {
                                    rotacjaOgona = 0f;
                                    obrotOgona = new Vector2(0, 0);
                                    kierunekOgona = 3;
                                }
                            }

                        }
                        else
                        {
                            polozenieOgona.Y--;
                        }
                        #endregion
                        break;
                    case 2:
                        #region 
                        if (polozenieZakretow[polozenieZakretow.Count - 1].Y == polozenieOgona.Y + 64)
                        {
                            polozenieZakretow.RemoveAt(polozenieZakretow.Count - 1);
                            polozenieOgona.Y += 64;
                            if (polozenieZakretow.Count == 0)
                            {
                                obrotOgona = obrotGlowy;
                                rotacjaOgona = rotacjaGlowy;
                                kierunekOgona = kierunek;
                            }
                            else
                            {
                                if (polozenieZakretow[polozenieZakretow.Count - 1].X < polozenieOgona.X)//zakert po prawej stronie ogona
                                {
                                    rotacjaOgona = Pi;
                                    obrotOgona = new Vector2(64, 64);
                                    kierunekOgona = 4;
                                }
                                else                                                                 //zakret po lewej stronie ogona
                                {
                                    rotacjaOgona = 0f;
                                    obrotOgona = new Vector2(0, 0);
                                    kierunekOgona = 3;
                                }
                            }

                        }
                        else
                        {
                            polozenieOgona.Y++;
                        }
                        #endregion
                        break;
                    case 3:
                        #region 
                        if (polozenieZakretow[polozenieZakretow.Count - 1].X == polozenieOgona.X + 64)
                        {
                            polozenieZakretow.RemoveAt(polozenieZakretow.Count - 1);
                            polozenieOgona.X += 64;
                            if (polozenieZakretow.Count == 0)
                            {
                                obrotOgona = obrotGlowy;
                                rotacjaOgona = rotacjaGlowy;
                                kierunekOgona = kierunek;
                            }
                            else
                            {
                                if (polozenieZakretow[polozenieZakretow.Count - 1].Y < polozenieOgona.Y)//zakert nad ogonem
                                {
                                    rotacjaOgona = 1.5f * Pi;
                                    obrotOgona = new Vector2(64, 0);
                                    kierunekOgona = 1;
                                }
                                else                                                                 //zakret pod ogonem
                                {
                                    rotacjaOgona = 0.5f * Pi;
                                    obrotOgona = new Vector2(0, 64);
                                    kierunekOgona = 2;
                                }
                            }

                        }
                        else
                        {
                            polozenieOgona.X++;
                        }
                        #endregion
                        break;
                    case 4:
                        #region 
                        if (polozenieZakretow[polozenieZakretow.Count - 1].X == polozenieOgona.X - 64)
                        {
                            polozenieZakretow.RemoveAt(polozenieZakretow.Count - 1);
                            polozenieOgona.X -= 64;
                            if (polozenieZakretow.Count == 0)
                            {
                                obrotOgona = obrotGlowy;
                                rotacjaOgona = rotacjaGlowy;
                                kierunekOgona = kierunek;
                            }
                            else
                            {
                                if (polozenieZakretow[polozenieZakretow.Count - 1].Y < polozenieOgona.Y)//zakert nad ogonem
                                {
                                    rotacjaOgona = 1.5f * Pi;
                                    obrotOgona = new Vector2(64, 0);
                                    kierunekOgona = 1;
                                }
                                else                                                                 //zakret pod ogonem
                                {
                                    rotacjaOgona = 0.5f * Pi;
                                    obrotOgona = new Vector2(0, 64);
                                    kierunekOgona = 2;
                                }
                            }

                        }
                        else
                        {
                            polozenieOgona.X--;
                        }
                        #endregion
                        break;
                }
            }
            #endregion
        }

        //-----------------------------------------------------------------------

        public void ChangeDirection(int kierunek)//1-gora 2-dol 3-prawo 4-lewo
        {
            polozenieZakretow.Insert(0, polozenieGlowy);
            switch (kierunek)
            {
                case 1:
                    rotacjaGlowy = 1.5f * Pi;
                    obrotGlowy = new Vector2(64, 0);
                    polozenieGlowy.Y -= 63;
                    break;
                case 2:
                    rotacjaGlowy = 0.5f * Pi;
                    obrotGlowy = new Vector2(0, 64);
                    polozenieGlowy.Y += 63;
                    break;
                case 3:
                    rotacjaGlowy = 0f;
                    obrotGlowy = new Vector2(0, 0);
                    polozenieGlowy.X += 63;
                    break;
                case 4:
                    rotacjaGlowy = Pi;
                    obrotGlowy = new Vector2(64, 64);
                    polozenieGlowy.X -= 63;
                    break;
            }
            this.kierunek = kierunek;
        }

        //------------------------------------------------------------------------

        public void Add()//przedluzenie snaka po zjedzeniu jablko
        {
            switch (kierunekOgona)
            {
                case 1:
                    polozenieOgona.Y += 64;
                    break;
                case 2:
                    polozenieOgona.Y-=64;
                    break;
                case 3:
                    polozenieOgona.X-=64;
                    break;
                case 4:
                    polozenieOgona.X+=64;
                    break;
            }
        }

    }
}
