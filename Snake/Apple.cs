using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework;

namespace Snake
{
    class Apple
    {
        #region Pola klasy
        private Vector2 polozenieJablka;
        private Texture2D Jablko;
        private ContentManager content;
        private Random random;
        #endregion

        //-----------------------------------------------------------

        public Apple(ContentManager content)
        {
            this.content = content;
        }

        //----------------------------------------------------------

        public void Inittialize(List<Vector2> polozenieWeza)
        {
            Jablko = content.Load<Texture2D>("jablko");
            polozenieJablka = new Vector2();
            random = new Random();
            RandomPositionApple(polozenieWeza);
        }

        //-----------------------------------------------------------

        private bool Sprawdzenie(Vector2 first, Vector2 second, Vector2 check)
        {
            if(first.Y == second.Y)
            {
                if(Math.Abs(first.Y - check.Y )> 64)
                {
                    if(first.X<second.X)
                    {
                        for(int i = (int)first.X-64;i<=second.X +64;i++)
                        {
                            if(i==check.X)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        for (int i = (int)second.X - 64; i <= first.X + 64; i++)
                        {
                            if (i == check.X)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if(Math.Abs(first.X-check.X)>64)
                {
                    if(first.Y<second.Y)
                    {
                        for(int i = (int)first.X-64;i<=second.X+64;i++)
                        {
                            if(i==check.X)
                            {
                                return false;
                            }
                        }
                        return true;
                    }
                    else
                    {
                        for (int i = (int)second.X - 64; i <= first.X + 64; i++)
                        {
                            if (i == check.X)
                            {
                                return false;
                            }
                        }
                        return true;
                    }                     
                }
                else
                {
                    return false;
                }
            }
        }

        //------------------------------------------------------------
        
        public void RandomPositionApple(List<Vector2> polozenieWeza)
        {
            polozenieJablka.X = random.Next(0, 1296);
            polozenieJablka.Y = random.Next(0, 704);
            for(int i = polozenieWeza.Count-1;i>0;i--)
            {
                if(Sprawdzenie(polozenieWeza[i],polozenieWeza[i-1],polozenieJablka) == false)
                {
                    RandomPositionApple(polozenieWeza);
                }
            }
        }

        //-------------------------------------------------------------

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Jablko, polozenieJablka, Color.White);
        }

        //-------------------------------------------------------------

        public Vector2 ApplePosition
        {
            get
            {
                return polozenieJablka;
            }
        }
    }
}
