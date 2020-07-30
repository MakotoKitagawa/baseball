using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace baseball
{
    class Batter
    {

        [Flags]
        public enum HitAction
        {
            OUT = 0b000,
            HIT = 0b001,
            TWO = 0b010,
            THREE = 0b011,
            HOMERUN = 0b100,
        }

        public static int MeetPoint { set; get; }
        public int BatterNumber { set; get; }

        public static HitAction[] ABatter { get; set; } = new HitAction[10]
        {
            HitAction.OUT,HitAction.HIT,HitAction.OUT,HitAction.TWO,HitAction.OUT,
            HitAction.HIT,HitAction.TWO,HitAction.HIT,HitAction.OUT,HitAction.HOMERUN
        };
        public static HitAction[] PBatter { get; set; } = new HitAction[10]
        {
            HitAction.OUT,HitAction.HIT,HitAction.TWO,HitAction.OUT,HitAction.THREE,
            HitAction.HIT,HitAction.OUT,HitAction.TWO,HitAction.OUT,HitAction.HOMERUN
        };
        public static HitAction[] LBatter { get; set; } = new HitAction[10]
        {
            HitAction.OUT,HitAction.HIT,HitAction.OUT,HitAction.HIT,HitAction.TWO,
            HitAction.OUT,HitAction.HIT,HitAction.OUT,HitAction.HIT,HitAction.THREE
        };


        //コンストラクタ
        public Batter()
        {
            this.BatterNumber = 1;
        }
        static Batter()
        {
            MeetPoint = 0;
        }
        Runner runner = new Runner();
        Pitcher pitcher = new Pitcher();
        Random rmd = new Random();
        Score score = new Score();
        NanJ nanJ = new NanJ();




        public bool HitJudge(int meetPoint)
        {
            pitcher.CpuPitching();
            bool hit = false;
            

            if (meetPoint >= 1 && meetPoint <= 3)
            {
                if (meetPoint != 3)
                {
                    if (meetPoint == pitcher.ThrowPoint || meetPoint == pitcher.ThrowPoint + 1)
                    {
                        hit = true;
                    }
                }
                else if (meetPoint == pitcher.ThrowPoint)
                {
                    hit = true;
                }


            }
            else if (meetPoint >= 4 && meetPoint <= 6)
            {
                if (meetPoint != 6)
                {
                    if (meetPoint == pitcher.ThrowPoint || meetPoint == pitcher.ThrowPoint + 1)
                    {
                        hit = true;
                    }
                }
                else if (meetPoint == pitcher.ThrowPoint)
                {
                    hit = true;
                }
            }
            else if (meetPoint >= 7 && meetPoint <= 9)
            {
                if (meetPoint != 9)
                {
                    if (meetPoint == pitcher.ThrowPoint || meetPoint == pitcher.ThrowPoint + 1)
                    {
                        hit = true;
                    }
                }
                else if (meetPoint == pitcher.ThrowPoint)
                {
                    hit = true;
                }
            }
            
            return hit;
        }

        public void CpuMeetPoint()
        {
            MeetPoint = rmd.Next(0, 10);
        }


        public void PlayerBatting()
        {
            while (score.OutCount < 3)
            {
                score.StirikeCount = 0;
                score.BallCount = 0;
                while (score.StirikeCount < 3)
                {
                    runner.RunnerAnnounce(Runner.BaseStatus);
                    Console.WriteLine("どこを打つ？");
                    score.strikeZone();
                    MeetPoint=int.Parse(Console.ReadLine());
                    score.loading();
                    if (this.HitJudge(MeetPoint) == true)
                    {
                        Console.WriteLine("HIT!!!");
                        this.PlayerHitDice();
                        this.BatterNumber++;
                        if (this.BatterNumber > 9)
                        {
                            this.BatterNumber = 1;
                        }
                        score.StirikeCount = 0;
                        score.BallCount = 0;
                        if (score.OutCount >= 3)
                        {
                            break;
                        }
                        continue;

                    }
                    else if (pitcher.ThrowPoint == 0 && MeetPoint == 0)
                    {
                        score.BallCount++;
                        Console.WriteLine("BALL!!!");
                        Console.WriteLine("B:" + score.BallCount);
                        if(score.BallCount>=4)
                        {
                            pitcher.PlayerFourBall();
                            nanJ.NanJpojiButter();
                            score.BallCount = 0;
                            score.StirikeCount = 0;
                        }

                    }
                    else
                    {
                        score.StirikeCount++;
                        Console.WriteLine("STRIKE!!!");
                        Console.WriteLine("S:" + score.StirikeCount);
                    }


                }
                score.OutCount++;
                Console.WriteLine("BatterOUT!!");
                nanJ.NanJnegaButter();
                this.BatterNumber++;
                if (this.BatterNumber > 9)
                {

                    this.BatterNumber = 1;
                }

                Console.WriteLine("O:" + score.OutCount);
            }
            score.OutCount = 0;
            Console.WriteLine("3OUT! Change!!");
            score.scoreView();

        }

        public void CpuBatting()
        {
            
            while (score.OutCount < 3)
            {
                score.StirikeCount = 0;
                score.BallCount = 0;
                while (score.StirikeCount < 3)
                {
                    runner.RunnerAnnounce(Runner.BaseStatus);
                    Console.WriteLine("どこに投げる？");
                    score.strikeZone();
                    pitcher.ThrowPoint = int.Parse(Console.ReadLine());
                    Console.WriteLine("球種選択\n0.ストレート\n1.シュート\n2.シンカー\n3.フォーク\n4.カーブ\n5.スライダー");
                    pitcher.BreakingBall(int.Parse(Console.ReadLine()));
                    score.loading();
                    if (pitcher.PichingJudge(pitcher.ThrowPoint) == true)
                    {
                        Console.WriteLine("HIT!!!");
                        this.CpuHitDice();
                        this.BatterNumber++;
                        if (this.BatterNumber > 9)
                        {
                            this.BatterNumber = 1;
                        }
                        score.StirikeCount = 0;
                        score.BallCount = 0;
                        if (score.OutCount >= 3)
                        {
                            break;
                        }
                        continue;

                    }
                    else if (pitcher.ThrowPoint == 0 && MeetPoint == 0)
                    {
                        score.BallCount++;
                        Console.WriteLine("BALL!!!");
                        Console.WriteLine("B:" + score.BallCount);
                        if (score.BallCount >= 4)
                        {
                            pitcher.PlayerFourBall();
                            nanJ.NanJnegaPitcher();
                            score.BallCount = 0;
                            score.StirikeCount = 0;
                        }

                    }
                    else
                    {
                        score.StirikeCount++;
                        Console.WriteLine("STRIKE!!!");
                        Console.WriteLine("S:" + score.StirikeCount);
                    }


                }
                score.OutCount++;
                Console.WriteLine("BatterOUT!!");
                this.BatterNumber++;
                Console.WriteLine("O:" + score.OutCount);
                if (this.BatterNumber > 9)
                {
                    this.BatterNumber = 1;
                }
            }
            score.OutCount = 0;
            Console.WriteLine("3OUT! Change!!");
            nanJ.NanJpojiPitcher();
            score.scoreView();


        }

        public void PlayerHitDice()
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 10);
            if(this.BatterNumber >= 1 && this.BatterNumber <= 3)
            {
                switch(ABatter[i])
                {
                    case HitAction.OUT:
                        score.loading();
                        Console.WriteLine("FinePlay!!");
                        switch(Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;

                        }
                        break;
                    case HitAction.HIT:
                        Console.WriteLine("1BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full;
                                Score.PlayerScore++; break;

                        }
                        break;
                    case HitAction.TWO:
                        Console.WriteLine("2BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.PlayerScore+=2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.PlayerScore+=2; break;

                        }
                        break;
                    case HitAction.THREE:
                        Console.WriteLine("3BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 3; break;

                        }
                        break;
                    case HitAction.HOMERUN:
                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 3; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 3; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 3; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 4; break;

                        }
                        break;
                }

            }
            else if(this.BatterNumber >= 4 && this.BatterNumber <= 6)
            {
                switch (PBatter[i])
                {
                    case HitAction.OUT:
                        score.loading();

                        Console.WriteLine("FinePlay!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;

                        }
                        break;
                    case HitAction.HIT:
                        Console.WriteLine("1BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full;
                                Score.PlayerScore++; break;

                        }
                        break;
                    case HitAction.TWO:
                        Console.WriteLine("2BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.PlayerScore += 2; break;
                        }
                        break;
                    case HitAction.THREE:
                        Console.WriteLine("3BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 3; break;

                        }
                        break;
                    case HitAction.HOMERUN:
                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 3; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 3; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 3; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.PlayerScore += 4; break;

                        }
                        break;

                }

            }
            else if(this.BatterNumber >= 7 && this.BatterNumber <= 9)
            {
                switch (LBatter[i])
                {
                    case HitAction.OUT:
                        score.loading();

                        Console.WriteLine("FinePlay!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaButter();
                                }
                                score.OutCount++; break;

                        }
                        break;
                    case HitAction.HIT:
                        Console.WriteLine("1BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full;
                                Score.PlayerScore++; break;

                        }
                        break;
                    case HitAction.TWO:
                        Console.WriteLine("2BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.PlayerScore += 2; break;

                        }
                        break;
                    case HitAction.THREE:
                        Console.WriteLine("3BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiButter();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.PlayerScore += 3; break;

                        }
                        break;
                   

                }

            }

        }

        public void CpuHitDice()
        {
            Random rnd = new Random();
            int i = rnd.Next(0, 10);
            if (this.BatterNumber >= 1 && this.BatterNumber <= 3)
            {
                switch (ABatter[i])
                {
                    case HitAction.OUT:
                        score.loading();

                        Console.WriteLine("FinePlay!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;

                        }
                        break;
                    case HitAction.HIT:
                        Console.WriteLine("1BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full;
                                Score.CpuScore++; break;

                        }
                        break;
                    case HitAction.TWO:
                        Console.WriteLine("2BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.CpuScore += 2; break;

                        }
                        break;
                    case HitAction.THREE:
                        Console.WriteLine("3BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 3; break;

                        }
                        break;
                    case HitAction.HOMERUN:
                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 3; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 3; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 3; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 4; break;

                        }
                        break;
                }

            }
            else if (this.BatterNumber >= 4 && this.BatterNumber <= 6)
            {
                switch (PBatter[i])
                {
                    case HitAction.OUT:
                        score.loading();

                        Console.WriteLine("FinePlay!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;

                        }
                        break;
                    case HitAction.HIT:
                        Console.WriteLine("1BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full;
                                Score.CpuScore++; break;

                        }
                        break;
                    case HitAction.TWO:
                        Console.WriteLine("2BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.CpuScore += 2; break;
                        }
                        break;
                    case HitAction.THREE:
                        Console.WriteLine("3BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 3; break;

                        }
                        break;
                    case HitAction.HOMERUN:
                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 3; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 3; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 3; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaHomerun();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.NoRunner;
                                Score.CpuScore += 4; break;

                        }
                        break;

                }

            }
            else if (this.BatterNumber >= 7 && this.BatterNumber <= 9)
            {
                switch (LBatter[i])
                {
                    case HitAction.OUT:
                        score.loading();

                        Console.WriteLine("FinePlay!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJpojiPitcher();
                                }
                                score.OutCount++; break;

                        }
                        break;
                    case HitAction.HIT:
                        Console.WriteLine("1BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.First;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoS;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.FtoT;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Full;
                                Score.CpuScore++; break;

                        }
                        break;
                    case HitAction.TWO:
                        Console.WriteLine("2BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Second;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.StoT;
                                Score.CpuScore += 2; break;

                        }
                        break;
                    case HitAction.THREE:
                        Console.WriteLine("3BaseHit!!");

                        switch (Runner.BaseStatus)
                        {
                            case Runner.RunnerPosition.NoRunner:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third; break;
                            case Runner.RunnerPosition.First:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Second:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.Third:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore++; break;
                            case Runner.RunnerPosition.FtoS:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.FtoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.StoT:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 2; break;
                            case Runner.RunnerPosition.Full:
                                if (Program.NanJ == true)
                                {
                                    nanJ.NanJnegaPitcher();
                                }
                                Runner.BaseStatus = Runner.RunnerPosition.Third;
                                Score.CpuScore += 3; break;

                        }
                        break;


                }

            }

        }


    }
}
