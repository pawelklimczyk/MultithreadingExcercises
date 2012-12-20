using System;
using System.Threading;

namespace ExercisesRunner.Excercises
{
    /// <summary>
    /// A race condition occurs when 2 or more threads are able to access shared data and they try to change it at the same time. Because the thread scheduling algorithm can swap between threads at any point, you don't know the order at which the threads will attempt to access the shared data. Therefore, the result of the change in data is dependent on the thread scheduling algorithm, i.e. both threads are 'racing' to access/change the data. 
    /// </summary>
    class Ex2_RaceCondition
    {
        public static void Run()
        {
            Test test = new Test();
            test.Execute();
        }

        public class Test
        {
            static int count = 0;
            static readonly object countLock = new object();

            public void Execute()
            {
                ThreadStart job = new ThreadStart(ThreadJob);
                Thread thread = new Thread(job);
                thread.Start();

                for (int i = 0; i < 5; i++)
                {
                    lock (countLock)
                    {
                        int tmp = count;
                        Console.WriteLine("Read count={0}", tmp);
                        Thread.Sleep(50);
                        tmp++;
                        Console.WriteLine("Incremented tmp to {0}", tmp);
                        Thread.Sleep(20);
                        count = tmp;
                        Console.WriteLine("Written count={0}", tmp);
                    }
                    Thread.Sleep(30);
                }

                thread.Join();
                Console.WriteLine("Final count: {0}", count);
            }

            static void ThreadJob()
            {
                for (int i = 0; i < 5; i++)
                {
                    lock (countLock)
                    {
                        int tmp = count;
                        Console.WriteLine("Read count={0}", tmp);
                        Thread.Sleep(20);
                        tmp++;
                        Console.WriteLine("Incremented tmp to {0}", tmp);
                        Thread.Sleep(10);
                        count = tmp;
                        Console.WriteLine("Written count={0}", tmp);
                    }
                    Thread.Sleep(40);
                }
            }
        }
    }
}
