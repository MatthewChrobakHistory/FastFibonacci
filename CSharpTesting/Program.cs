using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CSharpTesting
{
    public class Program
    {
        private static void Main(string[] args) {

            uint testingValue = 6900;


            // Denote that we are starting the current test with the given value.
            Console.WriteLine("Starting recursive optimized Fibonacci for {0}", testingValue);

            // Store the current time.
            var start = DateTime.Now;

            var val = new FibonacciSequence(testingValue).Calculate();

            // Subtract the starting time from the current time to get the elapsed time.
            var end = DateTime.Now - start;

            // Display results.
            Console.WriteLine("Elapsed time: {0}ms", end);
            Console.WriteLine("Val: {0:N0}", val);

            // Spacing writeline.
            Console.ReadKey();
        }
    }

    public class FibonacciTest
    {
        private List<uint> _testingNumbers = new List<uint>();

        // A private collection of known fibonaccis.
        // I should have used a regular array, or a list, but using a dictionary was easier.
        private Dictionary<int, BigInteger> _knownFibonaccis = new Dictionary<int, BigInteger>();

        public FibonacciTest(uint number) {
            // Keep on adding numbers to test.
            // This is done to prevent a stackoverflowexception.
            do {
                this._testingNumbers.Add(number);
                number = number / 2;
            } while (number > 1000);
        }

        // UInt wasn't big enough, and only covers up to Fibonacci(92). Also, anything bigger
        // than Fibonacci(6900) causes a StackOverflow.
        public BigInteger OptimizedFibonacci(int value) {

            // You can change the factor to remember a certain fibonacci value.
            // E.G. if you've already calculated F(10), store the value so you don't have to 
            // re-calculate it in the future.
            // The tradeoff here is memory for time and processing.

            // If you want to still use this approach, but use less memory and therefore take up more time and 
            // processing, use a bigger factor number.
            int factor = 1;

            // Base case of F(1) and F(2)
            if (value == 1 || value == 2) {
                return 1;
            } else {
                // Do we have an already calculated fibonacci value?
                if (_knownFibonaccis.ContainsKey(value)) {
                    // We do. Return that instead of making a recursive call to calculate it.
                    var val = _knownFibonaccis[value];
                    _knownFibonaccis.Remove(value);
                    return val;
                } else {
                    // Is the value we're asked to calculate a valid factor?
                    if (value % factor == 0) {
                        // Since it's a valid factor, and it's not contained in the dictionary, calculate it and store it.
                        _knownFibonaccis.Add(value, OptimizedFibonacci(value - 1) + OptimizedFibonacci(value - 2));

                        // Then return it.
                        return _knownFibonaccis[value];
                    }
                }
            }

            // If everything else fails, just make the standard recursive call.
            return OptimizedFibonacci(value - 1) + OptimizedFibonacci(value - 2);
        }

        public long RecursiveFibonaci(long value) {
            // Base case.
            if (value == 1 || value == 2) {
                return 1;
            } else {
                // Recursive call.
                return RecursiveFibonaci(value - 1) + RecursiveFibonaci(value - 2);
            }
        }
    }

    public class Sorter<T>
    {
        private T[] _data;
        private Func<T, bool> _condition;

        public Sorter(T[] data, Func<T, bool> condition) {
            this._data = data;
            this._condition = condition;
        }

        public T[] ListSort() {
            Console.WriteLine("Starting {0} sorter...", "List");
            var startTime = DateTime.Now;
            
            List<T> list = new List<T>();
            for (int i = 0; i < this._data.Length; i++) {
                if (this._condition.Invoke(this._data[i])) {
                    list.Add(this._data[i]);
                }
            }

            var elapsedTime = DateTime.Now - startTime;
            Console.WriteLine("ElapsedTime: {0}", elapsedTime.ToString());
            var array = list.ToArray();
            return array;
        }

        public T[] O_2N_Sort() {

            Console.WriteLine("Starting {0} sorter...", "O(2N)");
            var startTime = DateTime.Now;
            

            int count = 0;
            for (int i = 0; i < this._data.Length; i++) {
                if (this._condition.Invoke(this._data[i])) {
                    count++;
                }
            }

            T[] sortedArray = new T[count];
            count = -1;
            for (int i = 0; i < this._data.Length && count < sortedArray.Length; i++) {
                if (this._condition.Invoke(this._data[i])) {
                    sortedArray[++count] = this._data[i];
                }
            }

            var elapsedTime = DateTime.Now - startTime;
            Console.WriteLine("ElapsedTime: {0}", elapsedTime.ToString());
            return sortedArray;
        }

        public T[] ArrayResizeSort() {
            Console.WriteLine("Starting {0} sorter...", "ArrayResize");
            var startTime = DateTime.Now;

            T[] array = new T[0];

            for (int i = 0; i < this._data.Length; i++) {
                if (this._condition.Invoke(this._data[i])) {
                    Array.Resize(ref array, array.Length + 1);
                    array[array.Length - 1] = this._data[i];
                }
            }

            var elapsedTime = DateTime.Now - startTime;
            Console.WriteLine("ElapsedTime: {0}", elapsedTime.ToString());
            return array;
        }

        public T[] EnumerableSort() {
            Console.WriteLine("Starting {0} sorter...", "Enumerable");
            var startTime = DateTime.Now;


            T[] array = YieldSort().ToArray();


            var elapsedTime = DateTime.Now - startTime;
            Console.WriteLine("ElapsedTime: {0}", elapsedTime.ToString());
            return array;
        }

        private IEnumerable<T> YieldSort() {
            Console.WriteLine("Starting {0} sorter...", "Yield");
            var startTime = DateTime.Now;

            for (int i = 0; i < this._data.Length; i++) {
                if (this._condition.Invoke(this._data[i])) {
                    yield return this._data[i];
                }
            }

            var elapsedTime = DateTime.Now - startTime;
            Console.WriteLine("ElapsedTime: {0}", elapsedTime.ToString());
        }

        public T[] LinqSort() {
            Console.WriteLine("Starting {0} sorter...", "Linq");
            var startTime = DateTime.Now;

            var array = this._data.Where(this._condition).ToArray();

            var elapsedTime = DateTime.Now - startTime;
            Console.WriteLine("ElapsedTime: {0}", elapsedTime.ToString());
            return array;
        }


        public T[] LinkedListSort() {
            Console.WriteLine("Starting {0} sorter...", "LinkedList");
            var startTime = DateTime.Now;
            var linkedList = new LinkedList<T>();


            for (int i = 0; i < this._data.Length; i++) {
                if (this._condition.Invoke(this._data[i])) {
                    linkedList.AddLast(this._data[i]);
                }
            }

            var array = linkedList.ToArray();

            var elapsedTime = DateTime.Now - startTime;
            Console.WriteLine("ElapsedTime: {0}", elapsedTime.ToString());
            return array;
        }

        public T[] CustomListSort() {
            Console.WriteLine("Starting {0} sorter...", "CustomListSort");
            var startTime = DateTime.Now;

            T[] array = new T[0];
            int arrayPtr = 0;

            for (int i = 0; i < this._data.Length; i++) {
                if (this._condition.Invoke(this._data[i])) {

                    if (arrayPtr >= array.Length) {
                        Array.Resize(ref array, (array.Length + 1) * 5);
                    }

                    array[arrayPtr++] = this._data[i];
                }
            }

            Array.Resize(ref array, arrayPtr);

            var elapsedTime = DateTime.Now - startTime;
            Console.WriteLine("ElapsedTime: {0}", elapsedTime.ToString());
            return array;
        }
    }
}
