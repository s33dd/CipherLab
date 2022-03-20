using System;
using System.Collections.Generic;

public enum InputType {
  Manual = 0,
  Random = 1,
  File = 2,
}

namespace CipherLab {
  public static class Input {
    public static List<string> GetText(List<string> text) {
      if (text == null) { 
        text = new List<string>();
      }
      text.Add(Console.ReadLine());
      Console.WriteLine($"{Environment.NewLine}Press Enter to end input. Press any other key to continue.{Environment.NewLine}");
      ConsoleKeyInfo pressedKey = Console.ReadKey();
      Console.WriteLine(Environment.NewLine);
      while (pressedKey.Key != ConsoleKey.Enter) {
        text.Add(Console.ReadLine());
        Console.WriteLine($"{Environment.NewLine}Press Enter to end input. Press any other key to continue.{Environment.NewLine}");
        pressedKey = Console.ReadKey();
        Console.WriteLine(Environment.NewLine);
      }
      int emptyCounter = 0;
      foreach (string row in text) {
        if (row.Trim() == "") {
          emptyCounter++;
        }
      }
      if (emptyCounter == text.Count) {
        text = null;
      }
      return text;
    }
  }
}
