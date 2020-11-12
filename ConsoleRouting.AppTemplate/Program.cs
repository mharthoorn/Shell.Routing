﻿using ConsoleRouting;
using System;
using System.Linq;

namespace ConsoleAppTemplate
{

    /// <summary>
    /// 
    /// </summary>
    [Module, Hidden]
    public class BasicCommands
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        [Command]
        public void Documentation(Arguments args = null)
        {
            if (args is null | args.Count == 0)
            {
                Routing.WriteRoutes();
            }
            else
            {
                args.RemoveAt(0);
                RoutingWriter.WriteRouteHelp(Routing.Router, args);
            }
        }

        [Command, Default, Hidden]
        public void Default()
        {
            // If you provide no parameters, you end up here.
            Console.WriteLine($"Your tool. Version 0.1. Copyright (c) you.");
        }

        /// <summary>Says hello to the name in question</summary>
        /// <param name="name"> The name that will be greeted. You can use any name in the known universe </param>
        /// <param name="uppercase">Transforms the name to all caps</param>
        /// <param name="repeats">How many times the greeting should be repeated</param>
        [Command, Help("Says hello to the given name")]
        public void Greet([Optional]string name, bool uppercase, Flag<int> repeats)
        {
            if (uppercase) name = name.ToUpper();
            for(int i = 1; i < (repeats.HasValue ? repeats.Value : 1); i++)
            {
                Console.WriteLine($"Hello {name}!");
            }
        }


        /// <summary>
        /// This command shows how funny it is to create commands. Although the show command does not do anything, 
        /// it helps understand that you can write extensive documentation on commands from ConsoleRouting.
        /// 
        /// And here you see how that works. This line was after a double new line. Do you think we correctly deal with spaces?
        /// </summary>
        [Command]
        public void Show()
        {
        }
    }
    
    
    class Program
    {
        static void Main(string[] args)
        {
            Routing.Handle(args);
            //Routing.Interactive();
        }
    }
}
