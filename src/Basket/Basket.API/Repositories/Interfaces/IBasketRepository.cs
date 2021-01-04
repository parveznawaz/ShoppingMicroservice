using System.Threading.Tasks;
using Basket.API.Entities;

namespace Basket.API.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketCart> GetBasket(string userName);
        Task<BasketCart> UpdateBasket(BasketCart basket);
        Task<bool> DeleteBasket(string userName);

    }
}
