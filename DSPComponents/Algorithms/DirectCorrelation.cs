using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DirectCorrelation : Algorithm
    {
        public Signal InputSignal1 { get; set; }
        public Signal InputSignal2 { get; set; }
        public List<float> OutputNonNormalizedCorrelation { get; set; }
        public List<float> OutputNormalizedCorrelation { get; set; }

        public override void Run()
        {

            if (InputSignal2 == null)
            {
                List<float> output = new List<float>();
                List<float> output_norm = new List<float>();
                float v = 0;
                List<float> sam = new List<float>(InputSignal1.Samples);
                InputSignal2 = new Signal(sam, InputSignal1.Periodic);
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    v = 0;
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {

                        v += InputSignal1.Samples[j] * InputSignal2.Samples[j];

                    }

                    output.Add((1.0f / InputSignal1.Samples.Count) * v);

                    float val = InputSignal2.Samples[0];
                    InputSignal2.Samples.RemoveAt(0);
                    if (InputSignal2.Periodic)
                    {
                        InputSignal2.Samples.Add(val);
                    }
                    else
                    {

                        InputSignal2.Samples.Add(0);
                    }


                }
                OutputNonNormalizedCorrelation = output;
                for (int i = 0; i < output.Count; i++)
                {
                    output_norm.Add(output[i] / output.Max());
                }
                OutputNormalizedCorrelation = output_norm;

            }
            else
            {
                List<float> output = new List<float>();
                List<float> output_norm = new List<float>();
                float v = 0;

                int n = (InputSignal2.Samples.Count + InputSignal1.Samples.Count) - 1;
                for (int j = InputSignal2.Samples.Count; j < n; j++)
                {

                    InputSignal2.Samples.Add(0);
                    InputSignal2.SamplesIndices.Add(j);

                }
                for (int j = InputSignal1.Samples.Count; j < n; j++)
                {

                    InputSignal1.Samples.Add(0);
                    InputSignal1.SamplesIndices.Add(j);

                }
                float res = 0;
                float seg1 = 0;
                float seg2 = 0;
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    seg1 += InputSignal1.Samples[i] * InputSignal1.Samples[i];
                    seg2 += InputSignal2.Samples[i] * InputSignal2.Samples[i];
                }
                res = (float)Math.Sqrt(seg1 * seg2) * (1.0f / InputSignal1.Samples.Count);
                for (int i = 0; i < InputSignal1.Samples.Count; i++)
                {
                    v = 0;
                    for (int j = 0; j < InputSignal1.Samples.Count; j++)
                    {

                        v += InputSignal1.Samples[j] * InputSignal2.Samples[j];

                    }

                    output.Add((1.0f / InputSignal1.Samples.Count) * v);

                    float val = InputSignal2.Samples[0];
                    InputSignal2.Samples.RemoveAt(0);
                    if (InputSignal2.Periodic)
                    {
                        InputSignal2.Samples.Add(val);
                    }
                    else
                    {

                        InputSignal2.Samples.Add(0);
                    }


                }
                OutputNonNormalizedCorrelation = output;
                for (int i = 0; i < output.Count; i++)
                {
                    output_norm.Add(output[i] / res);
                }
                OutputNormalizedCorrelation = output_norm;

            



            }
        }
    }
}