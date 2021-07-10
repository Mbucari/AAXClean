using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AAXClean.Util
{
    internal class MovingAverage
    {
        public int K { get; }
        public double Average { get; private set; }
        private readonly double[] values;

        private int index = 0;
        private double sum = 0;        

        public MovingAverage(int numAverage)
        {
            K = numAverage;
            values = new double[K];
        }

        public void AddEntry(double nextInput)
        {
            sum = sum - values[index] + nextInput;
            values[index] = nextInput;
            index = (index + 1) % K;
            Average = sum / K;
        }
    }
}
