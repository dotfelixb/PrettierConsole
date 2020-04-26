using System;
using System.Collections.Generic;
using PrettierConsole;
using static System.Console;

namespace PrettierExample {
    class Program {
        static void Main(string[] args) {
            var data = new [] {
                new { Release = 1991, Album = "Out of Time", Songs = 10, Rating = "* * * *" },
                new { Release = 1992, Album = "Automatic for the People", Songs = 8, Rating = "* * * * *" },
                new { Release = 1994, Album = "Monster", Songs = 9, Rating = "* * *", },
                new { Release = 2006, Album = "Zombies! Aliens! Vampires! Dinosaurs!", Songs = 11, Rating = "* * *", }
            };

            WriteLine(data.ToPrettierTable(separator: false));
        }
    }

}