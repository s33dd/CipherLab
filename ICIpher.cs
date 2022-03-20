namespace CipherLab {
  public interface ICIpher {
    public string Encode(string text, Language language);
    public string Decode(string text, Language language);
  }
}
