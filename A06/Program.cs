// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to solve the 8 queens problem
// ------------------------------------------------------------------------------------------------
using System.Text;
using static System.Console;
namespace A06;

class Program {
   static void Main () {
      OutputEncoding = new UnicodeEncoding ();
      PlaceQueen (0);
      PrintSolutions (sSolns);
   }

   // Adds a valid solution to the list of solutions.
   static void AddSolution (int[] soln) {
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
   static void PlaceQueen (int row) {
      for (sPos[row] = 0; sPos[row] < N; sPos[row]++)
         if (IsValid ())
            if (row == N - 1) AddSolution ([.. sPos]);
            else PlaceQueen (row + 1);

      // Checks if the current position of the queen is valid.
      bool IsValid () {
         for (int r = 0; r < row; r++) {
            int dy = row - r, dx = Math.Abs (sPos[row] - sPos[r]);
            if (dx == 0 || dx == dy) return false;
         }
         return true;
      }
   }

   // Prints all the valid solutions of queens on the chessboard.
   static void PrintSolutions (List<int[]> solutions) {
      int count = 1;
      foreach (var solution in solutions) {
         WriteLine ($"Solution: {count++}\n┏━━━━┳━━━━┳━━━━┳━━━━┳━━━━┳━━━━┳━━━━┳━━━━┓");
         for (int row = 0; row < N; row++) {
            Write ("┃");
            for (int col = 0; col < N; col++)
               Write (solution[col] == row ? $" ♛  ┃" : $"    ┃");
            if (row < N - 1) WriteLine ("\n┣━━━━╋━━━━╋━━━━╋━━━━╋━━━━╋━━━━╋━━━━╋━━━━┫");
         }
         WriteLine ("\n┗━━━━┻━━━━┻━━━━┻━━━━┻━━━━┻━━━━┻━━━━┻━━━━┛");
      }
   }

   const int N = 8;
   static int[] sPos = new int[N];
   static List<int[]> sSolns = [];
}
