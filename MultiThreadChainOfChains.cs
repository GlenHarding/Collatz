namespace Collatz
{
    class MultiThreadChainOfChains
    {
        internal static void Process(int maxNumber, int[,] chainLengths, bool print)
        {
            Parallel.For(0, maxNumber, i => {
                if(print)
                    Console.WriteLine($"Testing integer {i+1}.");

                long x=i+1; //long because some numbers in the chain may be very big
                int count = 0;
                if (x<5)
                {
                    while (x!=4) //if we get to 4 we are in the 4->2->1 loop
                    {
                        if(x%2 == 0) //even
                        {
                            x=x/2;
                        }
                        else //odd
                        {
                            x=(x*3)+1;
                        }
                        count++;
                    }

                    if(print)
                        Console.WriteLine("Reached 4. In the 4->2->1 loop.");
                }

                else
                {
                    while (x>i) //For number greater than 4 we only need to check if we eventually reach a lower number because we already checked the lower numbers!
                    {
                        if(x%2 == 0) //even
                        {
                            x=x/2;
                        }
                        else //odd
                        {
                            x=(x*3)+1;
                        }
                        count++;
                    }

                    if(print)
                        Console.WriteLine("Reached a lower number that has already been tested and proven.");
                }

                //This should be thread safe because different threads never touch the same element
                chainLengths[0, i] = count;
                chainLengths[1, i] = (int)x; // ok to cast here because final number is always less than 100,000,000

                if(print)
                    Console.WriteLine($"Integer {i+1} will loop 4->2->1\n");
            });
        }
    }
}
