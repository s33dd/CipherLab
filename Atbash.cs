using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLab {
  public class Atbash : ICIpher {
    public string Encode(string text) {
      List<int> codes = new List<int>();
      string newText = "";
      foreach (char letter in text) {
        codes.Add((int)letter);
      }
      Console.WriteLine($"{Environment.NewLine}What language do you want to encode?");
      Language language = UI.AskLang();
      if (language == Language.Ru) {
        Console.WriteLine($"{Environment.NewLine}Please pay attention that yo letter is not changing!");
        for (int i = 0; i < codes.Count; i++) {
          //changes if uppercase
          if (codes[i] >= 1040 & codes[i] <= 1071) {
            int shift = 1071 - codes[i]; // 1071 is code of the last upper cyrillic letter in UTF-8
            codes[i] = 1040 + shift; //1040 is code of the first upper cyrillic letter in UTF-8
          }
          //changes if lowercase
          else if (codes[i] >= 1072 & codes[i] <= 1103) {
            int shift = 1103 - codes[i]; // 1103 is code of the last lower cyrillic letter in UTF-8
            codes[i] = 1072 + shift; //1072 is code of the first lower cyrillic letter in UTF-8
          }
          newText += (char)codes[i];
        }
      }
      else {
        for (int i = 0; i < codes.Count; i++) {
          //changes if uppercase
          if (codes[i] >= 65 & codes[i] <= 90) {
            int shift = 90 - codes[i]; // 90 is code of the last upper latin letter in UTF-8
            codes[i] = 65 + shift; //65 is code of the first upper latin letter in UTF-8
          }
          //changes if lowercase
          else if (codes[i] >= 97 & codes[i] <= 122) {
            int shift = 122 - codes[i]; // 122 is code of the last lower latin letter in UTF-8
            codes[i] = 97 + shift; //97 is code of the first lower latin letter in UTF-8
          }
          newText += (char)codes[i];
        }
      }
      return newText;
    }

    public string Decode(string text) {
      string s = "";
      return s;
    }
  }
}
