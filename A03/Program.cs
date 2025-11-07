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
      foreach (var (Score, Word, Pangram) in words.Where (IsValid).Select (GetScore)
                                                  .OrderByDescending (a => a.Score)
                                                  .ThenBy (a => a.Word)) {
         if (Pangram) ForegroundColor = ConsoleColor.Green;
         WriteLine ($"{Score,3}. {Word}");
         ResetColor ();
         total += Score;
      }
      WriteLine ($"----\n{total,3} total");
   }

   static bool IsValid (string word) {
      if (word.Length >= 4 && word.Contains (sLetters[0]) &&
         word.All (sLetters.Contains)) return true;
      return false;
   }

   static (int Score, string Word, bool Pangram) GetScore (string word) {
      bool pangram = sLetters.All (word.Contains);
      int score = (word.Length > 4 ? word.Length : 1) + (pangram ? 7 : 0);
      return (score, word, pangram);
   }

   static char[] sLetters = ['U', 'X', 'A', 'L', 'T', 'N', 'E'];
}