// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement file name parser with state machine
// ------------------------------------------------------------------------------------------------
using static System.Console;
using static A10.State;

namespace A10;

class Program {
   static void Main () {
      try {
         (char drive, string folders, string file, string extension) =
            ParseFilePath ("C:\\surprise\\folder\\readme.txt");
         WriteLine ($"Drive: {drive}\nFolders: {folders}\nFile: {file}\nExtension: {extension}");
      } catch (Exception e) { WriteLine (e.Message); }
   }

   // File path parser implemented as a state machine
   // See Diagram.jpg for state diagram
   static (char, string, string, string) ParseFilePath (string input) {
      State s = A;
      Action none = () => { }, todo;
      char drive = '\0';
      string firstFolder = "", subsequentPath = "", extension = "";
      foreach (char ch in input.Trim () + '~') {
         (s, todo) = (s, ch) switch {
            (A, >= 'A' and <= 'Z') => (B, todo = () => { drive = ch; }),
            (B, ':') => (C, none),
            (C, '\\') => (D, none),
            (D or E, >= 'A' and <= 'Z' or >= 'a' and <= 'z') => (E, todo = () => { firstFolder += ch; }),
            (E or G, '\\') => (F, todo = () => { subsequentPath += ch; }),
            (F or G, >= 'A' and <= 'Z' or >= 'a' and <= 'z') => (G, todo = () => { subsequentPath += ch; }),
            (G, '.') => (H, todo = () => { extension += ch; }),
            (H or I, >= 'A' and <= 'Z' or >= 'a' and <= 'z') => (I, todo = () => { extension += ch; }),
            (I, '~') => (J, none),
            _ => (Z, none),
         };
         todo ();
      }
      if (s == J) {
         int indexOfLastSlash = subsequentPath.LastIndexOf ('\\');
         string folders = firstFolder + subsequentPath[..indexOfLastSlash],
            file = subsequentPath[(indexOfLastSlash + 1)..];
         return (drive, folders, file, extension);
      } else throw new Exception ("Invalid input!");
   }
}

enum State { A, B, C, D, E, F, G, H, I, J, Z };