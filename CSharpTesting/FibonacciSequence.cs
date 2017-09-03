using System;
using System.Collections.Generic;
using System.Numerics;

namespace CSharpTesting
{
    public class FibonacciSequence
    {
        private List<uint> _testingValues;
        private Dictionary<uint, BigInteger> _knownValues;
        
        public FibonacciSequence(uint value) {
            // Initialize the collection of values to test.
             this._testingValues = new List<uint>();

            // Initiliaze the collection of known fibonacci values.
            this._knownValues = new Dictionary<uint, BigInteger>();

            do {
                // Separate the value into separate steps.
                this._testingValues.Add(value);
                if (value > 1000) {
                    value -= 1000;
                }
            } while (value > 1000);
        }

        public BigInteger NonRecCalculate() {
            uint i = 1;

            do {
                ProcessFibonacci(i++);

                if (i > 3) {
                    this._knownValues.Remove(i - 3);
                }
            } while (i < this._testingValues[0]);

            return ProcessFibonacci(this._testingValues[0]);
        }


        public BigInteger Calculate() {
            // Loop through all the necessary steps to calculate the massive Fibonacci value.
            for (int i = this._testingValues.Count - 1; i > 0; i--) {
                // Calculate all the steps, and then remove them.
                this.ProcessFibonacci(this._testingValues[i]);
                this._testingValues.RemoveAt(i);

                //TODO: Remove the unecessary dictionary values.
            }

            // Return the final value.
            return ProcessFibonacci(this._testingValues[0]);
        }


        private BigInteger ProcessFibonacci(uint value) {
            // Base cases.
            if (value == 1 || value == 2) {
                return 1;
            } else {
                // Have we already calulated the fibonacci value?
                if (this._knownValues.ContainsKey(value)) {
                    // We do. Return that instead of making a recursive call to calcluate it.
                    var fibonacciValue = this._knownValues[value];

                    // Remove the value from the dictionary. We no longer need it.
                    this._knownValues.Remove(value);

                    // Return the result.
                    return fibonacciValue;
                } else {
                    // Add the calculated fibonacci value.
                    this._knownValues.Add(value, ProcessFibonacci(value - 1) + ProcessFibonacci(value - 2));

                    // Then return it.
                    return this._knownValues[value];
                }
            }
        }
    }
}

/*
 * 
 * 
 * 5 callbacks MAX
 * 
 * F5 -> 5 callbacks needed
 * 
 * F10 -> F9    -> F8   -> F7   -> F6   -> F5
 * 
 * 
 * F10  -> F9   -> F8   -> F7   -> F6   -> F5   -> F4   -> F3   -> F2   -> F1
 *                      -> F6
 *              -> F7   -> F6
 *                      -> F5
 *              
 *      -> F8   -> F7   -> F6
 *                      -> F5
 *              -> F6   -> F5
 *                      -> F4
 * 
 * 
 */