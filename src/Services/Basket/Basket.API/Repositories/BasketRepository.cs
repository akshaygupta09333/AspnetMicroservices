using System.Security.AccessControl;
using Basket.API.Entities;
using Basket.API.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Memory;

namespace Basket.API.Repositories
{
    public class BasketRepository : IBasketRepository
    {

        readonly private IMemoryCache _memoryCache;
        public BasketRepository(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            ShoppingCart cart = new ShoppingCart(userName);
            _memoryCache.TryGetValue(userName, out ShoppingCart shoppingCart);
            if (shoppingCart != null)
            {
                cart = shoppingCart;
            }
            return cart;
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            _memoryCache.TryGetValue(basket.UserName, out ShoppingCart shoppingCart);
            if (shoppingCart != null)
            {
                _memoryCache.Remove(basket.UserName);
            }
            _memoryCache.Set(basket.UserName, basket);
            return basket;
        }

        public async Task DeleteBasket(string userName)
        {
            _memoryCache.Remove(userName);
        }
    }
}
