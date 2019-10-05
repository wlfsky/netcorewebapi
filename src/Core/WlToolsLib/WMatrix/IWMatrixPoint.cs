using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.WMatrix
{
    public interface IWMatrixPoint<TX, TY, TZ>
    {
        public TX MPX { get; set; }
        public TY MPY { get; set; }
        public TZ MPZ { get; set; }
    }
}
