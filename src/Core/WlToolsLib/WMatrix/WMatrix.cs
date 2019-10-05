using System;
using System.Collections.Generic;
using System.Text;

namespace WlToolsLib.WMatrix
{
    public class WMatrix<TX, TY, TZ>
    {
        public IList<IWMatrixPoint<TX, TY, TZ>> MatrixPoints { get; set; }
        public IList<TX> MatrixPointsX { get; set; }
        public IList<TY> MatrixPointsY { get; set; }
        public IList<TZ> MatrixPointsZ { get; set; }
    }
}
