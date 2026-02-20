// ------------------------------------------------------------------------------------------------
// Training ~ A training program for new joinees at Metamation, Batch - July 2025.
// Copyright (c) Metamation India.
// ------------------------------------------------------------------
// Program.cs
// Program to find all the anagrams and sort them based on count in O(n) time complexity.
// ------------------------------------------------------------------------------------------------
namespace A14;

class Program {
   static void Main () {
      var anagram = new Dictionary<string, List<string>> ();
      foreach (var word in File.ReadAllLines ("words.txt")) {
         var key = new string ([.. word.Order ()]);
         if (!anagram.TryGetValue (key, out var list)) anagram.Add (key, list = []);
         list.Add (word);
      }
      File.WriteAllLines ("anagram.txt", anagram.Where (kv => kv.Value.Count > 1)
         .OrderByDescending (kv => kv.Value.Count)
         .Select (kv => $"{kv.Value.Count} {string.Join (' ', kv.Value)}"));
   }
}