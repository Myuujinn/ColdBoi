using System;
using System.Runtime.Intrinsics.X86;
using Microsoft.Extensions.CommandLineUtils;

namespace ColdBoi
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var cmd = new CommandLineApplication();
            var argRom = cmd.Option("-r | --rom <file>", "Load this ROM", CommandOptionType.SingleValue);
            var argScale = cmd.Option("-s | --scale <factor>", "Scale screen with factor",
                CommandOptionType.SingleValue);

            cmd.OnExecute(() =>
            {
                if (!argRom.HasValue())
                {
                    Console.Error.WriteLine("No GameBoy ROM provided.");
                    //return 1;
                }

                var scale = 3;
                if (argScale.HasValue())
                {
                    int.TryParse(argScale.Value(), out scale);
                    if (scale < 1)
                    {
                        Console.Error.WriteLine("Scale must be >= 1");
                        return 1;
                    }
                }

                using var window = new ColdBoi(argRom.Value(), scale);
                window.Run();
                return 0;
            });

            cmd.HelpOption("-? | -h | --help");
            cmd.Execute(args);
        }
    }
}