using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherLab {
  internal class Vigenere : ICIpher {
    private char[][] tabulaRecta;
    private string keyword;
    private int letters;

    private void CreateTabulaRecta(int code, int letters) {
      tabulaRecta = new char[letters][];
      //Fill first string of Tabula Recta with letters
      tabulaRecta[0] = new char[letters];
      for (int i = 0; i < letters; i++) {
        tabulaRecta[0][i] = (char)code;
        code++;
      }
      //Filling other strings with shifted previous string
      for (int i = 1; i < letters; i++) {
        tabulaRecta[i] = new char[letters];
        for (int j = letters - 1; j > 0; j--) {
          char lastElement = tabulaRecta[i - 1][letters - 1];
          tabulaRecta[i][j] = tabulaRecta[i - 1][j - 1];
          tabulaRecta[i][0] = lastElement; // First element of current strings is last element of previous
        }
      }
    }
    public string Encode(string text, Language lang) {
      int code;
      int lastCode;
      text = text.ToUpper();
      if (lang == Language.Ru) {
        letters = 32;// 32 is quantity of letters in cyrillic without [yo]
        code = (int)'А';
        lastCode = (int)'Я';
        CreateTabulaRecta(code, letters);
      }
      else {
        letters = 26;// 26 is quantity of letters in latin;
        code = (int)'A';
        lastCode = (int)'Z';
        CreateTabulaRecta(code, letters);
      }
      if (keyword == null) {
        keyword = UI.AskKeyword(lang);
      }
      string newText = "";
      for (int i = 0; i < text.Length; i++) {
        if (text[i] >= code & text[i] <= lastCode) {
          //Subtract code of the first letter in UTF-8 to get position in Alphabet
          int stringPosition = (int)(keyword[i % keyword.Length]) - code;
          int columnPosition = (int)(text[i]) - code;

          newText += tabulaRecta[stringPosition][columnPosition];
        }
        else {
          newText += text[i];
        }
      }
      return newText;
    }
    public string Decode(string text, Language lang) {
      int code;
      int lastCode;
      text = text.ToUpper();
      if (lang == Language.Ru) {
        letters = 32;// 32 is quantity of letters in cyrillic without [yo]
        code = (int)'А';
        lastCode = (int)'Я';
        CreateTabulaRecta(code, letters);
      }
      else {
        letters = 26;// 26 is quantity of letters in latin;
        code = (int)'A';
        lastCode = (int)'Z';
        CreateTabulaRecta(code, letters);
      }
      if (keyword == null) {
        keyword = UI.AskKeyword(lang);
      }
      string newText = "";
      while (text.Length > keyword.Length) {
        for (int i = 0; i < keyword.Length; i++) {
          if (text[i] >= code & text[i] <= lastCode) {
            int stringNumber = (int)keyword[i] - code;
            int position = Array.IndexOf(tabulaRecta[stringNumber], text[i]);
            char decryptedLetter = (char)(position + code);
            newText += decryptedLetter;
          }
          else {
            newText += text[i];
          }
        }
        text = text.Remove(0, keyword.Length);
      }
      for (int i = 0; i < text.Length; i++) {
        if (text[i] >= code & text[i] <= lastCode) {
          int stringNumber = (int)keyword[i] - code;
          int position = Array.IndexOf(tabulaRecta[stringNumber], text[i]);
          char decryptedLetter = (char)(position + code);
          newText += decryptedLetter;
        }
        else {
          newText += text[i];
        }
      }
      return newText;
    }
  }
}
