using System;
using System.Threading;

namespace ExercisesRunner.Excercises
{
    internal class Ex5_Mutex
    {
        public static void Run()
        {
            var test = new Test();
            test.Execute();
        }

        private class Test
        {
            public void Execute()
            {
                bool firstInstance;

                using (var mutex = new Mutex(true, @"Global\MutexTestApp-2012", out firstInstance))
                {
                    if (!firstInstance)
                    {
                        Console.WriteLine("Other instance detected; aborting.");
                        return;
                    }

                    Console.WriteLine("We're the only instance running - yay!");

                    for (int i = 0; i < 10; i++)
                    {
                        Console.WriteLine(i);
                        Thread.Sleep(500);
                    }
                }
            }
        }
    }
}