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

    public static void SaveToFile(string name) {
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
      //Not ready yet
    }

  }
}
