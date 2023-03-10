using CommandLine;
using ConsoleApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp
{
    public static class CommandLineManager
    {
        public static Options ParseCommand(string[] args)
        {
            var command = args.FirstOrDefault();

            var options = Parser.Default.ParseArguments<Options>(args).Value;

            options.Command = command switch
            {
                "list" => Command.List,
                "add" => Command.Add,
                "delete" => Command.Delete,
                _ => throw new ArgumentException("Invalid command"),
            };
            return options;

        }

    }
}
