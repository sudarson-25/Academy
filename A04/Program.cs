// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to build a frequency table with occurrence of the 7 most common letters
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A04;

class Program {
   static void Main () {
      string text = File.ReadAllText ("c:/etc/words.txt");
      Dictionary<char, int> frequencyTable = [];
      foreach (char ch in text)
         if (char.IsLetter (ch) && !frequencyTable.TryAdd (ch, 1)) frequencyTable[ch]++;
      var keys = frequencyTable.OrderByDescending (pair => pair.Value).Take (7)
         .ToDictionary ().Keys;
      foreach (char key in keys) WriteLine ($"{key} {frequencyTable[key]}");
   }
}