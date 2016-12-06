using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Snake
{
    static class Zdarzenie
    {
        static public bool CzyWSciane(Vector2 check)
        {
            if(check.X<0)
            {
                return true;
            }
            if(check.X> 1296)
            {
                return true;
            }
            if(check.Y<0)
            {
                return true;
            }
            if(check.Y> 704)
            {
                return true;
            }
            return false;
        }
        static public bool CzyWSiebie(List<Vector2> polozenia,Vector2 glowa)
        {
            bool zwracanie = false;
            if(polozenia.Count<=3)
            {
                zwracanie = false;
            }
            else
            {
                for(int i = 0;i<polozenia.Count-2;i++)
                {
                    #region Y==Y
                    if(polozenia[i].Y == polozenia[i+1].Y)
                    {
                        if(polozenia[i].X<polozenia[i + 1].X)
                        {
                            if((glowa.Y > polozenia[i].Y - 63f && glowa.Y < polozenia[i].Y + 63f) && (glowa.X > polozenia[i].X - 63f && glowa.X < polozenia[i + 1].X + 63f))
                            {
                                zwracanie= true;
                            }
                        }
                        else
                        {
                            if ((glowa.Y > polozenia[i].Y - 63f && glowa.Y < polozenia[i].Y + 63f) && (glowa.X > polozenia[i + 1].X - 63f && glowa.X < polozenia[i].X + 63f))
                            {
                                zwracanie =true;
                            }
                        }
                    }
                    #endregion
                    #region X==X
                    else
                    {
                        if(polozenia[i].Y<polozenia[i + 1].Y)
                        {
                            if ((glowa.Y > polozenia[i].Y - 63f && glowa.Y < polozenia[i + 1].Y + 63f) && (glowa.Y > polozenia[i].Y - 63f && glowa.X < polozenia[i + 1].Y + 63f))
                            {
                                zwracanie= true;
                            }
                        }
                        else
                        {
                            if ((glowa.Y > polozenia[i + 1].Y - 63f && glowa.Y < polozenia[i].Y + 63f) && (glowa.X > polozenia[i].X - 63f && glowa.X < polozenia[i].X + 63f))
                            {
                                zwracanie = true;
                            }
                        }
                    }
                    #endregion
                }
            }
            return zwracanie;
        }
        static public bool CzyJablko(Vector2 waz, Vector2 jablko)
        {
            if(waz.X<=jablko.X+64 && waz.X>=jablko.X-64 && waz.Y>=jablko.Y-64 && waz.Y<=jablko.Y+64)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
