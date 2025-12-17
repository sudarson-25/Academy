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

   // Tries to parse string value into double
   static bool TryParse (string input, out double result) {
      result = double.NaN;
      if (input.Length == 0) return false;
      int count = 0;
      // Filters those inputs having successive signs
      while ((input[count] is '+' or '-') && (count < input.Length)) {
         count++;
         if (count > 1) return false;
      }
      input = GetAbsoluteValue (input, out bool isNegative);
      if (input.Length == 0) return false;
      int eIndex = input.IndexOf ('e');
      bool hasIndex = eIndex > 0;
      string basePart = hasIndex ? input[..eIndex] : input,
         expPart = hasIndex ? input[(eIndex + 1)..] : "0";
      if (basePart.StartsWith ('.') || basePart.EndsWith ('.')) return false;
      int i = 0, dotIndex = -1;
      while (i < basePart.Length) {
         char ch = basePart[i];
         if (ch == '.') {
            if (dotIndex != -1) return false;
            dotIndex = i++;
            continue;
         }
         if (ch < '0' || ch > '9') return false;
         i++;
      }
      if (expPart.Length == 0) return false;
      expPart = GetAbsoluteValue (expPart, out bool isExpNegative);
      if (expPart.Length == 0) return false;
      int j = 0;
      while (j < expPart.Length) {
         char ch = expPart[j++];
         if (ch < '0' || ch > '9') return false;
      }
      double baseValue = GetBaseValue (basePart, dotIndex);
      int expValue = GetExpValue (expPart, isExpNegative);
      result = baseValue * Math.Pow (10, expValue) * (isNegative ? -1 : 1);
      return true;
   }

   // Tries to parse string base value into double
   static double GetBaseValue (string basePart, int dotIndex) {
      int i = 0;
      double decimalFactor = 0.1, b = 0.0;
      while (i < basePart.Length) {
         int digit = basePart[i] - '0';
         if (i == dotIndex) { i++; continue; }
         if (dotIndex == -1 || i < dotIndex) b = b * 10 + digit;
         else {
            b += digit * decimalFactor;
            decimalFactor /= 10;
         }
         i++;
      }
      return b;
   }

   // Tries to parse string exponent value into double
   static int GetExpValue (string expPart, bool isExpNegative) {
      int e = 0;
      foreach (char ch in expPart) e = e * 10 + (ch - '0');
      if (isExpNegative) e = -e;
      return e;
   }

   static string GetAbsoluteValue (string input, out bool isNegative) {
      isNegative = input[0] == '-';
      if (input[0] == '+' || isNegative) return input[1..];
      return input;
   }
}