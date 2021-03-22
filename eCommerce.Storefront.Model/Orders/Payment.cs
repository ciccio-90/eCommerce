using System;
using Infrastructure.Domain;

namespace eCommerce.Storefront.Model.Orders
{
    public class Payment : ValueObjectBase
    {
        private readonly DateTime _datePaid;
        private readonly string _transactionId;
        private readonly string _merchant;
        private readonly decimal _amount;

        public Payment(DateTime datePaid, string transactionId, string merchant, decimal amount)
        {
            _datePaid = datePaid;
            _transactionId = transactionId;
            _merchant = merchant;
            _amount = amount;

            ThrowExceptionIfInvalid();
        }

        public DateTime DatePaid
        {
            get { return _datePaid; }
        }

        public string TransactionId
        {
            get { return _transactionId; }
        }

        public string Merchant
        {
            get { return _merchant; }
        }

        public decimal Amount
        {
            get { return _amount; }
        }

        protected override void Validate()
        {
            if (string.IsNullOrEmpty(_transactionId))
            {
                AddBrokenRule(PaymentBusinessRules.TransactionIdRequired);
            }

            if (string.IsNullOrEmpty(_merchant))
            {
                AddBrokenRule(PaymentBusinessRules.MerchantRequired);
            }

            if (_amount < 0)
            {
                AddBrokenRule(PaymentBusinessRules.AmountValid);
            }
        }
    }
}
