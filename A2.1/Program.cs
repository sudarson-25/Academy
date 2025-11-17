// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement an inverse guessing game (MSB-LSB)
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A2._1;

class Program {
   static void Main () {
      const int MIN = 1, MAX = 100;
      int secretNum, guess = 0;
      while (true) {
         Write ($"Enter a secret number ({MIN} to {MAX}): ");
         if (int.TryParse (ReadLine (), out secretNum) && secretNum >= MIN && secretNum <= MAX) break;
         WriteLine ("Invalid number!");
      }
      int left = MIN, right = MAX;
      while (left <= right) {
         guess = (right + left) / 2;
         WriteLine ($"\nComputer's guess: {guess}");
         if (guess == secretNum) break;
         ConsoleKey input;
         while (true) {
            Write ("User's response (H)igh / (L)ow: ");
            input = ReadKey ().Key;
            if (input is ConsoleKey.H or ConsoleKey.L) break;
            WriteLine ("\nInvalid input!");
         }
         if (input is ConsoleKey.L) left = guess + 1;
         else right = guess - 1;
      }
      WriteLine (guess == secretNum ? "Computer guessed correctly" : $"\nGame over!" +
         $"\nThe secret number is {secretNum}");
   }
}
