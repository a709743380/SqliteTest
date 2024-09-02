using WebApplication4.Models;

namespace WebApplication4.Interface
{
    public interface ICrypto
    {
        CryptoModel DoCrypto(CryptoModel model);
    }
}
