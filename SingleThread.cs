namespace Collatz
{
    class SingleThread
    {
        internal static Tuple<int, int> Process(int maxNumber, bool print)
        {
            int longestNumber = 0;
            int longestChain = 0;

            for(int i=1; i<=maxNumber; i++)
            {
                long x=i;
                int count = 0;

                while (x!=4) //if we get to 4 we are in the 4->2->1 loop
                {
                    count++;

                    if(x%2 == 0) //even
                        x=x/2;

                    else //odd
                        x=(x*3)+1;
                }

                if(count>longestChain)
                {
                    longestChain = count;
                    longestNumber = i;
                }

            if(print)
                Console.WriteLine($"Chain length for Integer {i} is {count + 1}\n");
            }

            return Tuple.Create(longestNumber, longestChain);
        }
    }
}
