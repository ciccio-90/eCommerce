using System;
using System.Collections.Generic;
using System.Linq;
using Infrastructure.Domain;
using eCommerce.Storefront.Model.Products;
using eCommerce.Storefront.Model.Shipping;

namespace eCommerce.Storefront.Model.Basket
{
    public class Basket : EntityBase<Guid>, IAggregateRoot
    {
        private readonly IList<BasketItem> _items;
        private DeliveryOption _deliveryOption;
        
        public Basket()
        {
            _items = new List<BasketItem>();
        }

        public new Guid Id { get; set; }

        public int NumberOfItems
        {
            get
            {
                return _items.Sum(i => i.Qty);
            }
        }

        public decimal BasketTotal
        {
            get { return ItemsTotal + DeliveryCost(); }
        }

        public decimal ItemsTotal
        {
            get { return _items.Sum(i => i.Qty * i.Product.Price); }
        }
        
        public void Add(Product product)
        {
            if (BasketContainsAnItemFor(product))
            {
                GetItemFor(product).IncreaseItemQtyBy(1);
            }
            else
            {
                _items.Add(new BasketItem(product, this, 1));
            }
        }

        public BasketItem GetItemFor(Product product)
        {
            return _items.FirstOrDefault(i => i.Contains(product));
        }

        private bool BasketContainsAnItemFor(Product product)
        {
            return _items.Any(i => i.Contains(product));
        }

        public void Remove(Product product)
        {
            if (BasketContainsAnItemFor(product))
            {
                _items.Remove(GetItemFor(product));
            }
        }

        public void ChangeQtyOfProduct(int qty, Product product)
        {
            if (BasketContainsAnItemFor(product))
            {
                GetItemFor(product).ChangeItemQtyTo(qty);
            }
        }

        public int NumberOfItemsInBasket()
        {
            return _items.Sum(i => i.Qty);
        }

        public IEnumerable<BasketItem> Items
        {
            get { return _items; }
        }

        public decimal DeliveryCost()
        {
            return DeliveryOption.GetDeliveryChargeForBasketTotalOf(ItemsTotal);
        }

        public DeliveryOption DeliveryOption
        {
            get { return _deliveryOption; }
        }

        public void SetDeliveryOption(DeliveryOption deliveryOption)
        {
            _deliveryOption = deliveryOption;
        }

        protected override void Validate()
        {
            if (DeliveryOption == null)
            {
                AddBrokenRule(BasketBusinessRules.DeliveryOptionRequired);
            }

            foreach (BasketItem item in Items)
            {
                if (item.GetBrokenRules().Any())
                {
                    AddBrokenRule(BasketBusinessRules.ItemInvalid);
                }
            }
        }
    }
}
