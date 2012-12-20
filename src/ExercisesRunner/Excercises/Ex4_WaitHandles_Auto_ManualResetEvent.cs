using System;
using System.Threading;

namespace ExercisesRunner.Excercises
{
    public class Ex4_WaitHandles_Auto_ManualResetEvent
    {
        public static void Run()
        {
            Test test = new Test();
            test.Execute();
        }

        class Test
        {
            public void Execute()
            {
                ManualResetEvent[] events = new ManualResetEvent[10];

                for (int i = 0; i < events.Length; i++)
                {
                    events[i] = new ManualResetEvent(false);
                    Runner r = new Runner(events[i], i);
                    new Thread(new ThreadStart(r.Run)).Start();
                }

                int index = WaitHandle.WaitAny(events);

                Console.WriteLine("***** The winner is {0} *****", index);

                WaitHandle.WaitAll(events);
                Console.WriteLine("All finished!");
            }
        }

        class Runner
        {
            static readonly object rngLock = new object();

            static Random rng = new Random();//this is not thread-safe!

            ManualResetEvent ev;
            int id;

            internal Runner(ManualResetEvent ev, int id)
            {
                this.ev = ev;
                this.id = id;
            }

            internal void Run()
            {
                for (int i = 0; i < 10; i++)
                {
                    int sleepTime;


                    lock (rngLock)
                    {
                        sleepTime = rng.Next(500);
                    }

                    Thread.Sleep(sleepTime);
                    Console.WriteLine("Runner {0} at stage {1}", id, i);
                }
                ev.Set();
            }
        }
    }
}
