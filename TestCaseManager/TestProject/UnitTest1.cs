using System;
using System.Threading;
using Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestProject
{
    //    Background:
    //目前PCT压力太大，已经超过了我们系统能够承受的极限。所以我们为了解决这个问题设计了一套新的PCT逻辑 -- “回溯测试”。简单的讲就是在PCT压力较大情况下将测试请求排队。当测试资源有空闲时，先跑队尾的(最新的一次)请求(index=n)。收集ΔFailed。然后跑第n-1个，只跑ΔFailed，依次类推，将测试压力减少十倍以上。这样就可以有更多的测试机来跑同一个测试。也缩短了PCT的等待时间。
    //Design简述：
    //例如跑BLL测试，假设总测试时间为120分钟，Workflow执行时间的损耗为10分钟。为了20分钟内出结果，我们会将BLL分配到12台VM上运行测试。这12台机器叫做VM1~VM12。VM1通过VSTest.Console来跑测试，我们已经修改了VSTest.Console，使其可以在开始跑测试之前询问DiscoveryServer来得到一个SubNodeServer, 然后每跑一个Case之前都会去问SubNodeServer：“这个Case有没有其它机器已经跑了？”。SubNodeServer返回True或False。



    //Requirement：
    //DiscoveryServer必须将同一组测试机分配到同一个SubNodeServer
    //a.     执行同一个测试请求的并且Test Scope完全相同的测试机叫做“同一组测试机”
    //DiscoveryServer的分配结果必须做到负载均衡
    //SubNodeServer可以有多个，但是数量不定。
    //VM之间互不认识
    //VM call method from SubNodeServer, method is “bool TryRegisterTestCase(string,string,string)”
    //a.    第一个参数是TestRunID
    //b.    第二个参数是TestScopeName
    //c.    第三个参数是TestCasename
    //SubNodeServer需要保证7X24小时运行不能崩溃
    //SubNodeServer需要及时响应Method Call
    //SubNodeServer需要能够处理1000次/s请求以上。
    //每次请求数据必须能够正确返回结果



    //DoD（Definition of  Done）：
    //写一个Demo, 包含Client，DiscoveryServer和SubNodeServer。
    //Client逻辑：
    //Client读取给定的TXT文件，每行内容作为参数传给SubNodeServer
    //每个Client只会读取一个TXT文件
    //多个Client会读取同一个TXT文件
    //有多个TXT文件
    //每秒100次以上向SubNodeServer请求数据，并记录性能
    //DiscoverServer逻辑: 对Client分配SubNodeServer，做到负载均衡
    //SubNodeServer逻辑:
    //及时响应来自Client的Call
    //完全相同的三个参数只能给一个Client返回True，其它Client的相同请求返回False
    //以上请先在本地环境测试通过，然后进入真实测试环境，会有300+VM启动后运行Client，所有Server会运行在一个Z620机器上(如果你有要求也可以分不到不同的Z620上面)。



    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SyncTest()
        {
            for (int id = 0; id < 100; id++)
            {
                TestAgent _testAgent = new TestAgent(id);
                _testAgent.RunCases(true, 100);
            }
        }

        [TestMethod]
        public void AsyncTest()
        {
            int AgentCount = 500;
            int RequestPerSecond = 100;
            bool useMockServer = true;

            for (int id = 0; id < AgentCount; id++)
            {
                Thread tr = new Thread(() =>
                    {
                        TestAgent _testAgent = new TestAgent(id);
                        _testAgent.RunCases(useMockServer, RequestPerSecond);
                    }
                    );

                {
                    tr.Name = "TestAgent  " + id.ToString();
                }

                tr.Start();
            }
        }
    }
}
