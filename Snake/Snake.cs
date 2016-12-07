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
        
        //-----------------------------------------------------------------------------------------------

        public int Kierunek
        {
            get
            {
                return kierunek;
            }
        }

        //-----------------------------------------------------------------------------------------------

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

        //-----------------------------------------------------------------------------------------------

        public Vector2 GlowaPolozenie
        {
            get
            {
                return polozenieGlowy;
            }
        }

        //-----------------------------------------------------------------------------------------------

        public Snake(ContentManager content)//konstruktor pobiera tylko content
        {
            this.content = content;
        }
        //-----------------------------------------------------------------------------------------------

        public void Initialize()//przygotowanie klasy do rozpoczecia gry
        {
            head = content.Load<Texture2D>("glowa");
            ogon = content.Load<Texture2D>("ogon");
            srodek = content.Load<Texture2D>("srodek");
            zakret = content.Load<Texture2D>("zakret1");
            polozenieZakretow = new List<Vector2>();
            rotacjaGlowy = 0f;
            rotacjaOgona = 0f;
            obrotGlowy = new Vector2(0f, 0f);
            obrotOgona = new Vector2(0f, 0f);
            polozenieGlowy = new Vector2(680, 352);
            polozenieOgona = new Vector2(552, 352);
            kierunek = 3;
            kierunekOgona = kierunek;
        }

        //-----------------------------------------------------------------------------------------------

        private void DrawMiddleRightLeft(float first, float second, float y, SpriteBatch spriteBatch)
        {
            for (int i = (int)first; i < second; i+=64)
            {
                spriteBatch.Draw(srodek, new Vector2(i, y), Color.White);
            }
        }

        //-----------------------------------------------------------------------------------------------

        private void DrawMiddleUpDown(float first, float second, float x, SpriteBatch spriteBatch)
        {
            for (int i = (int)first; i < second; i+=64)
            {
                spriteBatch.Draw(srodek, new Vector2(x, i), null, Color.White, 1.5f * Pi, new Vector2(64,0), 1f, SpriteEffects.None, 0f);
            }
        }

        //-----------------------------------------------------------------------------------------------

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

        //-----------------------------------------------------------------------------------------------

        private void DrawTurns(Vector2 first, Vector2 second, Vector2 third, SpriteBatch spriteBatch)
        {
            if (first.Y == second.Y)
            {
                if (first.X < second.X)
                {
                    if (second.Y > third.Y)
                    {
                        spriteBatch.Draw(zakret, second, null, Color.White, 1f * Pi, new Vector2(64, 64), 1f, SpriteEffects.None, 0f);//tick
                    }
                    else
                    {
                        spriteBatch.Draw(zakret, second, null, Color.White, 0.5f * Pi, new Vector2(0, 64), 1f, SpriteEffects.None, 0f);//tick
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

        //-----------------------------------------------------------------------------------------------

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

        //------------------------------------------------------------------------------------------------

        private bool HeadMove(int distance)//przemieszcza glowe o dany dystans
        {
            switch (kierunek)
            {
                case 1:
                    if (polozenieGlowy.Y - 64 < 32) { return true; }
                    if (CzyWSiebie(polozenieGlowy) == true) { return true; }
                    rotacjaGlowy = 1.5f * Pi;
                    obrotGlowy = new Vector2(64, 0);
                    polozenieGlowy.Y -= distance;
                    break;
                case 2:
                    if (polozenieGlowy.Y + 64 > 672) { return true; }
                    if (CzyWSiebie(polozenieGlowy) == true) { return true; }
                    rotacjaGlowy = 0.5f * Pi;
                    obrotGlowy = new Vector2(0, 64);
                    polozenieGlowy.Y += distance;
                    break;
                case 3:
                    if (polozenieGlowy.X + 64 > 1256) { return true; }
                    if (CzyWSiebie(polozenieGlowy) == true) { return true; }
                    rotacjaGlowy = 0f;
                    obrotGlowy = new Vector2(0, 0);
                    polozenieGlowy.X += distance;
                    break;
                case 4:
                    if (polozenieGlowy.X - 64 < 40) { return true; }
                    if (CzyWSiebie(polozenieGlowy) == true) { return true; }
                    rotacjaGlowy = Pi;
                    obrotGlowy = new Vector2(64, 64);
                    polozenieGlowy.X -= distance;
                    break;
            }
            return false;
        }

        //------------------------------------------------------------------------------------------------

        private void TailMove(int distance)//przemieszcza ogona o dany dystans
        {
            if(polozenieZakretow.Count == 0)
            {
                switch (kierunekOgona)
                {
                    case 1:
                        rotacjaOgona = 1.5f * Pi;
                        obrotOgona = new Vector2(64, 0);
                        polozenieOgona.Y -= distance;
                        break;
                    case 2:
                        rotacjaOgona = 0.5f * Pi;
                        obrotOgona = new Vector2(0, 64);
                        polozenieOgona.Y += distance;
                        break;
                    case 3:
                        rotacjaOgona = 0f;
                        obrotOgona = new Vector2(0, 0);
                        polozenieOgona.X += distance;
                        break;
                    case 4:
                        rotacjaOgona = Pi;
                        obrotOgona = new Vector2(64, 64);
                        polozenieOgona.X -= distance;
                        break;
                }
            }
            else
            {
                #region przemieszczania ogona po zakretach
                switch (kierunekOgona)
                {
                    case 1:
                        #region 
                        if (polozenieZakretow[polozenieZakretow.Count - 1].Y >= polozenieOgona.Y - 64)
                        {
                            polozenieOgona = polozenieZakretow[polozenieZakretow.Count - 1];
                            polozenieZakretow.RemoveAt(polozenieZakretow.Count - 1);
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
                            polozenieOgona.Y -= 64;
                        }
                        #endregion
                        break;
                    case 2:
                        #region 
                        if (polozenieZakretow[polozenieZakretow.Count - 1].Y <= polozenieOgona.Y + 64)
                        {
                            polozenieOgona = polozenieZakretow[polozenieZakretow.Count - 1];
                            polozenieZakretow.RemoveAt(polozenieZakretow.Count - 1);
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
                            polozenieOgona.Y += 64;
                        }
                        #endregion
                        break;
                    case 3:
                        #region 
                        if (polozenieZakretow[polozenieZakretow.Count - 1].X <= polozenieOgona.X + 64)
                        {
                            polozenieOgona = polozenieZakretow[polozenieZakretow.Count - 1];
                            polozenieZakretow.RemoveAt(polozenieZakretow.Count - 1);
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
                            polozenieOgona.X += 64;
                        }
                        #endregion
                        break;
                    case 4:
                        #region 
                        if (polozenieZakretow[polozenieZakretow.Count - 1].X >= polozenieOgona.X - 64)
                        {
                            polozenieOgona = polozenieZakretow[polozenieZakretow.Count - 1];
                            polozenieZakretow.RemoveAt(polozenieZakretow.Count - 1);
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
                            polozenieOgona.X -= 64;
                        }
                        #endregion
                        break;
                }
                #endregion
            }
        }

        //------------------------------------------------------------------------------------------------

        public bool Move()
        {
            bool zwroc = HeadMove(64);
            if(zwroc == false)
            {
                TailMove(64);
            }
            return zwroc;
        }


        //-------------------------------------------------------------------------------------------------

        public bool ChangeDirection(int kierunek)
        {
            Vector2 temp = polozenieGlowy;
            this.kierunek = kierunek;
            bool zwroc = HeadMove(64);
            if(zwroc == false)
            {
                polozenieZakretow.Insert(0, temp);
                TailMove(64);
            }
            return zwroc;
        }

        //-------------------------------------------------------------------------------------------------

        public void Add()
        {
            switch(kierunekOgona)
            {
                case 1:
                    polozenieOgona.Y += 64;
                    break;
                case 2:
                    polozenieOgona.Y -= 64;
                    break;
                case 3:
                    polozenieOgona.X -= 64;
                    break;
                case 4:
                    polozenieOgona.X += 64;
                    break;
            }
        }

        //-------------------------------------------------------------------------------------------------

        private bool CzyWSiebie(Vector2 glowa)
        {
            switch (kierunek)
            {
                case 1:
                    glowa.Y -= 64;
                    break;
                case 2:
                    glowa.Y += 64;
                    break;
                case 3:
                    glowa.X += 64;
                    break;
                case 4:
                    glowa.X -= 64;
                    break;
            }
            if (polozenieZakretow.Count < 3)
            {
                return false;
            }
            else
            {
                for (int i = polozenieZakretow.Count - 1; i > 0; i--)
                {
                    if (i == polozenieZakretow.Count - 1)
                    {
                        if (polozenieOgona.X == polozenieZakretow[i].X && polozenieOgona.X == glowa.X)
                        {
                            if ((((polozenieOgona.Y + 30) - glowa.Y) > 0 && (polozenieZakretow[i].Y - (glowa.Y + 30)) < 0) || ((polozenieOgona.Y - (glowa.Y + 30)) < 0 && ((polozenieZakretow[i].Y + 30) - glowa.Y) > 0)) { return true; }
                        }
                        if (polozenieOgona.Y == polozenieZakretow[i].Y && polozenieOgona.Y == glowa.Y)
                        {
                            if ((((polozenieOgona.X + 30) - glowa.X) > 0 && (polozenieZakretow[i].X - (glowa.X + 30)) < 0) || ((polozenieOgona.X - (glowa.Y + 30)) < 0 && ((polozenieZakretow[i].X + 30) - glowa.X) > 0)) { return true; }
                        }
                    }
                    else
                    {
                        if (polozenieZakretow[i + 1].X == polozenieZakretow[i].X && polozenieZakretow[i + 1].X == glowa.X)
                        {
                            if ((((polozenieZakretow[i + 1].Y + 30) - glowa.Y) > 0 && (polozenieZakretow[i].Y - (glowa.Y + 30)) < 0) || ((polozenieZakretow[i + 1].Y - (glowa.Y + 30)) < 0 && ((polozenieZakretow[i].Y + 30) - glowa.Y) > 0)) { return true; }
                        }
                        if (polozenieZakretow[i + 1].Y == polozenieZakretow[i].Y && polozenieZakretow[i + 1].Y == glowa.Y)
                        {
                            if ((((polozenieZakretow[i + 1].X + 30) - glowa.X) > 0 && (polozenieZakretow[i].X - (glowa.X + 30)) < 0) || ((polozenieZakretow[i + 1].X - (glowa.Y + 30)) < 0 && ((polozenieZakretow[i].X + 30) - glowa.X) > 0)) { return true; }
                        }
                    }
                }
            }
            return false;
        }
    }
}
