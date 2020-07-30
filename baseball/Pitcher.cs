using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace baseball
{
    class Pitcher
    {
        public int ThrowPoint { get; set; }

        public Pitcher()
        {
            this.ThrowPoint = 0;
        }

       

        public void CpuPitching()
        {
            Random rmd = new Random();
            this.ThrowPoint = rmd.Next(0, 10);
            int ball = rmd.Next(0, 5);
            BreakingBall(ball);
        }

        public bool PichingJudge(int throwPoint)
        {

            Batter batter = new Batter();
            batter.CpuMeetPoint();
            bool hit = false;

            if (Batter.MeetPoint >= 1 && Batter.MeetPoint <= 3)
            {
                if (Batter.MeetPoint != 3)
                {
                    if (Batter.MeetPoint == throwPoint || Batter.MeetPoint == throwPoint + 1)
                    {
                        hit = true;
                    }
                }
                else if (Batter.MeetPoint == throwPoint)
                {
                    hit = true;
                }


            }
            else if (Batter.MeetPoint >= 4 && Batter.MeetPoint <= 6)
            {
                if (Batter.MeetPoint != 6)
                {
                    if (Batter.MeetPoint == throwPoint || Batter.MeetPoint == throwPoint + 1)
                    {
                        hit = true;
                    }
                }
                else if (Batter.MeetPoint == throwPoint)
                {
                    hit = true;
                }
            }
            else if (Batter.MeetPoint >= 7 && Batter.MeetPoint <= 9)
            {
                if (Batter.MeetPoint != 9)
                {
                    if (Batter.MeetPoint == throwPoint || Batter.MeetPoint == throwPoint + 1)
                    {
                        hit = true;
                    }
                }
                else if (Batter.MeetPoint == throwPoint)
                {
                    hit = true;
                }
            }
            return hit;
        }

        public void BreakingBall(int ball)
        {
            switch (ball)
            {
                case 0: ThrowPoint += 0; break;
                case 1: if (ThrowPoint == 0 || ThrowPoint == 1 || ThrowPoint == 4 || ThrowPoint == 7)
                    {
                        ThrowPoint = 0;
                    }
                    else
                    { 
                        ThrowPoint -= 1; 
                    }
                    break;
                case 2:
                    if (ThrowPoint == 0 || ThrowPoint == 1 || ThrowPoint == 4 || ThrowPoint == 7 || ThrowPoint == 8 || ThrowPoint == 9)
                    {
                        ThrowPoint = 0;
                    }
                    else
                    {
                        ThrowPoint += 2;
                    }
                    break;
                case 3:
                    if (ThrowPoint == 0 || ThrowPoint == 7 || ThrowPoint == 8 || ThrowPoint == 9)
                    {
                        ThrowPoint = 0;
                    }
                    else
                    {
                        ThrowPoint += 3;
                    }
                    break;
                case 4:
                    if (ThrowPoint == 0 || ThrowPoint == 3 || ThrowPoint == 6 || ThrowPoint == 7 || ThrowPoint == 8 || ThrowPoint == 9)
                    {
                        ThrowPoint = 0;
                    }
                    else
                    {
                        ThrowPoint += 4;
                    }
                    break;
                case 5:
                    if (ThrowPoint == 0 || ThrowPoint == 3 || ThrowPoint == 6 || ThrowPoint == 9)
                    {
                        ThrowPoint = 0;
                    }
                    else
                    {
                        ThrowPoint += 1;
                    }
                    break;
            }
        }

        public void PlayerFourBall()
        {
            switch (Runner.BaseStatus)
            {
                case Runner.RunnerPosition.NoRunner:
                    Runner.BaseStatus = Runner.RunnerPosition.First;break;
                case Runner.RunnerPosition.First:
                    Runner.BaseStatus = Runner.RunnerPosition.FtoS; break;
                case Runner.RunnerPosition.Second:
                    Runner.BaseStatus = Runner.RunnerPosition.FtoS; break;
                case Runner.RunnerPosition.Third:
                    Runner.BaseStatus = Runner.RunnerPosition.FtoT; break;
                case Runner.RunnerPosition.FtoS:
                    Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                case Runner.RunnerPosition.FtoT:
                    Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                case Runner.RunnerPosition.StoT:
                    Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                case Runner.RunnerPosition.Full:
                    Runner.BaseStatus = Runner.RunnerPosition.Full;
                    Score.PlayerScore++; break;
            }
        }

        public void CpuFourBall()
        {
            switch (Runner.BaseStatus)
            {
                case Runner.RunnerPosition.NoRunner:
                    Runner.BaseStatus = Runner.RunnerPosition.First;break;
                case Runner.RunnerPosition.First:
                    Runner.BaseStatus = Runner.RunnerPosition.FtoS; break;
                case Runner.RunnerPosition.Second:
                    Runner.BaseStatus = Runner.RunnerPosition.FtoS; break;
                case Runner.RunnerPosition.Third:
                    Runner.BaseStatus = Runner.RunnerPosition.FtoT; break;
                case Runner.RunnerPosition.FtoS:
                    Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                case Runner.RunnerPosition.FtoT:
                    Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                case Runner.RunnerPosition.StoT:
                    Runner.BaseStatus = Runner.RunnerPosition.Full; break;
                case Runner.RunnerPosition.Full:
                    Runner.BaseStatus = Runner.RunnerPosition.Full;
                    Score.CpuScore++; break;
            }
        }


    }
}
