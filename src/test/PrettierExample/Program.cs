using System;
using PrettierConsole;
using static System.Console;

namespace PrettierExample {
    class Program {
        static void Main(string[] args) {
            var data = new [] {
                new { Year = 1991, Album = "Out of Time", Songs = 11, Rating = "* * * *" },
                new { Year = 1992, Album = "Automatic for the People", Songs = 12, Rating = "* * * * *" },
                new { Year = 1994, Album = "Monster", Songs = 12, Rating = "* * *" }
            };

            // what will writeline do,
            // it will call tostring on the type return
            WriteLine(data.ToPrettierTable());
        }
    }
}