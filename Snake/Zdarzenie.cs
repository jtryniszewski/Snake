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
        static public bool CzyWSiebie(List<Vector2> polozenia)
        {
            if (polozenia.Count == 2)
            {
                return false;
            }
            else
            {
                for(int i = polozenia.Count-1;i>1;i--)
                {
                    if(polozenia[i].Y==polozenia[i-1].Y)
                    {
                        #region Y==Y
                        if(polozenia[i].X<polozenia[i-1].X)
                        {
                            if (polozenia[0].Y >= polozenia[i].Y - 64 && polozenia[0].Y <= polozenia[i].Y + 64 && polozenia[0].X >= polozenia[i].X && polozenia[0].X <= polozenia[i - 1].X)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (polozenia[0].Y >= polozenia[i].Y - 64 && polozenia[0].Y <= polozenia[i].Y + 64 && polozenia[0].X >= polozenia[i-1].X && polozenia[0].X <= polozenia[i].X)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        if(polozenia[i].Y<polozenia[i-1].Y)
                        {
                            if (polozenia[0].Y >= polozenia[i].Y - 64 && polozenia[0].Y <= polozenia[i-1].Y + 64 && polozenia[0].X >= polozenia[i].X && polozenia[0].X <= polozenia[i].X)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            if (polozenia[0].Y >= polozenia[i - 1].Y - 64 && polozenia[0].Y <= polozenia[i].Y + 64 && polozenia[0].X >= polozenia[i].X && polozenia[0].X <= polozenia[i].X)
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
            }
            return false;
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
