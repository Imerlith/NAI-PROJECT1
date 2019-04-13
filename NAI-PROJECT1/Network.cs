using System;
using System.Collections.Generic;
using System.Linq;


namespace NAI_PROJECT1
{
    class Network
    {
        private List<Neuron> Neurons;
        private double Alpha;
        private List<Training> TrainingSet; 
        public Network(int numberOfNeurons,List<Training> TrainingSet,double Alpha)
        {
            Neurons = new List<Neuron>();
            this.Alpha = Alpha;
            this.TrainingSet = TrainingSet;
            for (int i = 0; i < numberOfNeurons; i++)
            {
                Neurons.Add(new Neuron());
            }
            foreach (Neuron neuron in Neurons)
            {
                neuron.RadomizeWeights();
            }
        }
       
        public void StartLearning()
        {
            
           
            for (int i = 1; i < 101; i++)
            {
                double networkError = 0;
                Console.WriteLine("Ucze się już "+i+" epoke");
                foreach(Training training in TrainingSet)
                {
                    var output = new List<double>();
                    foreach(Neuron neuron in Neurons)
                    {
                        output.Add(CalculateOutput(training.Input,neuron));

                    }
                    CalculateNewWeights(output, training.Expected, training.Input);
                    for (int j = 0; j < training.Expected.Count(); j++)
                    {
                        networkError += Math.Pow((training.Expected.ElementAt(j)-output.ElementAt(j)), 2);
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
        public string Test(IEnumerable<double> input)
        {
            string response="";
            Console.WriteLine("TESTOWE");

            foreach(Neuron neuron in Neurons)
            {
                var y = CalculateOutput(input, neuron);
                response += y;
                Console.WriteLine(y);
            }
            switch (response)
            {
                case "100": response = "Liczba to : 0"; break;
                case "010": response = "Liczba to : 1"; break;
                case "001": response = "Liczba to : 2"; break;
                default:response = "Nieznana liczba";break;
            }
            return response;
        }
        private double CalculateOutput(IEnumerable<double> input, Neuron neuron)
        {
            var net = 0.0;
            for (int i = 0; i < input.Count(); i++)
            {
                net += input.ElementAt(i) * neuron.Weigths.ElementAt(i);
            }
            net += neuron.Bias;
            return Activate(net);
        }
        private double Activate(double net)
        {
            return net >= 0 ? 1 : 0;
        }
        private void CalculateNewWeights(List<double> received, List<double>  expected, List<double> input)
        {

            for (int i = 0; i < Neurons.Count(); i++)
      
            {
                var nWeights = new List<double>();
                for (int j = 0; j < input.Count(); j++)
                {
                    var oldW = Neurons.ElementAt(i).Weigths.ElementAt(j);
                    nWeights.Add(oldW +Alpha * (expected.ElementAt(i) - received.ElementAt(i)) * input.ElementAt(j));
                }
                Neurons.ElementAt(i).Weigths = nWeights;
                Neurons.ElementAt(i).Bias += Alpha * (expected.ElementAt(i) - received.ElementAt(i));
            }

            
        }
       


    }
}
