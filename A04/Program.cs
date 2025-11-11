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
      string text = File.ReadAllText ("c:/etc/words.txt");
      Dictionary<char, int> frequencyTable = [];
      text = text.ToUpper ();
      foreach (char ch in text)
         if (char.IsLetter (ch)) frequencyTable[ch] = frequencyTable.GetValueOrDefault (ch) + 1;
      var keys = frequencyTable.OrderByDescending (pair => pair.Value).Take (7)
         .ToDictionary ().Keys;
      foreach (char key in keys) Console.WriteLine ($"{key} {frequencyTable[key]}");
   }
}