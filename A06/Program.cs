// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to solve the 8 queens problem
// ------------------------------------------------------------------------------------------------
using static System.Console;
namespace A06;

class Program {
   static void Main () {
      while (true) {
         Write ("Menu\n~~~~\n1.Print all the solutions\n2.Print unique solutions" +
            "\nEnter your choice: ");
         if (!int.TryParse (ReadLine (), out int choice) || (choice != 1 && choice != 2)) {
            WriteLine ("Invalid input!\n"); continue;
         }
         sSolns.Clear ();
         if (choice == 1) PlaceQueen (0, false);
         else PlaceQueen (0, true);
         PrintSolutions (sSolns);
         WriteLine ("Do you want to continue (y/n)?");
         if (ReadKey (true).Key is not ConsoleKey.Y) break;
         Clear ();
      }
   }

   // Adds a valid solution to the list of solutions.
   static void AddSolution (int[] soln, bool unique) {
      if (unique)
         for (int i = 0; i < 4; i++) {
            soln = Rotate (soln);
            if (Exists (soln) || Exists (VMirror (soln))) return;
         }
      sSolns.Add (soln);

      // Checks if the solution exists.
      bool Exists (int[] a) => sSolns.Any (s => s.SequenceEqual (a));

      // Returns the rotated variation of the solution.
      int[] Rotate (int[] a) {
         int[] tmp = new int[N];
         for (int i = 0; i < N; i++) tmp[a[i]] = N - i - 1;
         return tmp;
      }

      // Returns the vertical symmetric variation of the solution.
      int[] VMirror (int[] a) => [.. a.Reverse ()];
   }

   // Recursive function to place queens on the chessboard.
   static void PlaceQueen (int row, bool unique) {
      for (sPos[row] = 0; sPos[row] < N; sPos[row]++)
         if (IsValid ())
            if (row == N - 1) AddSolution ([.. sPos], unique);
            else PlaceQueen (row + 1, unique);

      // Checks if the current position of the queen is valid.
      bool IsValid () {
         for (int r = 0; r < row; r++) {
            int dx = Math.Abs (sPos[row] - sPos[r]);
            if (dx == 0 || dx == row - r) return false;
         }
         return true;
      }
   }

   // Prints all the valid solutions of queens on the chessboard.
   static void PrintSolutions (List<int[]> solutions) {
      OutputEncoding = new System.Text.UnicodeEncoding ();
      for (int i = 0; i < solutions.Count; i++) {
         Clear ();
         WriteLine ($"Solution: {i + 1}\n┏━━━━┳━━━━┳━━━━┳━━━━┳━━━━┳━━━━┳━━━━┳━━━━┓");
         for (int row = 0; row < N; row++) {
            Write ("┃");
            for (int col = 0; col < N; col++)
               Write (solutions[i][col] == row ? $" ♛  ┃" : $"    ┃");
            if (row < N - 1) WriteLine ("\n┣━━━━╋━━━━╋━━━━╋━━━━╋━━━━╋━━━━╋━━━━╋━━━━┫");
         }
         WriteLine ("\n┗━━━━┻━━━━┻━━━━┻━━━━┻━━━━┻━━━━┻━━━━┻━━━━┛\n-> (Next)\n<- (Previous)" +
            "\nPress any other key to exit printing solutions");
         var key = ReadKey (true);
         if (key.Key is ConsoleKey.LeftArrow) i -= i > 0 ? 2 : 1;
         else if (key.Key is ConsoleKey.RightArrow) {
            if (i == solutions.Count - 1) i--;
         } else break;
      }
   }

   const int N = 8;
   static int[] sPos = new int[N];
   static List<int[]> sSolns = [];
}
