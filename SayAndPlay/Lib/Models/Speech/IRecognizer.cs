using System.Threading.Tasks;

namespace Lib.Models.Speech
{
    public interface IRecognizer
    {
        Task<string> RecognizeAsync(byte[] bytes);
    }
}
