namespace Collatz
{
    class CountProcessing
    { 
        internal static Tuple<int, int> GetLongest(int maxNumber, int[,] chainLengths, bool print)
        {
            int numMax = 0;
            int max = 0;
            object sync = new object();

            Parallel.For(0, maxNumber, i => {
                
                //start with the minimum number we stopped at and the chain length to get there
                int fullChain = chainLengths[0, i];
                int intermediateMinNumber = chainLengths[1, i];

                //then add up the chain lengths for each smaller number we already checked until we get to 4
                while (intermediateMinNumber != 4)
                {
                    fullChain += chainLengths[0, intermediateMinNumber-1];
                    intermediateMinNumber = chainLengths[1, intermediateMinNumber-1];
                }
                
                if(print)
                    Console.WriteLine($"Chain length for Integer {i+1} is {fullChain+1}");

                if(fullChain>max)
                {
                    lock(sync) //make these shared variable updates thread safe
                    {
                        max = fullChain;
                        numMax = i+1;
                    }
                }
            });
            return Tuple.Create(numMax, max);
        }
    }
}
