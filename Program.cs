using System;
using System.Collections.Generic;

namespace CipherLab {
  public class Program {
    static void Main(string[] args) {
      UI.StartInfo();
      List<string> cryptedText = new List<string>();
      List<string> text = new List<string>();
      text = null;
      cryptedText = null;
      while (true) {
        Action action = UI.AskAction();
        if (action == Action.Encode) {
          if (text == null) {
            Console.WriteLine($"{Environment.NewLine}There is nothing to encrypt.");
            continue;
          }
          Cipher cipher = UI.AskCipher();
          if (cipher == Cipher.Atbash) {
            Atbash crypter = new Atbash();
            cryptedText = new List<string>();
            Language language = UI.AskLang();
            foreach (string row in text) {
              cryptedText.Add(crypter.Encode(row, language));
            }
          }
          else {
            //Vigenere is not ready yet
          }
        }
        else if (action == Action.Decode) {
          if (text == null) {
            Console.WriteLine($"{Environment.NewLine}There is nothing to decrypt.");
            continue;
          }
          Cipher cipher = UI.AskCipher();
          if (cipher == Cipher.Atbash) {
            Atbash crypter = new Atbash();
            cryptedText = new List<string>();
            Language language = UI.AskLang();
            foreach (string row in text) {
              cryptedText.Add(crypter.Decode(row, language));
            }
          }
          else {
            //Vigenere is not ready yet
          }
        }
        else if (action == Action.InputText) {
          if (text != null) {
            Console.WriteLine($"{Environment.NewLine}Do you want to clear previous text?");
            if (UI.Ask() == true) {
              text = null;
            }
          }
          InputType type = UI.AskType();
          if (type == InputType.Manual) {
            Console.WriteLine($"{Environment.NewLine}Print your text.");
            text = Input.GetText(text);
          }
          else {
            Console.WriteLine($"{Environment.NewLine}Print path to file:");
            string path = Console.ReadLine();
            text = FileWork.ReadFromFile(path);
          }
        }
        else if (action == Action.ShowOriginText) {
          if (text == null) {
            Console.WriteLine($"{Environment.NewLine}There is nothing to show.");
            continue;
          }
          Console.WriteLine(Environment.NewLine);
          UI.ShowText(text);
        }
        else if (action == Action.ShowCryptedText) {
          if (cryptedText == null) {
            Console.WriteLine($"{Environment.NewLine}There is nothing to show.");
            continue;
          }
          Console.WriteLine(Environment.NewLine);
          UI.ShowText(cryptedText);
        }
        else if (action == Action.SaveOrigin) {
          if (text == null) {
            Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Nothing to save!");
            continue;
          }
          Console.WriteLine($"{Environment.NewLine}Print path to file:");
          string path = Console.ReadLine();
          while (!FileWork.AllowedName(path)) {
            Console.WriteLine($"{Environment.NewLine}Please, enter correct path:");
            path = Console.ReadLine();
          }
          FileWork.SaveToFile(path, text);
        }
        else if (action == Action.SaveCrypted) {
          if (cryptedText == null) {
            Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}Nothing to save!");
            continue;
          }
          Console.WriteLine($"{Environment.NewLine}Print path to file:");
          string path = Console.ReadLine();
          while (!FileWork.AllowedName(path)) {
            Console.WriteLine($"{Environment.NewLine}Please, enter correct path:");
            path = Console.ReadLine();
          }
          FileWork.SaveToFile(path, cryptedText);
        }
        else if (action == Action.Exit) {
          Console.WriteLine($"{Environment.NewLine}Do you really want to close the program?");
          bool isExit = UI.Ask();
          if (isExit) {
            Environment.Exit(0);
          }
        }
      }
    }
  }
}