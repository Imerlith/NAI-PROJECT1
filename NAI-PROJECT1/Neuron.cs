using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAI_PROJECT1
{
    class Neuron
    {
        public IEnumerable<double> Weigths { set; get; }
        public double Bias { set; get; }
        public double CalculateNet(IEnumerable<double> input)
        {
            var net = 0.0;
            for (int i = 0; i < input.Count(); i++)
            {
                net += input.ElementAt(i) * Weigths.ElementAt(i);
            }
            net += Bias;
            return net;
        }
        public int CalculateOutput(double net)
        {
            var ret = net >= 0 ? 1 : 0;
            return ret;
        }
        public void RadomizeWeights()
        {
            var random = new Random();
            var weights = new List<double>();
            for (int i = 0; i < 24; i++)
            {
                weights.Add(random.NextDouble() * 20 - 10);
            }
            Weigths = weights;
            Bias = random.NextDouble() * 20 - 10;
        }
        public void CalculateNewWeights(double received,double expected,List<double> input)
        {
            var nWeights = new List<double>();
            for (int i = 0; i < Weigths.Count(); i++)
            {
                nWeights.Add(Weigths.ElementAt(i) + 0.5 * (expected - received) * input.ElementAt(i));
            }

            Bias = 0.5 * (expected - received);
            Weigths = nWeights;
        }
       

    }
}
