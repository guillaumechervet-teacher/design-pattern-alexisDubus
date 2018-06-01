using System.Collections.Generic;
using Basket.OrientedObject.Domain;

namespace Basket.OrientedObject
{
    public class BasketOperation     {     
        private readonly Infrastructure.BasketService _basketService;

        public BasketOperation(Infrastructure.BasketService basketService)
        {
            _basketService = basketService;
        } 
 
        public int CalculateAmout(IList<BasketLineArticle>  basketLineArticles)        
        {             
            Domain.Basket basket = _basketService.GetBasket(basketLineArticles);             
            return basket.Calculate();         
        }     
    } 
} 
