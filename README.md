
PrettierConsole takes a data structure, format it nicely for printing to console or any output


```csharp
var data = new [] {
    new { Release = 1991, Album = "Out of Time", Songs = 10, Rating = "* * * *" },
    new { Release = 1992, Album = "Automatic for the People", Songs = 8, Rating = "* * * * *" },
    new { Release = 1994, Album = "Monster", Songs = 9, Rating = "* * *", },
    new { Release = 2006, Album = "Zombies! Aliens! Vampires! Dinosaurs!", Songs = 11, Rating = "* * *", }
};

Console.WriteLine(data.ToPrettierTable(separator: true));
```
Outputs:

<pre>
+---------+---------------------------------------+-------+-----------+
| Release | Album                                 | Songs | Rating    |
+---------+---------------------------------------+-------+-----------+
| 1991    | Out of Time                           | 10    | * * * *   |
+---------+---------------------------------------+-------+-----------+
| 1992    | Automatic for the People              | 8     | * * * * * |
+---------+---------------------------------------+-------+-----------+
| 1994    | Monster                               | 9     | * * *     |
+---------+---------------------------------------+-------+-----------+
| 2006    | Zombies! Aliens! Vampires! Dinosaurs! | 11    | * * *     |
+---------+---------------------------------------+-------+-----------+

(0.01 sec)
</pre>
