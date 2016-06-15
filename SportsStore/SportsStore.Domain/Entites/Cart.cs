namespace SportsStore.Domain.Entites
{
    using System.Collections.Generic;
    using System.Linq;

    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        public IEnumerable<CartLine> Lines
        {
            get
            { return lineCollection; }
        }

        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection
                .Where(p => p.Product.ProductID == product.ProductID)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductID == product.ProductID);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(x => x.Quantity * x.Product.Price);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }
    }
}
