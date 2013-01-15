using System;
using System.Collections.Generic;
using System.Threading;

namespace ExercisesRunner.Excercises
{
    public class Ex7_LockException
    {
        public static void Run()
        {
            Test test = new Test();
            test.Execute();
        }

        class Test
        {
            private readonly object _locker = new object();

            public void Execute()
            {
                Dictionary<string, object> dict1 = new Dictionary<string, object>
                    {
                        {"seconds", 1},
                        {"throwExc", true},
                        {"name", "t1"}
                    };

                Dictionary<string, object> dict2 = new Dictionary<string, object>
                    {
                        {"seconds", 2},
                        {"throwExc", false},
                        {"name", "t2"}
                    };

                Thread t1 = new Thread(threadMainMethod);
                Thread t2 = new Thread(threadMainMethod);

                t1.Start(dict1);
                t2.Start(dict2);
            }

            void threadMainMethod(object obj)
            {
                Dictionary<string, object> dict = obj as Dictionary<string, object>;

                int sleepInSeconds = (int)dict["seconds"];
                Thread.Sleep(sleepInSeconds * 1000);
                
                try
                {
                    Console.WriteLine("Locking in thread {0}... waiting....", dict["name"]);

                    lock (_locker)
                    {
                        Console.WriteLine("Locked in thread {0}", dict["name"]);

                        bool throwException = (bool)dict["throwExc"];

                        if (throwException)
                        {
                            Console.WriteLine("Exception thrown thread {0}", dict["name"]);
                            throw new Exception();
                        }
                    }

                    Console.WriteLine("Unlocking in thread {0}", dict["name"]);
                }
                catch (Exception)
                {
                    Console.WriteLine("Exception catched in thread {0}", dict["name"]);
                }
            }
        }
    }
}
