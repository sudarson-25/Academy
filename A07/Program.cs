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
      WriteLine (TryParse (ReadLine () ?? "".Trim ()
         .ToLower (), out double result) ? $"Parsed value: {result}" : "Invalid input!");
   }

   // Tries to parse string value into double
   static bool TryParse (string input, out double result) {
      result = double.NaN;
      if (input == "") return false;
      int count = 0;
      // Filters those inputs having successive signs
      while ((input[count] is '+' or '-') && (count < input.Length)) {
         count++;
         if (count > 1) return false;
      }
      bool isNegative = input[0] == '-';
      if (input[0] == '+' || isNegative) input = input[1..];
      int eIndex = input.IndexOfAny (['e', 'E']);
      bool hasIndex = eIndex > 0;
      string basePart = hasIndex ? input[..eIndex] : input,
         expPart = hasIndex ? input[(eIndex + 1)..] : "0";
      bool isBaseParsed = TryParseBase (basePart, out double b),
         isExpParsed = TryParseExp (expPart, out double e);
      if (isBaseParsed && isExpParsed) result = b * Math.Pow (10, e) * (isNegative ? -1 : 1);
      else return false;
      return true;
   }

   // Tries to parse string base value into double
   static bool TryParseBase (string basePart, out double b) {
      bool hasDecimal = false;
      double decimalFactor = 0.1;
      int i = 0;
      if (basePart.EndsWith ('.')) {
         b = double.NaN;
         return false;
      }
      if (basePart.StartsWith ('.')) { hasDecimal = true; i++; }
      b = 0.0;
      while (i < basePart.Length) {
         char ch = basePart[i];
         if (ch == '.') {
            if (hasDecimal) {
               b = double.NaN;
               return false;
            }
            hasDecimal = true;
            i++;
            continue;
         }
         if (ch < '0' || ch > '9') {
            b = double.NaN;
            return false;
         }
         int digit = ch - '0';
         if (!hasDecimal) b = b * 10 + digit;
         else { b += digit * decimalFactor; decimalFactor /= 10; }
         i++;
      }
      return true;
   }

   // Tries to parse string exponent value into double
   static bool TryParseExp (string expPart, out double e) {
      e = 0;
      int i = 0;
      bool isNegative = expPart[i] == '-';
      if (expPart[i] == '+' || isNegative) i++;
      if (i == expPart.Length) return false;
      while (i < expPart.Length) {
         char ch = expPart[i++];
         if (ch < '0' || ch > '9') return false;
         e = e * 10 + (ch - '0');
      }
      if (isNegative) e *= -1;
      return true;
   }
}