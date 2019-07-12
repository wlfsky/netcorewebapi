using System;
using System.Collections.Generic;
using System.Text;
using WlToolsLib.TreeStructure;
using Xunit;

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
    }
}
