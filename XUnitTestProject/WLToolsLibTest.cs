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
            TreeTest t = new TreeTest();

            t.test();
            
        }
    }
}
