namespace Collatz
{
    class Program
    {
        static void Main(string[] args)
        {
            bool finished = false;

            Console.CancelKeyPress += delegate {
                Console.ResetColor();
            };

            while (finished == false) {
                var watchTotal = new System.Diagnostics.Stopwatch();
                watchTotal.Start();

                Console.ForegroundColor = ConsoleColor.Red;

                if (args.Length == 0)
                {
                    Console.WriteLine(@"No input parameters were received.\n Please provide a maximum number to test.
Optional Arguments:
'fc' = Calculate full chain length for each number (slower).
'p' = Print progress of calculation (will slow the calculation significantly).
's' = Single threaded (for testing - slower but will produce sequential output with the 'p' option).");
                    Console.ResetColor();
                    return;
                }

                // setup vars
                bool fullChain = false;
                bool singleThread = false;
                bool print = false;
                int maxNumber;
                var result = new Tuple <int, int>(0, 0);

                // Parse intput args
                if (args.Contains("fc"))
                    fullChain = true;
                if (args.Contains("p"))
                    print = true;
                if (args.Contains("s"))
                {
                    singleThread = true;
                    fullChain = true;
                }

                if (args.Length != 1 && args.Length != 2 && args.Length != 3 && args.Length != 4) 
                {
                    Console.WriteLine($"Unknown Arguments");
                    Console.ResetColor();
                    return;
                }

                try
                {
                    maxNumber = Int32.Parse(args[0]);
                }
                catch (FormatException)
                {
                    Console.WriteLine($"Unable to parse max number to test: '{args[0]}'");
                    Console.ResetColor();
                    return;
                }
                if (maxNumber < 1)
                {
                    Console.WriteLine($"Max number cannot be less than 1");
                    Console.ResetColor();
                    return;
                }

                var chainLengths = new int[2, maxNumber]; //Array to store chain of chain data. Pre allocated for speed.

                // Start calculation. Keeping everything inline for faster performace.
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nStarting test of Collatz Conjecture for numbers 1 through {maxNumber}...\n");
                
                var testWatch = new System.Diagnostics.Stopwatch();

                testWatch.Start();

                if(fullChain)
                {
                    if(!singleThread)
                    {
                        result = MultiThread.Process(maxNumber, print);
                    }
                    
                    else //Just for testing. Single threaded, so slower, but produces consecutive output if we print to the console, so we prove we are doing all the work.
                    {
                        Console.WriteLine("Argument 's' specified. Running on a single thread!\n");
                        result = SingleThread.Process(maxNumber, print);
                    }
                }
                else
                {
                    MultiThreadChainOfChains.Process(maxNumber, chainLengths, print);
                    result = CountProcessing.GetLongest(maxNumber, chainLengths, print);
                }

                testWatch.Stop();

                Console.WriteLine($"Numbers 1 through {maxNumber} tested successfully.\nExecution Time: {testWatch.ElapsedMilliseconds} ms.\n");
                Console.WriteLine($"The number with the longest chain is {result.Item1}.\nThe longest chain (including the starting number, to reach the first 4) is {result.Item2 + 1}.\n");
    
                watchTotal.Stop();
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"Total program execution time: {watchTotal.ElapsedMilliseconds} ms.\n");
                Console.ResetColor();

                finished = true;
            }
        }
    }
}
