using System.Collections.Generic;

namespace eCommerce.Storefront.Services.ViewModels
{
    public class RefinementGroup
    {
        public string Name { get; set; }
        public int GroupId { get; set; }
        public IEnumerable<Refinement> Refinements { get; set; }
    }
}