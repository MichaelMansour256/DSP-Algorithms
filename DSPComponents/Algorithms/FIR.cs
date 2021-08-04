using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSPAlgorithms.DataStructures;

namespace DSPAlgorithms.Algorithms
{
    public class FIR : Algorithm
    {
        public Signal InputTimeDomainSignal { get; set; }
        public FILTER_TYPES InputFilterType { get; set; }
        public float InputFS { get; set; }
        public float? InputCutOffFrequency { get; set; }
        public float? InputF1 { get; set; }
        public float? InputF2 { get; set; }
        public float InputStopBandAttenuation { get; set; }
        public float InputTransitionBand { get; set; }
        public Signal OutputHn { get; set; }
        public Signal OutputYn { get; set; }

        
            public override void Run()
            {
                int N = 0;
                float value = 0.0f;
                String windowName = "";
                OutputHn = new Signal(new List<float>(),new List<int>(),false);
                if (InputStopBandAttenuation <= 21) {
                     value = 0.9f;
                     windowName = "rectangle";
                }
                else if (InputStopBandAttenuation > 21 && InputStopBandAttenuation <=44) { 
                     value= 3.1f;
                     windowName = "hanning";
                }
                else if (InputStopBandAttenuation > 44 && InputStopBandAttenuation <= 53)
                {
                    value = 3.3f;
                    windowName = "hamming";

                }
                else if (InputStopBandAttenuation > 53 && InputStopBandAttenuation <= 74)
                {
                    value = 5.5f;
                    windowName = "blackman";

                }
                N = (int)Math.Floor((value / (InputTransitionBand / InputFS)) + 1);
                float cut_off_normalized = 0.0f;
                float cut_off_normalized_1 = 0.0f;
                float cut_off_normalized_2 = 0.0f;

                if (InputFilterType==FILTER_TYPES.HIGH || InputFilterType==FILTER_TYPES.LOW) {

                    cut_off_normalized = (float)(InputCutOffFrequency + (InputTransitionBand / 2));

                } else if (InputFilterType == FILTER_TYPES.BAND_STOP || InputFilterType == FILTER_TYPES.BAND_PASS) {

                    cut_off_normalized_1 = (float)(InputF1 - (InputTransitionBand / 2));
                    cut_off_normalized_2 = (float)(InputF2 + (InputTransitionBand / 2));
                }
                cut_off_normalized = cut_off_normalized / InputFS;
                cut_off_normalized_1 = cut_off_normalized_1 / InputFS;
                cut_off_normalized_2 = cut_off_normalized_2 / InputFS;



                for(int i=0 , n=(int) -N/2;i<N;i++,n++)
                {
                    OutputHn.SamplesIndices.Add(n);
                }

                if (InputFilterType == FILTER_TYPES.LOW) {
                    for (int i=0;i<N;i++) {
                        int index = Math.Abs(OutputHn.SamplesIndices[i]);
                        if (OutputHn.SamplesIndices[i] == 0) {
                            float hn = 2 * cut_off_normalized;
                            float wn = window_function(windowName, index, N);
                           OutputHn.Samples.Add( hn*wn);
                        }
                        else
                        {
                            float wc = (float)(2 * Math.PI * cut_off_normalized * index);
                            float hn = (float)(2 * cut_off_normalized *Math.Sin(wc)/wc);
                            float wn = window_function(windowName, index, N);
                            OutputHn.Samples.Add(hn*wn);
                        }
                    }
                }
                else if (InputFilterType == FILTER_TYPES.HIGH) {
                    for (int i = 0; i < N; i++)
                    {
                        int index = Math.Abs(OutputHn.SamplesIndices[i]);
                        if (OutputHn.SamplesIndices[i] == 0)
                        {
                            float hn = (2 * cut_off_normalized);
                            hn = 1 - hn;
                            float wn = window_function(windowName, index, N);
                            OutputHn.Samples.Add(hn * wn);
                        }
                        else
                        {
                            float wc = (float)(2 * Math.PI * cut_off_normalized * index);
                            float hn = (float)(2 * cut_off_normalized * Math.Sin(wc) / wc);
                            hn = - hn;
                            float wn = window_function(windowName, index, N);
                            OutputHn.Samples.Add(hn * wn);
                        }
                    }

                }
                else if (InputFilterType == FILTER_TYPES.BAND_PASS)
                {
                    for (int i = 0; i < N; i++)
                    {
                        int index = Math.Abs(OutputHn.SamplesIndices[i]);
                        if (OutputHn.SamplesIndices[i] == 0)
                        {
                            float hn = 2 * (cut_off_normalized_2 - cut_off_normalized_1);
                            float wn = window_function(windowName, index, N);
                            OutputHn.Samples.Add(hn*wn);
                        }
                        else
                        {
                            float w2 = (float)(2 * Math.PI * cut_off_normalized_2 * index);
                            float w1 = (float)(2 * Math.PI * cut_off_normalized_1 * index);
                            float hn = (float)((2 * cut_off_normalized_2 * Math.Sin(w2) / w2) - (2 * cut_off_normalized_1 * Math.Sin(w1) / w1)); 

                            float wn = (window_function(windowName, index, N));
                            OutputHn.Samples.Add(hn*wn );
                        }
                    }
                }
                else if (InputFilterType == FILTER_TYPES.BAND_STOP)
                {
                    for (int i = 0; i < N; i++)
                    {
                        int index = Math.Abs(OutputHn.SamplesIndices[i]);
                        if (OutputHn.SamplesIndices[i] == 0)
                        {
                            float hn =1-( 2 * (cut_off_normalized_2 - cut_off_normalized_1));
                            float wn = window_function(windowName, index, N);
                            OutputHn.Samples.Add(hn * wn);
                        }
                        else
                        {
                            float w2 = (float)(2 * Math.PI * cut_off_normalized_2 * index);
                            float w1 = (float)(2 * Math.PI * cut_off_normalized_1 * index);
                            float hn = (float)((2 * cut_off_normalized_1 * Math.Sin(w1) / w1) - (2 * cut_off_normalized_2 * Math.Sin(w2) / w2));

                            float wn = (window_function(windowName, index, N));
                            OutputHn.Samples.Add(hn * wn);
                        }
                    }
                }



                DirectConvolution c = new DirectConvolution();
                c.InputSignal1 = InputTimeDomainSignal;
                c.InputSignal2 = OutputHn;
                c.Run();
                OutputYn = c.OutputConvolvedSignal;



            }
            public float window_function(String windowName , int n , int N)
            {
                float res = 0.0f;
                if (windowName== "rectangle")
                {
                    res = 1;
                } else if (windowName == "hanning")
                {
                    res = (float)0.5 + (float)(0.5 * Math.Cos((2 * Math.PI * n) / N));
                } else if (windowName == "hamming") {
                    res = (float)0.54 + (float)(0.46 * Math.Cos((2 * Math.PI * n) / N));
                } else if (windowName == "blackman") {


                    float term1 = (float)(0.5 * Math.Cos((2 * Math.PI * n) / (N - 1)));
                    float term2 = (float)(0.08 * Math.Cos((4 * Math.PI * n) / (N - 1)));
                    res = (float)(0.42 + term1 + term2);
                }

                return res;
            }
        }
}
