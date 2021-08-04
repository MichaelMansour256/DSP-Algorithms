using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class InverseDiscreteFourierTransform : Algorithm
    {
        public Signal InputFreqDomainSignal { get; set; }
        public Signal OutputTimeDomainSignal { get; set; }

        public override void Run()
        {
            StreamReader sr = new StreamReader("text.txt");

            string line = sr.ReadLine();
            float amp = 0, phase = 0;
            int file = 0;
            while (line != null)
            {
                string[] row = line.Split(',');

                amp = float.Parse(row[0]);
                phase = float.Parse(row[1]);
                InputFreqDomainSignal.FrequenciesAmplitudes[file] = amp;
                InputFreqDomainSignal.FrequenciesPhaseShifts[file] = phase;
                file += 1;
                line = sr.ReadLine();

            }

            sr.Close();

            List<float> Samples = new List<float>();
            List<float> Frequencies = new List<float>();
            List<int> SamplesIndices = new List<int>();
            



            OutputTimeDomainSignal = new Signal(Samples, SamplesIndices, false, Frequencies, InputFreqDomainSignal.FrequenciesAmplitudes, InputFreqDomainSignal.FrequenciesPhaseShifts);

            
            for (int k =0;k< InputFreqDomainSignal.FrequenciesAmplitudes.Count; k++) {
                
                float i = 0.0f;
                float j = 0.0f;

                float a = 0.0f;
                float b = 0.0f;
                for (int n = 0; n < InputFreqDomainSignal.FrequenciesAmplitudes.Count; n++) {

                    a = InputFreqDomainSignal.FrequenciesAmplitudes[n] *(float) Math.Cos(InputFreqDomainSignal.FrequenciesPhaseShifts[n]);
                    b = InputFreqDomainSignal.FrequenciesAmplitudes[n] *(float)Math.Sin(InputFreqDomainSignal.FrequenciesPhaseShifts[n]);


                    i += a* ((float)Math.Cos((k * 2 * 180 * n / InputFreqDomainSignal.FrequenciesAmplitudes.Count *Math.PI) /180));
                    j += -1 * b * ((float)Math.Sin((k * 2 * 180 * n / InputFreqDomainSignal.FrequenciesAmplitudes.Count * Math.PI) /180) );

                }
                OutputTimeDomainSignal.Samples.Add((i+j)/ InputFreqDomainSignal.FrequenciesAmplitudes.Count);
                OutputTimeDomainSignal.SamplesIndices.Add(k);
            }
            
            }
        }
    }

