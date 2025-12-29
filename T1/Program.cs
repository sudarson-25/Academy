using static System.Console;

namespace T1;

class Program {
   static void Main () {
      while (true) {
         Write ("Input: ");
         string input;
         do input = ReadLine ()!; while (input == null);
         DoAction (input.Trim (), commands);
      }
   }

   static void DoAction (string input, Stack<(char, string)> commands) {
      if (input.Length > 3 && input[..3] is "ADD") {
         commands.Push (('A', input[3..]));
         text += input[3..].Trim ();
         return;
      }
      if (input.Length > 6 && input[..6] is "DELETE") {
         if (!int.TryParse (input[6..].Trim (), out int number) || number > text.Length) return;
         commands.Push (('D', text[(text.Length - number)..])); //Pushing deleted text
         text = text[..(text.Length - number)];
         return;
      }
      if (input is "SHOW") Console.WriteLine (text);
      if (input is "EXIT") return;
      if (input is "UNDO") {
         if (commands is not null) {
            (char command, string content) = commands.Pop ();
            if (command is 'A') DoAction ("DELETE" + content.Length, undo);
            if (command is 'D') DoAction ("ADD" + content, undo);
         }
         return;
      }
      if (input is "REDO") {
         if (undo is not null) {
            (char command, string content) = undo.Pop ();
            if (command is 'A') DoAction ("DELETE" + content.Length, commands);
            if (command is 'D') DoAction ("ADD" + content, commands);
         }
         return;
      }
   }

   static string text = "";
   static Stack<(char, string)> commands = new (), undo = new ();
}