using System;
using System.Collections;
using System.Threading;

namespace ExercisesRunner.Excercises
{
    public class Ex3_DeadLocks_and_Pulses
    {
        public static void Run()
        {
            Test test = new Test();
            test.Execute();
        }

        public class Test
        {
            static ProducerConsumer queue;

            public void Execute()
            {
                queue = new ProducerConsumer();
                Thread consumer=new Thread(new ThreadStart(ConsumerJob));
                consumer.Start();

                Random rng = new Random(0);
                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine("Producing {0}", i);
                    queue.Produce(i);
                    Thread.Sleep(rng.Next(100));
                }

                Console.WriteLine("Waiting for consumer to finish....");
                consumer.Join();
                Console.WriteLine("Consumer finished his job....");
            }

            static void ConsumerJob()
            {
                // Make sure we get a different random seed from the
                // first thread
                Random rng = new Random(1);
                // We happen to know we've only got 10 
                // items to receive
                for (int i = 0; i < 10; i++)
                {
                    object o = queue.Consume();
                    Console.WriteLine("Consuming {0}", o);
                    Thread.Sleep(rng.Next(500));
                }
            }
        }

        public class ProducerConsumer
        {
            readonly object listLock = new object();
            Queue queue = new Queue();

            public void Produce(object o)
            {
                lock (listLock)
                {
                    queue.Enqueue(o);

                    // We always need to pulse, even if the queue wasn't
                    // empty before. Otherwise, if we add several items
                    // in quick succession, we may only pulse once, waking
                    // a single thread up, even if there are multiple threads
                    // waiting for items.            
                    Monitor.Pulse(listLock);
                }
            }

            public object Consume()
            {
                lock (listLock)
                {
                    // If the queue is empty, wait for an item to be added
                    // Note that this is a while loop, as we may be pulsed
                    // but not wake up before another thread has come in and
                    // consumed the newly added object. In that case, we'll
                    // have to wait for another pulse.
                    while (queue.Count == 0)
                    {
                        // This releases listLock, only reacquiring it
                        // after being woken up by a call to Pulse
                        Monitor.Wait(listLock);
                    }
                    return queue.Dequeue();
                }
            }
        }
    }
}
