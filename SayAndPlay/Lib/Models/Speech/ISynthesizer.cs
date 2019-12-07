namespace Lib.Models.Speech
{
    public interface ISynthesizer
    {
        byte[] Synthesize(string text);
    }
}
