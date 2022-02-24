using System.Collections.Generic;
using System.Text;

namespace eCommerce.Storefront.Model
{
    public abstract class EntityBase<TId>
    {
        private readonly List<BusinessRule> _brokenRules = new List<BusinessRule>();

        public TId Id { get; set; }

        protected abstract void Validate();

        public void ThrowExceptionIfInvalid()
        {
            _brokenRules.Clear();
            Validate();

            if (_brokenRules.Count > 0)
            {
                StringBuilder issues = new StringBuilder();

                foreach (BusinessRule businessRule in _brokenRules)
                {
                    issues.AppendLine(businessRule.Rule);
                }

                throw new EntityBaseIsInvalidException(issues.ToString());
            }
        }

        public IEnumerable<BusinessRule> GetBrokenRules()
        {
            _brokenRules.Clear();
            Validate();

            return _brokenRules;
        }

        protected void AddBrokenRule(BusinessRule businessRule)
        {
            _brokenRules.Add(businessRule);
        }

        public override bool Equals(object obj)
        {
            return obj is EntityBase<TId> && this == (EntityBase<TId>)obj;
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }
    }
}