// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement a simple guessing game
// ------------------------------------------------------------------------------------------------
using static System.Console;

int num = new Random ().Next (1, 101);
while (true) {
   Write ("Enter your guess (1 to 100): ");
   if (int.TryParse (ReadLine (), out int guess) && guess >= 1 && guess <= 100) {
      if (guess == num) {
         WriteLine ("You guessed correctly"); break;
      }
      WriteLine ("Your guess is too " + (guess > num ? "high" : "low"));
   } else WriteLine ("Invalid input!");
}