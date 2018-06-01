namespace Basket.OrientedObject.Domain
{
    public class BasketLine
    {
        private readonly Article _article;
        private readonly int _number;

        public BasketLine(Article article, int number)
        {
            _article = article;
            _number = number;
        }

        public int Calculate()
        {
            return _article.Calculate() * _number;
        }
    }
}