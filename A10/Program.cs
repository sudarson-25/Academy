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
            ParseFilePath ("C:\\surprise\\folder\\sdgsg\\sgsg\\readme.txt");
         WriteLine ($"Drive: {drive}\nFolders: {folders}\nFile: {file}\nExtension: {extension}");
      } catch (Exception e) { WriteLine (e.Message); }
   }

   // File path parser implemented as a state machine
   // See Diagram.jpg for state diagram
   static (char, string, string, string) ParseFilePath (string input) {
      State s = A;
      Action none = () => { }, todo;
      char drive = '\0';
      string firstFolder = "", file = "", extension = "";
      foreach (char ch in input.Trim () + '~') {
         (s, todo) = (s, ch) switch {
            (A, >= 'A' and <= 'Z') => (B, () => { drive = ch; }),
            (B, ':') => (C, none),
            (C, '\\') => (D, none),
            (D or E, >= 'A' and <= 'Z' or >= 'a' and <= 'z') => (E, () => { firstFolder += ch; }),
            (E, '\\') => (F, none),
            (G, '\\') => (F, () => { file += ch; }),
            (F or G, >= 'A' and <= 'Z' or >= 'a' and <= 'z') => (G, () => { file += ch; }),
            (G, '.') => (H, () => { extension += ch; }),
            (H or I, >= 'A' and <= 'Z' or >= 'a' and <= 'z') => (I, () => { extension += ch; }),
            (I, '~') => (J, none),
            _ => (Z, none),
         };
         todo ();
      }
      if (s == J) {
         int indexOfLastSlash = file.LastIndexOf ('\\');
         string folders = firstFolder + '\\' + file[..indexOfLastSlash],
            fileName = file[(indexOfLastSlash + 1)..];
         return (drive, folders, fileName, extension);
      } else throw new Exception ("Invalid input!");
   }
}

enum State { A, B, C, D, E, F, G, H, I, J, Z };