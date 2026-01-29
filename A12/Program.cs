// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement the Wordle game
// ------------------------------------------------------------------------------------------------
using static System.Console;
using static System.ConsoleColor;
using static System.ConsoleKey;

namespace A12;

class Program { static void Main () => new Wordle ().Run (); }

class Wordle {
   bool GameOver => mSucceeded || mFailed;

   // Initializes the game and runs the gameplay loop
   public void Run () {
      ClearScreen ();
      SelectWord ();
      DisplayBoard ();
      while (!GameOver) {
         ConsoleKeyInfo key = ReadKey (true);
         UpdateGameState (key);
         DisplayBoard ();
      }
      PrintResult ();
   }

   // Clears the screen
   void ClearScreen () {
      Clear ();
      OutputEncoding = System.Text.Encoding.Unicode;
      CursorVisible = false;
   }

   // Returns a random word from a given list of secret words
   void SelectWord () {
      mDict = File.ReadAllLines ("dict-5.txt");
      string[] seedWords = File.ReadAllLines ("puzzle-5.txt");
      mWord = seedWords[new Random ().Next (seedWords.Length)];
   }

   // Displays the wordle gameplay board
   void DisplayBoard () {
      // Guesses
      for (int r = 0; r < 6; r++) {
         SetCursorPosition (10, r);
         for (int c = 0; c < 5; c++) {
            var (Letter, Color) = mBoard[r, c];
            char ch = Letter == '\0' ? '·' : Letter;
            ConsoleColor color = (r < mRow) ? Color : Gray;
            if (c == mCol && r == mRow) ch = '◌';
            ForegroundColor = color; Write (ch);
         }
         WriteLine ();
      }
      ResetColor (); WriteLine ("-------------------------");

      // Alphabets
      SetCursorPosition (6, 7);
      for (int i = 0; i < 26; i++) {
         if (i == 13) { WriteLine (); SetCursorPosition (6, 8); }
         char ch = (char)('A' + i);
         ConsoleColor color = mLetterState.TryGetValue (ch, out ConsoleColor value) ? value : White;
         ForegroundColor = color; Write (ch);
      }

      // Error Message
      string error = (mInvalidWord != null) ? $"{mInvalidWord} is not a word" :
         new string (' ', 20);
      SetCursorPosition (3, 10);
      ForegroundColor = Yellow;
      WriteLine (error); ResetColor ();
   }

   // Updates the game state based on user input
   void UpdateGameState (ConsoleKeyInfo info) {
      mInvalidWord = null;
      if (info.Key is LeftArrow or Backspace && mCol > 0) { mBoard[mRow, --mCol] = ('\0', Gray); return; }
      if (info.Key is Enter && mCol == 5) {
         char[] word = new char[5];
         for (int c = 0; c < 5; c++) word[c] = mBoard[mRow, c].Letter;
         string guess = new (word);
         if (!mDict.Contains (guess)) { mInvalidWord = guess; return; }
         mCol = 0;
         Dictionary<char, int> remaining = mWord.GroupBy (c => c)
            .ToDictionary (g => g.Key, g => g.Count ());
         // Greens
         for (int c = 0; c < 5; c++) {
            char character = mBoard[mRow, c].Letter;
            if (mWord[c] == character) {
               SetColor (mRow, c, character, Green);
               remaining[character]--;
            }
         }
         // Blues / Grays
         for (int c = 0; c < 5; c++) {
            if (mBoard[mRow, c].Color == Green) continue;
            char character = mBoard[mRow, c].Letter;
            if (remaining.TryGetValue (character, out int count) && count > 0) {
               SetColor (mRow, c, character, Blue);
               remaining[character]--;
            } else SetColor (mRow, c, character, DarkGray);
         }
         if (guess == mWord) mSucceeded = true;
         else if (++mRow == 6) mFailed = true;
         return;
      }
      char ch = char.ToUpper (info.KeyChar);
      if (ch is >= 'A' and <= 'Z' && mCol < 5) { mBoard[mRow, mCol++].Letter = ch; return; }

      // Colors a grid cell and updates the alphabet accordingly
      void SetColor (int row, int col, char ch, ConsoleColor color) {
         mBoard[row, col].Color = color;
         if (!mLetterState.TryGetValue (ch, out ConsoleColor value)) mLetterState[ch] = color;
         else if (value != Green) mLetterState[ch] = color;
      }
   }

   // Prints the result
   void PrintResult () => WriteLine (mSucceeded ? " You won!" : $" You lost. Word was {mWord}");

   (char Letter, ConsoleColor Color)[,] mBoard = new (char, ConsoleColor)[6, 5];
   string[] mDict = [];
   string? mInvalidWord;
   string mWord = "";
   int mRow = 0, mCol = 0;
   bool mSucceeded, mFailed;
   Dictionary<char, ConsoleColor> mLetterState = [];
}
