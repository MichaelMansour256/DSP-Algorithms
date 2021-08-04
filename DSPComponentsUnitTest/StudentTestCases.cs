using System;
using System.Collections.Generic;
using DSPAlgorithms.Algorithms;
using DSPAlgorithms.DataStructures;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DSPComponentsUnitTest
{
    /// <summary>
    /// Feel Free to add your test cases to test your code!
    /// You can always add another Test class in this project:
    /// Right click on the project > Add > New Item > UnitTest
    /// </summary>
    [TestClass]
    public class StudentTestCases
    {
        /// Give a name for the Test Method (i.e StudentTestMethod1)
        /// You need to add the attribute '[TestMethod]' above your method.
        [TestMethod]
        public void StudentTestMethod1()
        {
            /// Add a new instance of your algorithm
            /// Initialize the algorithm input
            /// Find common functionalities used (i.e. load signal from file) in the UnitTestUtilities class
            /// Compare with expected output
            /// Run the test cases from the menu: Test > Windows > Test Explorer
            /// Make sure you have built the porject and run the test case
            /// Hurraaaay .. you made it :) 
            /// 
            /*DirectCorrelation dc = new DirectCorrelation();

            var expectedOutput = new Signal(new List<float>() { 0.5f,0.25f,0.0f,0.25f}, false);


            dc.InputSignal1 = new Signal(new List<float>() { 1.0f, 0.0f, 0.0f, 1.0f } , false);

            dc.Run();

            Assert.IsTrue(UnitTestUtitlities.SignalsSamplesAreEqual(expectedOutput.Samples, dc.OutputNonNormalizedCorrelation));*/
        }
    }
}
