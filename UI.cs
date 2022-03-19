﻿using System;

public enum Action {
  Encode = 0,
  Decode = 1,
  Exit = 3,
}

namespace CipherLab {
  public static class UI {
    public static string greetings = $"The second lab {Environment.NewLine}Made by: Student of 403th group Sukhoverikov Denis";
    public static string shortInfo = $"Var 6 {Environment.NewLine}Realisation of the Atbash and Vigenere ciphers on C#";
    public static string gapLine = "________________________";

    public static void StartInfo() {
      Console.Clear();
      Console.WriteLine(greetings);
      Console.WriteLine(gapLine);
      Console.WriteLine(shortInfo);
      Console.WriteLine(gapLine);
    }

    public static bool Ask() {
      bool choiseIsMade = false;
      bool choise = false;
      ConsoleKeyInfo pressedKey;
      while (!choiseIsMade) {
        Console.Write(Environment.NewLine);
        Console.Write("(Y/N)");
        pressedKey = Console.ReadKey();
        if (pressedKey.Key == ConsoleKey.Y) {
          choise = true;
          choiseIsMade = true;
        }
        else if (pressedKey.Key == ConsoleKey.N) {
          choise = false;
          choiseIsMade = true;
        }
      }
      return choise;
    }

    public static InputType AskType() {
      InputType? chosenType = null;
      ConsoleKeyInfo pressedKey;
      while (chosenType == null) {
        Console.WriteLine($"{Environment.NewLine}Press 'R' for random input, 'M' for manual input, 'F' for input from file");
        pressedKey = Console.ReadKey();
        if (pressedKey.Key == ConsoleKey.R) {
          chosenType = InputType.Random;
        }
        else if (pressedKey.Key == ConsoleKey.M) {
          chosenType = InputType.Manual;
        }
        else if (pressedKey.Key == ConsoleKey.F) {
          chosenType = InputType.File;
        }
      }
      return (InputType)chosenType;
    }

    public static Action AskAction() {
      Action? chosenAction = null;
      ConsoleKeyInfo pressedKey;
      while (chosenAction == null) {
        Console.WriteLine($"{Environment.NewLine}What do you want to do?");
        Console.WriteLine("Press E to Encode");
        Console.WriteLine("Press D to Decode");
        Console.WriteLine("Press S to Save data in file");
        Console.WriteLine("Press Esc to Exit");
        pressedKey = Console.ReadKey();
        if (pressedKey.Key == ConsoleKey.E) {
          chosenAction = Action.Encode;
        }
        else if (pressedKey.Key == ConsoleKey.D) {
          chosenAction = Action.Decode;
        }
        else if (pressedKey.Key == ConsoleKey.Escape) {
          chosenAction = Action.Exit;
        }
      }
      return (Action)chosenAction;
    }
  }
}