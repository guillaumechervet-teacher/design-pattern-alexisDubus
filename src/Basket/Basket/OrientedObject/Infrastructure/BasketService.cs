using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Basket.OrientedObject.Domain;
using Newtonsoft.Json;

namespace Basket.OrientedObject.Infrastructure {

    public class BasketService
    {
        public Basket1 GetBasket(IList<BasketLineArticle> basketLineArticles)
        {
            var list = new List<BasketLine>();
            foreach (var basketLineArticle in basketLineArticles)
            {
                // cahrge articleDatabase depuis la bdd
                
                // map artcle du domaine métier
            }
            return new Basket1(list);
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
    }
} 