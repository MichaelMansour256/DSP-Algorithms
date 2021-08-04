using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class MultiplySignalByConstant : Algorithm
    {
        public Signal InputSignal { get; set; }
        public float InputConstant { get; set; }
        public Signal OutputMultipliedSignal { get; set; }

        public override void Run()
        {

            List<float> Result = new List<float>();
            float res = 0;
            for (int j = 0; j < InputSignal.Samples.Count; j++)
            {
                res = InputConstant * InputSignal.Samples[j];
                Result.Add(res);
                res = 0;
                OutputMultipliedSignal = new Signal(Result, false);
            }
        }
    }
}
