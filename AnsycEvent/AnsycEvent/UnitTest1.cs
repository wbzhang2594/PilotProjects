using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AnsycEvent
{
    [TestClass]
    public class UnitTest1
    {
        public delegate void AsyncMethodCaller_RaiseEvent();
        [TestMethod]
        public void TestMethod1()
        {
            ClassSubscriber cs = new ClassSubscriber();

            ClassPublisher cp = new ClassPublisher();

            cp.OnEventRaised += cs.LongTimeProcess;
            cp.AsyncMethodCaller_RaiseEvent.BeginInvoke(null, null);
        }
    }

    public class ClassPublisher
    {
        public ClassPublisher()
        {
        }

        public event EventHandler OnEventRaised;


        public AnsycEvent.UnitTest1.AsyncMethodCaller_RaiseEvent AsyncMethodCaller_RaiseEvent
        {
            get
            {
                return raiseEvent;
            }
        }
        

        private void raiseEvent()
        {
            if (OnEventRaised != null)
            {
                try
                {
                    OnEventRaised(this, null);

                }
                catch
                {

                }
                //OnEventRaised(this, null);
            }
        }


    }

    public class ClassSubscriber
    {
        public ClassSubscriber()
        {


        }

        public void LongTimeProcess(object sender, EventArgs e)
        {

            throw new NotImplementedException();

            Console.WriteLine("enter LongTimeProcess.");
            Thread.Sleep(10000);
            Console.WriteLine("out LongTimeProcess.");

        }
    }
}
