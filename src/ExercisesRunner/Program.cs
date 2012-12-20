using System;
using ExercisesRunner.Excercises;

namespace ExercisesRunner
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            //Ex1_Basic.Run();
            //Ex2_RaceCondition.Run();
            //Ex3_DeadLocks_and_Pulses.Run();
            //Ex4_WaitHandles_Auto_ManualResetEvent.Run();
            //Ex5_Mutex.Run();
            Ex6_Interlocked.Run();

            Console.WriteLine("Done. Press any key to continue....");
            Console.ReadKey();
        }
    }
}