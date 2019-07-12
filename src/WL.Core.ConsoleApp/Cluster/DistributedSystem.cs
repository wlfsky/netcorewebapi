using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using static System.Console;

namespace WL.Core.ConsoleApp.Cluster
{
    public class DistributedSystem
    {
    }

    public class DistributedSystemTest
    {
        #region --集群分体处理测试方法入口--
        public static void test_the_cluster()
        {
            var fsio00 = System.IO.File.CreateText(AppDomain.CurrentDomain.BaseDirectory + "log_00.txt");

            TaskWork<int, string> tw = new TaskWork<int, string>();
            tw.PCluster.ProcessHandle = (i) =>
            {
                //Task.Delay(2000);
                Thread.Sleep(500);
                var str = "===={0}====".FormatStr(i.TaskPID);

                //fsio00.WriteLine(str);
                return new TaskID<string>() { TaskPID = i.TaskPID, TaskData = str };
            };

            tw.OutputTask.PullProcess = (p) =>
            {
                //Task.Delay(200);
                Thread.Sleep(100);
                fsio00.WriteLine("===> Saved {0}.".FormatStr(p.TaskPID));
            };

            tw.RunTask();
            tw.OutputStatus();
            WriteLine("==start push==");
            Thread t = new Thread(new ThreadStart(() =>
            {
                for (int i = 100; i < 200; i++)
                {
                    WriteLine("==> input ..." + i.ToString());
                    tw.InputTask.PushTask(i);
                }
            }));
            t.Start();
            Thread t2 = new Thread(new ThreadStart(() =>
            {
                for (int i = 200; i < 400; i++)
                {
                    WriteLine("==> input ..." + i.ToString());
                    tw.InputTask.PushTask(i);
                }
            }));
            t2.Start();
            System.Threading.Tasks.Parallel.Invoke(() =>
            {
                for (int i = 0; i < 100; i++)
                {
                    WriteLine("==> input ..." + i.ToString());
                    tw.InputTask.PushTask(i);
                }
            }, () =>
            {
                for (int i = 100; i < 200; i++)
                {
                    WriteLine("==> input ..." + i.ToString());
                    tw.InputTask.PushTask(i);
                }
            });
            for (int i = 0; i < 100; i++)
            {
                if (tw.OutputTask.DiverterCount > 0)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    break;
                }
            }
            //Thread.Sleep(20000);
            //Task.Delay(10000);
            tw.PCluster.Stop();
            fsio00.Close();
        }
        #endregion
    }
}
