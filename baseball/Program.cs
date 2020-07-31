using System;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace baseball
{
    class Program
    {
        public static bool NanJ { get; set; }

        static Program()
        {
            Program.NanJ = false;
        }

        static void Main(string[] args)
        {
            Batter Playerbatter = new Batter();
            Batter CPUbatter = new Batter();
            Pitcher pitcher = new Pitcher();
            Score score = new Score();

            Console.Title = "やきうゲーム";
            //ゲーム開始の選択
            
            Console.WriteLine("やきうゲーム\n" +
                "1:開始　2:終了");

            int Start = int.Parse(Console.ReadLine());
            if (Start == 334)
            {
                NanJ = true;
            }

            while (Start == 1||Start==334)
            {

                Console.WriteLine("PlayBall!!");

                Console.WriteLine("遊ぶイニング数を入力してください");
                int Inning = int.Parse(Console.ReadLine());
                Random rnd = new Random();
                Console.WriteLine("先攻を決定します(1なら先攻)");
                int Offence = rnd.Next(1, 3); //1or2のダイスを振る。
                if (Offence == 1)
                {
                    Console.WriteLine("{0}!!あなたは先攻です。", Offence);
                }
                else
                { 
                    Console.WriteLine("{0}!!あなたは後攻です。", Offence);

                }


                //ここからゲーム開始。
                for (int NowInning=1; NowInning<=Inning; NowInning++)
                {
                    if (Offence == 1)
                    {
                        Console.WriteLine("{0}回表",NowInning);
                        Thread.Sleep(2000);

                        //ここから攻撃
                        Playerbatter.PlayerBatting();
                        Console.WriteLine(Playerbatter.BatterNumber);
                        Console.WriteLine();

                        Console.WriteLine("{0}回裏",NowInning);
                        Thread.Sleep(2000);

                        //ここから守備
                        CPUbatter.CpuBatting();
                        Console.WriteLine(CPUbatter.BatterNumber);

                    }
                    else
                    {
                        Console.WriteLine("{0}回表", NowInning);
                        Thread.Sleep(2000);

                        //ここから守備
                        CPUbatter.CpuBatting();
                        Console.WriteLine(CPUbatter.BatterNumber);


                        Console.WriteLine("{0}回裏", NowInning);
                        Thread.Sleep(2000);

                        //ここから攻撃
                        Playerbatter.PlayerBatting();
                        Console.WriteLine(Playerbatter.BatterNumber);

                    }
                }
                Console.WriteLine("試合終了！");
                score.scoreView();

                Console.WriteLine("もう一回遊ぶ？\n"+ "1:開始　2:終了");
                Start = int.Parse(Console.ReadLine());
            }
            Console.WriteLine("キー入力で終了します...");
            Console.ReadKey();
            Environment.Exit(0);

        }
    }
}
