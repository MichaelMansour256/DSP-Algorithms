using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Adder : Algorithm
    {
        public List<Signal> InputSignals { get; set; }
        public Signal OutputSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            List<float> Result = new List<float>();
            float res = 0;
            int max = 0;
            for (int j = 0; j < InputSignals.Count; j++)
            {
                if (max < InputSignals[j].Samples.Count) {
                    max = InputSignals[j].Samples.Count;
                }

            }
            for (int j = 0; j < InputSignals.Count; j++)
            {
                while (InputSignals[j].Samples.Count<max)
                {
                    InputSignals[j].Samples.Add(0);
                }
            }
            for (int j = 0; j < InputSignals[0].Samples.Count; j++)
            {
                //Console.WriteLine(OutputSignal.Samples[j]);
                
                for (int i = 0; i < InputSignals.Count; i++)
                {

                     res += InputSignals[i].Samples[j];

                }
                Result.Add(res);
                res = 0;
            }
            OutputSignal = new Signal(Result,false);
        }
    }
}