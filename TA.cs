using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TALib;

namespace IGTrading
{
    public static class TA
    {
        public static double[] SMA(double[] prices,int period)
        {
            int outBgd = 0;

            double[] sma1 = new double[prices.Length];

            double[] sma20 = new double[prices.Length ];

            int elementCount =0;
            Core.Sma(prices, 0, prices.Length-1 , sma1, out outBgd, out elementCount, period);
            Array.Copy(sma1, 0, sma20, period, sma1.Count() - period);

            return sma20;
        }
    }
}
