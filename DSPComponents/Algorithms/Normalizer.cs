using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class Normalizer : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputMinRange { get; set; }
        public float InputMaxRange { get; set; }
        public Signal OutputNormalizedSignal { get; set; }

        public override void Run()
        {
            //throw new NotImplementedException();
            List<float> Result = new List<float>();
            float res = 0;
            float max = InputSignal.Samples.Max();
            float min = InputSignal.Samples.Min();
            for (int j = 0; j < InputSignal.Samples.Count; j++)
            {
                    res = (InputMaxRange-InputMinRange)*((InputSignal.Samples[j] - min) / (max - min))+InputMinRange;
                    Result.Add(res);
                    res = 0;

            }
            OutputNormalizedSignal = new Signal(Result, false);
            
            
                
        }
    }
}
