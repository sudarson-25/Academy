// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement an inverse guessing game (LSB-MSB)
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A2._2;
class Program {
   static void Main () {
      while (true) {
         Write ("Enter a secret number (1 to 100): ");
         if (int.TryParse (ReadLine (), out int secretNum) && secretNum >= 1 && secretNum <= 100)
            break;
         WriteLine ("Invalid number!");
      }
      WriteLine ($"\nSecret number: {GetSecretNum (out char[] revBin)}\nLSB->" +
         $"{new string (revBin)}<-MSB");
   }

   static int GetSecretNum (out char[] revBin) {
      int secretNum = 0, divisor = 1;
      revBin = new char[8];
      for (int i = 0; i < 8; i++) {
         divisor *= 2;
         ConsoleKey input;
         while (true) {
            Write ($"\nIs the remainder when divided by {divisor} >= {1 << i}? (Y/N): ");
            input = ReadKey ().Key;
            if (input is ConsoleKey.Y or ConsoleKey.N) break;
            Write ("\nInvalid input");
         }
         if (input == ConsoleKey.Y) {
            secretNum += 1 << i;
            revBin[i] = '1';
         } else revBin[i] = '0';
      }
      return secretNum;
   }
}