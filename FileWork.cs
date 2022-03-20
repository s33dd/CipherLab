using System;
using System.Collections.Generic;
using System.IO;

namespace CipherLab {
  public static class FileWork {
    public static bool AllowedName(string name) {
      FileInfo file = new FileInfo(name);
      if (file.Exists) {
        return true;
      }
      else {
        try {
          FileStream testFile = file.Create();
          testFile.Close();
        }
        catch {
          return false;
        }
        if (file.Exists) {
          file.Delete();
          return true;
        }
        else return false;
      }
    }

    public static bool AskOverwrite(string name) {
      FileInfo file = new FileInfo(name);
      Console.WriteLine($"{Environment.NewLine}This file is already exists! Do you want to overwrite it?");
      bool isOverwrite = UI.Ask();
      if (isOverwrite) {
        return true;
      }
      else return false;
    }

    public static void SaveToFile(string name, List<string> text) {
      FileInfo file = new FileInfo(name);
      if (file.Exists) {
        bool isOverwrite = false;
        while (file.Exists && !isOverwrite) {
          isOverwrite = AskOverwrite(name);
          if (!isOverwrite) {
            Console.WriteLine($"{Environment.NewLine}Print another name:");
            name = Console.ReadLine();
            while (!FileWork.AllowedName(name)) {
              Console.WriteLine($"{Environment.NewLine}Please, enter correct path:");
              name = Console.ReadLine();
            }
            file = new FileInfo(name);
          }
        }
        if (isOverwrite) {
          if (file.IsReadOnly) {
            Console.WriteLine($"{Environment.NewLine}File is read only. I can`t overwrite it");
            return;
          }
          file.Delete();
        }
      }
      using (StreamWriter sw = file.AppendText()) {
        foreach (string row in text) {
          sw.WriteLine(row);
        }
      }
    }

    public static List<string> ReadFromFile(string name) {
      FileInfo file = new FileInfo(name);
      List<string> text = new List<string>();
      while (!file.Exists) {
        Console.WriteLine($"{Environment.NewLine}This file doesn`t exist!");
        Console.WriteLine($"{Environment.NewLine}Print path to another file:");
        name = Console.ReadLine();
        file = new FileInfo(name);
      }
      using (StreamReader sr = file.OpenText()) {
        string line = "";
        while ((line = sr.ReadLine()) != null) {
          text.Add(line);
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
        if (text == null) {
          Console.WriteLine($"{Environment.NewLine}{Environment.NewLine}File was empty.");
        }
        return text;
      }
    }
  }
}
