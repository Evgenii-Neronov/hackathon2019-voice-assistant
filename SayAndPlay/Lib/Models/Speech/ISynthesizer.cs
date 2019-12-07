using System.Threading.Tasks;

namespace Lib.Models.Speech
{
    public interface ISynthesizer
    {
        Task<byte[]> SynthesizeAsync(string text);
    }
}
