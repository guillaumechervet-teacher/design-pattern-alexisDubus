using System.Collections.Generic;
using Basket.OrientedObject.Infrastructure;

namespace Basket.OrientedObject
{
    public class BasketOperation
    {
        private readonly BasketService _basketService;

        public BasketOperation(BasketService basketService)
        {
            _basketService = basketService;
        }

        public int CalculateAmout(IList<BasketLineArticle> basketLineArticles)
        {
            var basket = _basketService.GetBasket(basketLineArticles);
            return basket.Calculate();
        }
    }
}