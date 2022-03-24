using System;
using System.Collections.Generic;

public enum Action {
  Encode = 0,
  Decode = 1,
  SaveOrigin = 2,
  SaveCrypted = 3,
  InputText = 4,
  ShowCryptedText = 5,
  ShowOriginText = 6,
  Exit = 7,
}

public enum Language {
  Eng = 0,
  Ru = 1,
}

public enum Cipher {
  Atbash = 0,
  Vigenere = 1,
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
        Console.WriteLine($"{Environment.NewLine}Press 'M' for manual input, 'F' for input from file");
        pressedKey = Console.ReadKey();
        if (pressedKey.Key == ConsoleKey.M) {
          chosenType = InputType.Manual;
        }
        else if (pressedKey.Key == ConsoleKey.F) {
          chosenType = InputType.File;
        }
      }
      return (InputType)chosenType;
    }

    public static Language AskLang() {
      Language? choosenLanguage = null;
      ConsoleKeyInfo pressedKey;
      while (choosenLanguage == null) {
        Console.WriteLine($"{Environment.NewLine}Press R for Russian language");
        Console.WriteLine("Press E for English language");
        pressedKey = Console.ReadKey();
        if (pressedKey.Key == ConsoleKey.R) {
          choosenLanguage = Language.Ru;
        }
        else if (pressedKey.Key == ConsoleKey.E) {
          choosenLanguage = Language.Eng;
        }
      }
      return (Language)choosenLanguage;
    }

    public static Action AskAction() {
      Action? chosenAction = null;
      ConsoleKeyInfo pressedKey;
      while (chosenAction == null) {
        Console.WriteLine($"{Environment.NewLine}What do you want to do?");
        Console.WriteLine("Press I to Input text");
        Console.WriteLine("Press E to Encode");
        Console.WriteLine("Press D to Decode");
        Console.WriteLine("Press O to Show putted text");
        Console.WriteLine("Press C to Show modified text");
        Console.WriteLine("Press S to Save initial data in file");
        Console.WriteLine("Press Shift + S to Save modified data in file");
        Console.WriteLine("Press Shift + E to Exit");
        pressedKey = Console.ReadKey();
        if (pressedKey.Key == ConsoleKey.I) {
          chosenAction = Action.InputText;
        }
        else if (pressedKey.Key == ConsoleKey.E & pressedKey.Modifiers != ConsoleModifiers.Shift) {
          chosenAction = Action.Encode;
        }
        else if (pressedKey.Key == ConsoleKey.D) {
          chosenAction = Action.Decode;
        }
        else if (pressedKey.Key == ConsoleKey.O) {
          chosenAction = Action.ShowOriginText;
        }
        else if (pressedKey.Key == ConsoleKey.C) {
          chosenAction = Action.ShowCryptedText;
        }
        else if (pressedKey.Key == ConsoleKey.S & pressedKey.Modifiers != ConsoleModifiers.Shift) {
          chosenAction = Action.SaveOrigin;
        }
        else if (pressedKey.Key == ConsoleKey.S & pressedKey.Modifiers == ConsoleModifiers.Shift) {
          chosenAction = Action.SaveCrypted;
        }
        else if (pressedKey.Key == ConsoleKey.E & pressedKey.Modifiers == ConsoleModifiers.Shift) {
          chosenAction = Action.Exit;
        }
      }
      return (Action)chosenAction;
    }

    public static void ShowText(List<string> text) {
      foreach (string row in text) {
        Console.WriteLine(row);
      }
    }

    public static Cipher AskCipher() {
      Cipher? cipher = null;
      ConsoleKeyInfo pressedKey;
      while (cipher == null) {
        Console.WriteLine($"{Environment.NewLine}Which cipher do you want to use?");
        Console.WriteLine("Press A for Atbash");
        Console.WriteLine("Press V for Vigenere");
        pressedKey = Console.ReadKey();
        if (pressedKey.Key == ConsoleKey.A) {
          cipher = Cipher.Atbash;
        }
        else if (pressedKey.Key == ConsoleKey.V) {
          cipher = Cipher.Vigenere;
        }
      }
      return (Cipher)cipher;
    }

    public static string AskKeyword(Language lang) {
      Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Please, enter the keyword or print 'RANDOM!' for random generation.");
      Console.WriteLine("Useless for decoding but maybe you`re lucky and will get right keyword.");
      string keyword = Console.ReadLine().ToUpper();
      if (keyword == "RANDOM!") {
        Random random = new Random();
        keyword = "";
        int upperBorder = 19;
        int lowerBorder = 7;
        int quantity = random.Next(lowerBorder, upperBorder);
        if (lang == Language.Ru) {
          lowerBorder = (int)'А';
          upperBorder = (int)'Я';
        }
        else {
          lowerBorder = (int)'A';
          upperBorder = (int)'Z';
        }
        for (int i = 0; i < quantity; i++) {
          keyword += (char)random.Next(lowerBorder, upperBorder);
        }
        Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}{keyword} — is your keyword.");
        Console.WriteLine("Remember it! Without it you can`t decode your message in a simple way.");
        return keyword;
      }
      bool badSymbol = true;
      bool isShowed = false;
      if (lang == Language.Ru) {
        while (badSymbol) {
          for (int i = 0; i < keyword.Length; i++) {
            if ((int)keyword[i] < 1040 | (int)keyword[i] > 1071) { //Borders of first and last cyrillic letters in UTF-8
              badSymbol = true;
            }
            else {
              badSymbol = false;
            }
          }
          if (badSymbol & !isShowed) {
            Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}There is wrong symbols in your keyword. Use only cyrillic letters without Yo.");
            isShowed = true;
            keyword = Console.ReadLine().ToUpper();
          }
        }
        return keyword;
      }
      else {
        while (badSymbol) {
          for (int i = 0; i < keyword.Length; i++) {
            if ((int)keyword[i] < 65 | (int)keyword[i] > 90) { //Borders of first and last latin letters in UTF-8
              badSymbol = true;
            }
            else {
              badSymbol = false;
            }
          }
          if (badSymbol & !isShowed) {
            Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}There is wrong symbols in your keyword. Use only latin letters.");
            isShowed = true;
            keyword = Console.ReadLine().ToUpper();
          }
        }
        return keyword;
      }
    }
  }
}
