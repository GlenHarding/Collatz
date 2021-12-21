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
'nc' = Don't calculate chain lengths (much faster!).
'p' = Print progress of calculation (will slow the calculation significantly).
's' = Single threaded (slower but will produce sequential output with the 'p' option).");
                    Console.ResetColor();
                    return;
                }

                // setup vars
                bool calculateChain = true;
                bool parallel = true;
                bool print = false;
                int maxNumber;
                var result = new Tuple <int, int>(0, 0);
                            
                // Parse intput args
                if (args.Contains("nc"))
                    calculateChain = false;
                if (args.Contains("s"))
                    parallel = false;
                if (args.Contains("p"))
                    print = true;

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

                // Start calculation. Keeping everything inline for faster performace.
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nStarting test of Collatz Conjecture for numbers 1 through {maxNumber}...\n");
                
                var testWatch = new System.Diagnostics.Stopwatch();

                testWatch.Start();

                if(calculateChain)
                {
                    if(parallel)
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
                    MultiThreadNoChain.Process(maxNumber);

                testWatch.Stop();

                Console.WriteLine($"Numbers 1 through {maxNumber} tested successfully.\nExecution Time: {testWatch.ElapsedMilliseconds} ms.\n");
                if(calculateChain)
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
