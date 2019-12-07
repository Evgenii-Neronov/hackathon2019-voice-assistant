namespace Lib.Models.Speech
{
    public interface IRecognizer
    {
        string Recognize(byte[] bytes);
    }
}
