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
      const int MIN = 0, MAX = 255;
      int secretNum;
      while (true) {
         Write ($"Enter a secret number ({MIN} to {MAX}): ");
         if (int.TryParse (ReadLine (), out secretNum) && secretNum >= MIN && secretNum <= MAX)
            break;
         WriteLine ("Invalid number!");
      }
      if (IsNumberFound (secretNum, out char[] revBin))
         WriteLine ($"\nSecret number: {secretNum}\nLSB->{new string (revBin)}<-MSB");
      else WriteLine ("\nIncorrect answers!");
   }

   // Checks if the number found by querying the user for each bit matches the secret number.
   static bool IsNumberFound (int secretNum, out char[] revBin) {
      int num = 0;
      revBin = new char[8];
      for (int i = 0; i < 8; i++) {
         ConsoleKey input;
         while (true) {
            // 1<<i gives the value of the current bit position
            Write ($"\nIs the remainder >= {1 << i} when the secret number is divided by ");
            Write ($"{1 << (i + 1)}? (Y/N) ");// 1<<(i+1) is the divisor to isolate that bit
            input = ReadKey ().Key;
            if (input is ConsoleKey.Y or ConsoleKey.N) break;
            Write ("\nInvalid input");
         }
         if (input == ConsoleKey.Y) {
            num |= 1 << i;
            revBin[i] = '1';
         } else revBin[i] = '0';
      }
      if (num == secretNum) return true;
      return false;
   }
}