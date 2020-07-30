using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace baseball
{
    class Score
    {
        public int StirikeCount { get; set; }
        public int BallCount { get; set; }
        public int OutCount { get; set; }
        public static int PlayerScore { get; set; }
        public static int CpuScore { get; set; }


        static Score()
        {
            Score.PlayerScore = 0;
            Score.CpuScore = 0;
        }

        public void strikeZone()
        {
            Console.WriteLine("1  2  3\n4  5  6\n7  8  9");
        }

        public void loading()
        {
            for (int i = 1; i<=3; i++)
            {
                Console.WriteLine(".");
                Thread.Sleep(1000);
            }
            Console.Clear();
        }

        public void scoreView()
        {
            Console.WriteLine("{0}-{1}", Score.PlayerScore, Score.CpuScore);
            Thread.Sleep(3000);
        }
    }

}
