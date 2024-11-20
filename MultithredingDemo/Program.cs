using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MultithredingDemo
{
    public class Test
    {
        public void M1()
        {
            for(int i = 1; i <= 5; i++)
            {
                Console.WriteLine(i);
                if (i == 3)   //condition based sleep
                {
                    Thread.Sleep(2000);
                }
            }
        }
        public void M2()
        {
            for (int i = 6; i <= 10; i++)
            {
                Console.WriteLine(i);
            }
        }

        
    }
    public class Test2
    {
        public void M3()
        {
            lock (this)//lock keyword is used to make synchonized thread.
            {
                for (int i = 100; i <= 110; i++)
                {
                    Console.WriteLine(i);
                }
            }
        }
        public void M4()
        {
            Monitor.Enter(this);
            {
                for (int i = 200; i <= 210; i++)
                {
                    Console.WriteLine(i);
                }
            }
            Monitor.PulseAll(this);
            Monitor.Exit(this);
        }
        public void M5()
        {
            Monitor.Enter(this);
            try
            {
                for(int i = 500; i <= 505; i++)
                {
                    Console.WriteLine(i);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                Monitor.PulseAll(this);
                Monitor.Exit(this);
            }
        }
       
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Test test= new Test();
            Thread t1 = new Thread(new ThreadStart(test.M1));
            Thread t2 = new Thread(new ThreadStart(test.M2));
            
            //t1.Priority = ThreadPriority.BelowNormal;
            //t2.Priority = ThreadPriority.Highest;
            t1.Start();
            t2.Start();
           
            //t1.Join();  // next all thread will be in wait state to complete the task of t1 thread
            //t2.Join();

            Test2 test2= new Test2();
            Thread t3 = new Thread(new ThreadStart(test2.M3));
            Thread t4 = new Thread(new ThreadStart(test2.M4));
            Thread t5 = new Thread(new ThreadStart(test2.M5));
            Thread t6 = new Thread(new ThreadStart(test2.M5));
            t3.Start();
            t4.Start();
            t5.Start();
            t6.Start();

        }
    }
}
