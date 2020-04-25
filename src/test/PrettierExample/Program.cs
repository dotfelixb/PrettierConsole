using System;
using System.Collections.Generic;
using PrettierConsole;
using static System.Console;

namespace PrettierExample {
    class Program {
        static void Main(string[] args) {
            // var data = new [] {
            //     new { Release = 1991, Album = "Out of Time", Songs = 11, Rating = "* * * *", Single = false },
            //     new { Release = 1992, Album = "Automatic for the People", Songs = 12, Rating = "* * * * *", Single = false },
            //     new { Release = 1994, Album = "Monster", Songs = 12, Rating = "* * *", Single = true },
            //     new { Release = 2006, Album = "Zombies! Aliens! Vampires! Dinosaurs!", Songs = 11, Rating = "* * *", Single = true }
            // };

            var data = new List<Track> {
                new Track { Release = 1991, Album = "Out of Time", Songs = 11, Bonus = 3, Rating = "* * * *" },
                new Track { Release = 1992, Album = "Automatic for the People", Songs = 7, Bonus = 3, Rating = "* * * * *", },
                new Track { Release = 1994, Album = "Monster", Songs = 10, Bonus = 1, Rating = "* * *", Single = true },
                new Track { Release = 2006, Album = "Zombies! Aliens! Vampires! Dinosaurs!", Songs = 12, Bonus = 0, Rating = "* * *", },
                new Track { Release = 2006, Album = "Boys Like Girls", Songs = 8, Bonus = 2, Rating = "* * * *", Single = true },

            };

            // what will writeline do,
            // it will call tostring on the type return
            WriteLine(data.ToPrettierTable());
        }
    }

    class Track {
        public int Release { get; set; }
        public string Album { get; set; }
        public int Songs { get; set; }
        public int Bonus { get; set; }
        public string Rating { get; set; }
        public bool Single { get; set; }
    }
}