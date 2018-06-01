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
        public Domain.Basket GetBasket(IList<BasketLineArticle> basketLineArticles)
        {
            var list = new List<BasketLine>();
            int i = 0;
            foreach (var basketLineArticle in basketLineArticles)
            {
                // cahrge articleDatabase depuis la bdd
                ArticleDatabase db = GetArticleDatabase(basketLineArticle.Id);
                list.Add(new BasketLine(new  Article(db.Price, db.Category), i));
                i++;
                //list.Add(db);
                // map artcle du domaine métier
            }
            return new Domain.Basket(list);
        }
        
         
        public ArticleDatabase GetArticleDatabase(string id)        
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