using static System.Console;

namespace T2;

class Program {
   static void Main () {
      WriteLine ("Input: ");
      string[] lines = File.ReadAllLines ("C:\\Work\\Academy\\T2\\dial.txt");
      int[] circularDial = new int[100];
      for (int i = 0; i < circularDial.Length; i++) circularDial[i] = i;
      int start = 50;
      foreach (string line in lines) {
         (char dir, int move) = (line[0], int.Parse (line[1..]));
      }
   }
}