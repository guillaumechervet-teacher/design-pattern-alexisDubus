using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Basket;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
        public void ReturnCorrectAmoutGivenBasket(BasketTest basketTest)
        {
            var amountTotal = 0;
            Assert.AreEqual(amountTotal, basketTest.ExpectedPrice);
        }

        public class BasketTest
        {
            public List<BasketLineArticle> BasketLineArticles { get; set; }
            public int ExpectedPrice { get; set; }
        }
    }
}