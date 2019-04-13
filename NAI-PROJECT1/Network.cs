using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NAI_PROJECT1
{
    class Network
    {
        
         
        private List<Neuron> Neurons;

        public void InitiateNetwork()
        {
            Neurons = new List<Neuron>
            {
                new Neuron(),
                new Neuron(),
                new Neuron()
            };
            foreach(Neuron neuron in Neurons)
            {
                neuron.RadomizeWeights();
            }
        }
        public void StartLearning()
        {
            
            var correctFor0 = new List<double>(new double[] {0,1,1,0,1,0,0,1,1,0,0,1,1,0,0,1,1,0,0,1,0,1,1,0});
            var expedtedFor0 = new List<int>(new int[] {1,0,0 });
            var correctFor1 = new List<double>(new double[] { 0, 0, 1, 0 , 0, 0, 1, 0 , 0, 0, 1, 0, 0, 0, 1, 0 , 0, 0, 1, 0 , 0, 0, 1, 0 ,  });
            var expedtedFor1 = new List<int>(new int[] { 0, 1, 0 });
            var correctFor2 = new List<double>(new double[] { 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 1, 0, 0, 0, 1, 1, 1, 1 });
            var expedtedFor2 = new List<int>(new int[] { 0, 0, 1 });
            var correctInputs = new List<List<double>>
            {
                correctFor0,
                correctFor1,
                correctFor2
            };
            var expectedOutputs = new List<List<int>>
            {
                expedtedFor0,
                expedtedFor1,
                expedtedFor2
            };
            for (int i = 1; i < 101; i++)
            {
                double networkError = 0;
                Console.WriteLine("Ucze się już "+i+" epoke");
                for (int j = 0; i < Neurons.Count(); i++)
                {
                    
                    var input = correctInputs.ElementAt(j);
                    var net =   Neurons.ElementAt(j).CalculateNet(input);
                    var output = Neurons.ElementAt(j).CalculateOutput(net);
                    var expected = expectedOutputs.ElementAt(j).ElementAt(j);
                    if (output != expected)
                    {
                        Neurons.ElementAt(j).CalculateNewWeights(output, expected, input);
                        networkError += Math.Pow((expected - output), 2);
                    }
                    
                }
                networkError /= 2;
                Console.WriteLine(networkError);
                if (networkError == 0.0)
                {
                    Console.WriteLine("Nauczylem sie");
                    break;
                }
            }

        }
        public void Test(IEnumerable<double> input)
        {
            Console.WriteLine("TESTOWE");
            foreach(Neuron neuron in Neurons)
            {
                var net = neuron.CalculateNet(input);
                var y = neuron.CalculateOutput(net);
                Console.WriteLine(y);
            }
        }
        
    }
}
