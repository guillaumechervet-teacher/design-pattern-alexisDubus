using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Basket
{
    public class ImperativeProgramming     { 
 
        public static int CalculateBasketAmount(IList<BasketLineArticle> basketLineArticle) {          
            var amountTotal = 0;            
            foreach (var oneBasketLineArticle in basketLineArticle)             {     
                // Retrive article from database

                #if DEBUG         
                    var article = GetArticleDatabaseMock(oneBasketLineArticle.Id); 
                #else                
                    var article = GetArticleDatabase(oneBasketLineArticle.Id); 
                #endif  
 
                // Calculate amount
                var amount = 0;              
                switch (article.Category)                
                {
                    case "food":                        
                        amount += article.Price * 100 + article.Price * 12;
                        break;                     
                    case "electronic":                  
                        amount += article.Price * 100 + article.Price * 20 + 4;    
                        break; 
                    case "desktop":                        
                        amount += article.Price * 100 + article.Price * 20;                       
                        break;
                    
                }          
                amountTotal += amount * oneBasketLineArticle.Number;
                
            }

            return amountTotal;

        }
        
        public static ArticleDatabase GetArticleDatabase(string id)        
        {  
            var codeBase = Assembly.GetExecutingAssembly().CodeBase; 
            var uri = new UriBuilder(codeBase);                 
            var path = Uri.UnescapeDataString(uri.Path);   
            var assemblyDirectory = Path.GetDirectoryName(path);     
            var jsonPath = Path.Combine(assemblyDirectory, "articledatabase.json");        
            IList<ArticleDatabase> articleDatabases =
                JsonConvert.DeserializeObject<List<ArticleDatabase>>(File.ReadAllText(jsonPath));  
            // here your code implementation
            var article = articleDatabases.First(articleDatabase => articleDatabase.Id == id);
            return article;
        }

        public static ArticleDatabase GetArticleDatabaseMock(string id)
        {
            switch (id)             {        
                case "1":         
                return new ArticleDatabase {Id = "1", Price = 1, Stock = 35, Label = "Banana", Category = "food"};     
                case "2":                
                    return new ArticleDatabase
                {
                    Id = "2",             
                    Price = 500,          
                    Stock = 20,            
                    Label = "Fridge electrolux",        
                    Category = "electronic"
                    
                };        
                case "3":       
                    return new ArticleDatabase {Id = "3", Price = 49, Stock = 68, Label = "Chair", Category = "desktop"};          
                default:           
                    throw new NotImplementedException();
                
            }
        } 
        
    } 
}