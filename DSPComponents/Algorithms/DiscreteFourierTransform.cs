using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class DiscreteFourierTransform : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public float InputSamplingFrequency { get; set; }
        public Signal OutputFreqDomainSignal { get; set; }

        public override void Run()
        {
            List<float> sins = new List<float>();
            List<float> cosins = new List<float>();
            List<float> amp = new List<float>();



            
            List<float> Samples = new List<float>();
            List<float> Frequencies = new List<float>();
            List<float> FrequenciesAmplitudes = new List<float>();
            List<float> FrequenciesPhaseShifts = new List<float>();
            

            OutputFreqDomainSignal = new Signal(Samples, false, Frequencies, FrequenciesAmplitudes, FrequenciesPhaseShifts);
            float omga = (int)(2 * Math.PI) / (InputTimeDomainSignal.Samples.Count * (1 / InputSamplingFrequency));

            for (int k = 0; k < InputTimeDomainSignal.Samples.Count; k++)
            {
                float v = 0.0f;
                float i = 0.0f;
                float j = 0.0f;
                for (int n = 0; n < InputTimeDomainSignal.Samples.Count; n++)
                {
                    i += InputTimeDomainSignal.Samples[n] * ((float)Math.Cos((k * 2 * 180 * n / InputTimeDomainSignal.Samples.Count) * (Math.PI/180)));
                    j += -1*InputTimeDomainSignal.Samples[n] * ((float)Math.Sin((k * 2 * 180 * n / InputTimeDomainSignal.Samples.Count) * (Math.PI / 180)));
                    v += InputTimeDomainSignal.Samples[n] * ((float)Math.Cos(k*2*180*n / InputTimeDomainSignal.Samples.Count) - (float)Math.Sin(k * 2 * 180 * n / InputTimeDomainSignal.Samples.Count));

                }
                sins.Add(j);
                cosins.Add(i);
                OutputFreqDomainSignal.FrequenciesAmplitudes.Add((float)(Math.Sqrt(Math.Pow(i, 2) + Math.Pow(j, 2))));
                OutputFreqDomainSignal.FrequenciesPhaseShifts.Add((float)Math.Atan2(j,i));
                OutputFreqDomainSignal.Frequencies.Add(omga * (k + 1));


            }
            

            


            StreamWriter sw = new StreamWriter("text.txt");
            for (int itr = 0; itr < InputTimeDomainSignal.Samples.Count; itr++)
            {
                sw.WriteLine(OutputFreqDomainSignal.FrequenciesAmplitudes[itr] + "," + OutputFreqDomainSignal.FrequenciesPhaseShifts[itr]);
            }
            sw.Close();


        }
    }
}
