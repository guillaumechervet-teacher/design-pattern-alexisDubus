using System.Collections;
using System.Collections.Generic;

namespace Basket.OrientedObject.Domain
{
    public class Basket1
    {
        private readonly IList<BasketLine> _basketLine;

        public Basket1(IList<BasketLine> basketLine)
        {
            _basketLine = basketLine;
        }

        public int Calculate()
        {
            var total = 0;
            foreach (var basketLine in _basketLine)
            {
                total += basketLine.Calculate();
            }

            return total;
        }
        
    }
}