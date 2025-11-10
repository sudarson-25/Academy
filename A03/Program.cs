// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to find all valid Spelling Bee words form the given dictionary
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A03;

class Program {
   static void Main () {
      int total = 0;
      string[] words = File.ReadAllLines ("C:/etc/words.txt");
      var results = words.Where (IsValid).Select (GetScore).OrderByDescending (a => a.Score)
         .ThenBy (a => a.Word);
      foreach (var result in results) {
         if (result.IsPangram) ForegroundColor = ConsoleColor.Green;
         WriteLine ($"{result.Score,3}. {result.Word}");
         ResetColor ();
         total += result.Score;
      }
      WriteLine ($"----\n{total,3} total");
   }

   static bool IsValid (string word) => word.Length >= 4 && word.Contains ('U') &&
         word.All (sLetters.Contains);

   static (int Score, string Word, bool IsPangram) GetScore (string word) {
      bool pangram = sLetters.All (word.Contains);
      return ((word.Length > 4 ? word.Length : 1) + (pangram ? 7 : 0), word, pangram);
   }

   static char[] sLetters = ['U', 'X', 'A', 'L', 'T', 'N', 'E'];
}