using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.EasyHttpClient;
using WlToolsLib.TreeStructure;
using Xunit;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using WlToolsLib.JsonHelper;
using System.IO;

namespace XUnitTestProject
{
    public class WLToolsLibTest
    {
        [Fact]
        public void TestTree1()
        {
            try
            {
                TreeTest t = new TreeTest();

                t.test();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        #region --HttpClient--
        [Fact]
        public void Test_HttpClient()
        {
            IEasyHttpClient hc = new DefaultEasyHttpClient();
            bool done = false;
            Task.Run(async () =>
            {
                try
                {
                    //var res = hc.SetBaseUrl("http://localhost:8090").Post("/api/categorys", new { id = "1" }.ToJson());
                    //Debug.WriteLine(res.Result);


                    //var res1 = hc.SetBaseUrl("http://localhost:8090").Get("/api/category/1").ContinueWith((r)=> {
                    //    if (r.IsCanceled)
                    //    {
                    //        Debug.WriteLine("Canceled");
                    //    }
                    //    if (r.IsCompletedSuccessfully)
                    //    {
                    //        Debug.WriteLine(r.Result);
                    //    }
                    //});

                    var res3 = hc.SetBaseUrl("http://192.168.21.197").GetStream("/download1/fdat").ContinueWith((s) => {
                        s.Result.Position = 0;
                        using (FileStream fs = new FileStream("fdat", FileMode.OpenOrCreate))
                        {
                            s.Result.CopyToAsync(fs);
                            fs.Flush();
                        }
                    });

                    

                    done = true;
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message);
                    Debug.WriteLine(ex.StackTrace);
                }
            });
            while (!done)
            {
                Thread.Sleep(3000);
            }
            Console.WriteLine(done);
        }
        #endregion
    }
}
