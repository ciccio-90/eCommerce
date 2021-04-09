using Infrastructure.Domain;
using eCommerce.Storefront.Model.Products;

namespace eCommerce.Storefront.Model.Basket
{
    public class BasketItem : EntityBase<long>
    {
        private int _qty;
        private readonly Product _product;
        private readonly Basket _basket;

        public BasketItem()
        {
        }
        
        public BasketItem(Product product, Basket basket, int qty)
        {
            _product = product;
            _basket = basket;
            _qty = qty;
        }

        public decimal LineTotal()
        {
            return Product.Price * Qty;
        }

        public int Qty { get { return _qty; } }
        
        public Product Product { get { return _product; } }
        
        public Basket Basket { get { return _basket; } }
        
        public bool Contains(Product product)
        {
            return Product.Id == product.Id;
        }

        public void IncreaseItemQtyBy(int qty)
        {
            _qty += qty;
        }

        public void ChangeItemQtyTo(int qty)
        {
            _qty = qty;
        }
        
        protected override void Validate()
        {    
            if (Qty < 0)
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(Qty), Rule = "The quantity of a basket item cannot be negative." });
            }

            if (Product == null)
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(Product), Rule = "A basket item must be related to a product." });
            }

            if (Basket == null)
            {
                AddBrokenRule(new BusinessRule() { Property = nameof(Basket), Rule = "A basket item must be related to a basket." });
            }
        }
    }
}
