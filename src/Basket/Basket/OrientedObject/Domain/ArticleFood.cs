namespace Basket.OrientedObject.Domain
{
    public class ArticleFood : ArticleBase
    {
        public ArticleFood(string id, int price) : base(id, price)
        {
        }

        public override string Category => "food";

        public override int CalculateAmout()
        {
            return Price * 100 + Price * 12;
        }
    }
}