using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ExercisesRunner.Excercises
{
    class Ex6_Interlocked
    {

        public static void Run()
        {
            var test = new Test();
            test.Execute();
        }

        public class Test
        {
            static int count = 0;

            public void Execute()
            {
                ThreadStart job = new ThreadStart(ThreadJob);
                Thread thread = new Thread(job);
                thread.Start();

                for (int i = 0; i < 5; i++)
                {
                    Interlocked.Increment(ref count);
                }

                thread.Join();
                Console.WriteLine("Final count: {0}", count);
            }

            static void ThreadJob()
            {
                for (int i = 0; i < 5; i++)
                {
                    Interlocked.Increment(ref count);
                }
            }
        }
    }
}
