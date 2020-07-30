using System;
using System.Collections.Generic;
using System.Text;

namespace baseball
{
    class Runner
    {
        [Flags]
        public enum RunnerPosition
        {
            NoRunner = 0b000,
            First    = 0b001,
            Second   = 0b010,
            Third    = 0b100,
            FtoS     = 0b011,
            StoT     = 0b110,
            FtoT     = 0b101,
            Full     = 0b111,
        }
        public static RunnerPosition BaseStatus { get; set; }

        static Runner()
        {
            Runner.BaseStatus = RunnerPosition.NoRunner;
        }

        public void RunnerAnnounce(RunnerPosition runnerPosition)
        {
            switch(runnerPosition)
            {
                case RunnerPosition.NoRunner: Console.WriteLine("ランナーはありません"); break;
                case RunnerPosition.First: Console.WriteLine("ランナーは1塁です"); break;
                case RunnerPosition.Second: Console.WriteLine("ランナーは2塁です"); break;
                case RunnerPosition.Third: Console.WriteLine("ランナーは3塁です"); break;
                case RunnerPosition.FtoS: Console.WriteLine("ランナーは1・2塁です"); break;
                case RunnerPosition.FtoT: Console.WriteLine("ランナーは1・3塁です"); break;
                case RunnerPosition.StoT: Console.WriteLine("ランナーは2・3塁です"); break;
                case RunnerPosition.Full: Console.WriteLine("ランナーは満塁です"); break;
            }
        }
    }
}
