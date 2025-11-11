// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to build a frequency table with occurrence of the 7 most common letters
// ------------------------------------------------------------------------------------------------
namespace A04;

class Program {
   static void Main () {
      var frequencyTable = File.ReadAllText ("c:/etc/words.txt").ToUpper ().Where (char.IsLetter)
         .GroupBy (ch => ch).OrderByDescending (g => g.Count ()).Take (7);
      foreach (var group in frequencyTable) Console.WriteLine ($"{group.Key} {group.Count ()}");
   }
}