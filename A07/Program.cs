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
      Write ($"Enter the string to be parsed: ");
      WriteLine (TryParse ((ReadLine () ?? "").Trim ().ToLower (), out double result) ?
         $"Parsed value: {result}" : "Invalid input!");
   }

   // Tries to parse the input string into a double value
   static bool TryParse (string input, out double result) {
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
         if (i == dotIndex) { i++; continue; }
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
