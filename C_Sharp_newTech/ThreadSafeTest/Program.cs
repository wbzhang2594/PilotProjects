using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadSafeTest
{
    class Program
    {

        static void Main(string[] args)
        {
            int ThreadCount = 64;
            ManualResetEvent[] donEvent = new ManualResetEvent[ThreadCount];
            Dictionary<string, string> dic_map = new Dictionary<string, string>();
            ThreadSafeTestClass testclass = new ThreadSafeTestClass(dic_map);


            for (int i = 0; i < 10000; i++)
            {
                dic_map["key_" + i.ToString()] = "value_" + i;
            }

            for (int i = 0; i < ThreadCount; i++)
            {
                donEvent[i] = new ManualResetEvent(false);

                ThreadPool.QueueUserWorkItem(testclass.ThreadPoolCallBack, donEvent[i]);
            }

            WaitHandle.WaitAll(donEvent);
            System.Console.WriteLine("Finished.");
            System.Console.ReadKey();
        }

    }

    class ThreadSafeTestClass
    {
        Dictionary<string, string> dic_map;

        public ThreadSafeTestClass(Dictionary<string, string> dic_map)
        {
            this.dic_map = dic_map;
        }

        public void ThreadPoolCallBack(object state)
        {
            ManualResetEvent donEvent = (ManualResetEvent)state;

            for (int count = 0; count < 10000; count++)
            {
                string key = "key_" + count;
                if(!dic_map.ContainsKey(key))
                {
                    System.Console.WriteLine("Found no key issue.");
                }
                Thread.Sleep(1);
            }
            System.Console.WriteLine("Thread Finished.");
            donEvent.Set();
        }
    }
}
