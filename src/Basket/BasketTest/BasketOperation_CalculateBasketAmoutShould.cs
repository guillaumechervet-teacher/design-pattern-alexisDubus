using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Basket;
using Basket.OrientedObject;
using Basket.OrientedObject.Infrastructure;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace BasketTest
{
    public class BasketOperation_CalculateBasketAmoutShould
    {
        private static IEnumerable<object[]> Baskets => new[]
        {
            new object[]
            {
                new BasketTest
                {
                    BasketLineArticles = new List<BasketLineArticle>
                    {
                        new BasketLineArticle {Id = "1", Number = 12, Label = "Banana"},
                        new BasketLineArticle {Id = "2", Number = 1, Label = "Fridge electrolux"},
                        new BasketLineArticle {Id = "3", Number = 4, Label = "Chair"}
                    },
                    ExpectedPrice = 84868
                }
            },
            new object[]
            {
                new BasketTest
                {
                    BasketLineArticles = new List<BasketLineArticle>
                    {
                        new BasketLineArticle {Id = "1", Number = 20, Label = "Banana"},
                        new BasketLineArticle {Id = "3", Number = 6, Label = "Chair"}
                    },
                    ExpectedPrice = 37520
                }
            }
        };

        [TestMethod]
        [DynamicData("Basket")]
        public void ReturnCorrectAmoutGivenBasket1(BasketTest basketTest)
        {
            var amountTotal = 0;
            Assert.AreEqual(amountTotal, basketTest.ExpectedPrice); 
        }

        public class BasketTest
        {
            public List<BasketLineArticle> BasketLineArticles { get; set; }
            public int ExpectedPrice { get; set; }
        }
        [TestMethod]         
        [DynamicData("Baskets")]        
        public void ReturnCorrectAmoutGivenBasket(BasketTest basketTest)         
        {             
            var amountTotal = 0;            
            foreach (var basketLineArticle in basketTest.BasketLineArticles)             {     
                // Retrive article from database
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;             
                var uri = new UriBuilder(codeBase);                 
                var path = Uri.UnescapeDataString(uri.Path);   
                var assemblyDirectory = Path.GetDirectoryName(path);     
                var jsonPath = Path.Combine(assemblyDirectory, "articledatabase.json");        
                IList<ArticleDatabase> articleDatabases = JsonConvert.DeserializeObject<List<ArticleDatabase>>(File.ReadAllText(jsonPath));  
                var article = articleDatabases.First(articleDatabase => articleDatabase.Id == basketLineArticle.Id); 
 
            // Calculate amount
            var amount = 0;              
                switch (article.Category)                
                {
            case "food":                         amount += article.Price * 100 + article.Price * 12;                        
                break;                     case "electronic":                  
                        amount += article.Price * 100 + article.Price * 20 + 4;    
                        break; 
            case "desktop":                        
                amount += article.Price * 100 + article.Price * 20;                       
                break;
                    
                }          
                amountTotal += amount * basketLineArticle.Number;
                
            } 
 
        Assert.AreEqual(amountTotal, basketTest.ExpectedPrice);
            
        }

        [TestMethod]
        [DynamicData("Baskets")]
        public void ReturnCorrectAmoutGivenBasketBis(BasketTest basketTest)
        {
            var basKetService = new BasketService();          
            var basketOperation = new BasketOperation(basKetService);      
            var amountTotal = basketOperation.CalculateAmout(basketTest.BasketLineArticles);      
            Assert.AreEqual(amountTotal, basketTest.ExpectedPrice);
        } 
    }
}