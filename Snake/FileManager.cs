using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake
{
    public class FileManager
    {
        public static void SaveScoreToFile(List<HighScore> HighList, string text)
        {
            string line;
            string[] posplicie;
            string tmp;
            StreamReader sr = new StreamReader("HighScore.txt");
            HighList.Clear();
            for (int i = 0; i < 5; i++)
            {
                line = sr.ReadLine().ToString();
                posplicie = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                HighList.Add(new HighScore { nick = posplicie[0].ToString(), points = Int32.Parse(posplicie[1]) });
            }
            sr.Close();
            StreamWriter sw = new StreamWriter("HighScore.txt");
            HighList.RemoveAt(4);
            HighList.Add(new HighScore { nick = text, points = GameEngine.score });
            HighList = HighList.OrderByDescending(z => z.points).ToList();
            HighList = HighList.OrderByDescending(z => z.points).ToList();
            foreach (HighScore element in HighList)
            {
                int y = 28 - (element.nick.Length + element.points.ToString().Length);
                tmp = element.nick.PadRight(y) + element.points.ToString();
                sw.WriteLine(tmp);
            }
            sw.Close();
        }

    }
}
