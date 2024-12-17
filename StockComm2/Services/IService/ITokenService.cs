using StockComm.Models;

namespace StockComm.Services.IService
{
    public interface ITokenService
    {
        public string CreateToken(AppUser user);
    }
}
