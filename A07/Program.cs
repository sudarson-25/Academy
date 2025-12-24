// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to implement double.Parse method that takes a string and returns a double
// ------------------------------------------------------------------------------------------------
using static System.Console;

namespace A07;

class Program {
   static void Main () {
      Dictionary<string, double> validExp = new () { {"abc", double.NaN}, {".34", double.NaN},
         {"45.", double.NaN}, {".e2", double.NaN}, {"12.", double.NaN}, {"0", 0}, {"7", 7},
         {".1", double.NaN}, {"08.6", 8.6}, {"7897", 7897}, {"00990.009", 990.009},
         {"-7.78", -7.78}, {"-7.78e1", -77.8}, {"-7E2", -700}, {"+778e0", 778}, {"8e-3", 0.008},
         {"4e2.3", double.NaN}, {"+4e2.3", double.NaN}, {"0.003e", double.NaN}, {"x", double.NaN},
         {"", double.NaN}, {"8e9.- 3", double.NaN}, {"8 - .7e3", double.NaN},
         {"3.4e4 - .3", double.NaN}, {"3.4e4 + .3", double.NaN}, {"-35.- 354e1", double.NaN},
         {"e1", double.NaN}, {"1e", double.NaN}, {"1jkse", double.NaN}, {"1++$6e", double.NaN} };
      double result;
      foreach (var exp in validExp) {
         WriteLine ($"Input: {exp.Key}\nExpected: {exp.Value}");
         TryParse (exp.Key, out result);
         ForegroundColor = exp.Value == result || double.IsNaN (exp.Value) && double.IsNaN (result)
            ? ConsoleColor.Green : ConsoleColor.Red;
         WriteLine ($"Result: {result}\n");
         ResetColor ();
      }
      Write ($"Enter the string to be parsed: ");
      WriteLine (TryParse ((ReadLine () ?? ""), out result) ?
         $"Parsed value: {result}" : "Invalid input!");
   }

   // Tries to parse the input string into a double value
   static bool TryParse (string input, out double result) {
      input = input.Trim ().ToLower ();
      result = double.NaN;
      if (input.Length == 0 || input.Count (c => c is '+' or '-') > 1) return false;
      input = GetAbsoluteValue (input, out bool isNegative);
      if (input.Length == 0) return false;
      int eIndex = input.IndexOf ('e');
      bool hasIndex = eIndex > 0;
      string basePart = hasIndex ? input[..eIndex] : input,
         expPart = hasIndex ? input[(eIndex + 1)..] : "0";
      if (basePart.StartsWith ('.') || basePart.EndsWith ('.') ||
         basePart.Count (c => c == '.') > 1 || expPart.Length == 0) return false;
      foreach (char ch in basePart) if (ch != '.' && !char.IsDigit (ch)) return false;
      expPart = GetAbsoluteValue (expPart, out bool isExpNegative);
      if (expPart.Length == 0 || !expPart.All (char.IsDigit)) return false;
      result = GetDouble (basePart, expPart, isNegative, isExpNegative);
      return true;
   }

   // Converts the base and exponent parts into double value
   static double GetDouble (string basePart, string expPart, bool isNegative, bool isExpNegative) {
      int dotIndex = basePart.IndexOf ('.');
      double decimalFactor = 0.1, b = 0;
      for (int i = 0; i < basePart.Length; i++) {
         int digit = basePart[i] - '0';
         if (i == dotIndex) continue;
         if (dotIndex == -1 || i < dotIndex) b = b * 10 + digit;
         else {
            b += digit * decimalFactor;
            decimalFactor /= 10;
         }
      }
      int e = 0;
      foreach (char ch in expPart) e = e * 10 + (ch - '0');
      if (isExpNegative) e = -e;
      return b * Math.Pow (10, e) * (isNegative ? -1 : 1);
   }

   // Returns the absolute value of the input string and indicates if it was negative
   static string GetAbsoluteValue (string input, out bool isNegative) {
      isNegative = input[0] == '-';
      return input[0] == '+' || isNegative ? input[1..] : input;
   }
}
