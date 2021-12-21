namespace Collatz
{
    class MultiThreadNoChain
    {
        internal static void Process(int maxNumber)
        {
            Parallel.For(1, maxNumber, i => {
                int x=i;
                if (x<5)
                {
                    while (x!=4) //if we get to 4 we are in the 4->2->1 loop
                        if(x%2 == 0) //even
                        {
                            x=x/2;
                        }
                        else //odd
                        {
                            x=(x*3)+1;
                        }
                }

                else
                {
                    while (x>i-1) //For number greater than 4 we only need to check if we eventually reach a lower number because we already checked the lower numbers!
                        if(x%2 == 0) //even
                        {
                            x=x/2;
                        }
                        else //odd
                        {
                            x=(x*3)+1;
                        }
                }
            });
        }
    }
}
