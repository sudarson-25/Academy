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
   // See file://Diagram.jpg for state diagram
   static (char, string, string, string) ParseFilePath (string input) {
      State s = A;
      Action none = () => { }, todo;
      char drive = '\0';
      string folders = "", file = "", extension = "";
      foreach (char ch in input.Trim () + '~') {
         bool isUpper = char.IsAsciiLetterUpper (ch), isAlphabet = char.IsAsciiLetter (ch);
         (s, todo) = (s, ch) switch {
            (A, _) when isUpper => (B, () => { drive = ch; }),
            (B, ':') => (C, none),
            (C, '\\') => (D, none),
            (D or E, _) when isAlphabet => (E, () => { folders += ch; }),
            (E, '\\') => (F, none),
            (F or G, _) when isAlphabet => (G, () => file += ch),
            (G, '\\') => (F, () => { folders += '\\' + file; file = string.Empty; }),
            (G, '.') => (H, () => { extension += ch; }),
            (H or I, _) when isAlphabet => (I, () => { extension += ch; }),
            (I, '~') => (J, none),
            _ => (Z, none),
         };
         todo ();
      }
      if (s == J) return (drive, folders, file, extension);
      throw new Exception ("Invalid input!");
   }
}

enum State { A, B, C, D, E, F, G, H, I, J, Z };