// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement a simple guessing game
// ------------------------------------------------------------------------------------------------
using static System.Console;

const int MIN = 1, MAX = 100;
int num = new Random ().Next (MIN, MAX + 1), i;
for (i = 0; i < 7; i++) {
   Write ($"Enter your guess ({MIN} to {MAX}): ");
   if (int.TryParse (ReadLine (), out int guess) && guess >= MIN && guess <= MAX) {
      if (guess == num) { WriteLine ("You guessed correctly"); break; }
      WriteLine ($"Your guess is too {(guess > num ? "high" : "low")}");
   } else WriteLine ("Invalid input!");
}
if (i == 7) WriteLine ($"Maximum attempts reached!\nThe correct number is {num}");