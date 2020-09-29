using CommandLine;
using System;
using System.Collections.Generic;

namespace FileConverter
{
    public class Options
    {
        [Option('i', "input", Required = true, HelpText = "Path to input file.")]
        public string Input { get; set; }

        [Option('o', "output", Required = true, HelpText = "File path to output to.")]
        public string Output { get; set; }
    }

    public class FileConverterProgram
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                          .WithParsed(Run)
                          .WithNotParsed(Abort);
        }

        public static void Run(Options options)
        {
            throw new NotImplementedException();
        }

        static void Abort(IEnumerable<Error> errors)
        {
            foreach (var error in errors)
            {
                Console.WriteLine(error.Tag);
            }
        }
    }
}
