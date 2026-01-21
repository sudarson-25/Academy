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
      Dictionary<string, (char?, string?, string?, string?)> tests = new () {
         {@"C:\surprise\folder\sdgsg\sgsg\readme.txt", ('C', @"surprise\folder\sdgsg\sgsg",
         "readme", ".txt")}, { @"Cz:\abc\def\r.txt", (null, null, null, null) },
         { @"C:\Readme.txt", (null, null, null, null) },
         { @"C:\abc\.bcf", (null, null, null, null) },
         { @"C:\abc\bcf.", (null, null, null, null) }, { @"Readme.txt", (null, null, null, null) },
         { @"C:\abc\def", (null, null, null, null) }, { @"C:\abc:d", (null, null, null, null) },
         { @"\abcd\Readme.txt", (null, null, null, null) }, { "", (null, null, null, null) },
         { @"C:\ab.c\def\r.txt", (null, null, null, null) },
         { @".\abc", (null, null, null, null) }, { @"..abc", (null, null, null, null) },
         { @"abc", (null, null, null, null) }, { @"C:\abc6\def\r.txt", (null, null, null, null) },
         { @"C:\abc\def\r.txt.txt", (null, null, null, null) },
         { @"C:\PROGRAM\DATA\MSOFFICE", (null, null, null, null) },
         { @"C:\PROGRAM\DATA", (null, null, null, null) },
         { @"C:\PROGRAM", (null, null, null, null) },
         { @"\PROGRAM\DATA\MSOFFICE\EXCEL", (null, null, null, null) },
         { @"C\PROGRAM\DATA\MSOFFICE\EXCEL", (null, null, null, null) },
         { @"PROGRAM", (null, null, null, null) },
         { @"C:PROGRAM\DATA\MSOFFICE\EXCEL", (null, null, null, null) },
         { @"G:\adc\def\ghi\jkl.cs", ('G', @"adc\def\ghi", "jkl", ".cs") }};
      WriteLine ("Running the test cases-----------\n");
      foreach (string test in tests.Keys) {
         try {
            (char drive, string folders, string file, string extension) = ParseFilePath (test);
            if ((drive, folders, file, extension) == tests[test]) {
               ForegroundColor = ConsoleColor.Green;
               WriteLine ($"Drive: {drive}\nFolders: {folders}\nFile: {file}\nExtension: {extension}\n");
            } else {
               ForegroundColor = ConsoleColor.Red;
               WriteLine ("Failed\n");
            }
            ResetColor ();
         } catch (Exception e) {
            ForegroundColor = ConsoleColor.Yellow;
            WriteLine ($"{e.Message}\n");
            ResetColor ();
         }
      }
      Write ("Enter a file path: ");
      string? input;
      do input = ReadLine (); while (input == null);
      (char drive1, string folders1, string file1, string extension1) = ParseFilePath (input);
      WriteLine ($"Drive: {drive1}\nFolders: {folders1}\nFile: {file1}\nExtension: {extension1}\n");
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
            (A, _) when isUpper => (B, () => drive = ch),
            (B, ':') => (C, none),
            (C, '\\') => (D, none),
            (D or E, _) when isAlphabet => (E, () => folders += ch),
            (E, '\\') => (F, none),
            (F or G, _) when isAlphabet => (G, () => file += ch),
            (G, '\\') => (F, () => { folders += '\\' + file; file = string.Empty; }),
            (G, '.') => (H, () => extension += ch),
            (H or I, _) when isAlphabet => (I, () => extension += ch),
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