using Dolphin.Web.ViewModel.Sales;

namespace Dolphin.Web.ViewModel.Person
{
    public class CountryRegionViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public TerritoryViewModel[] Territories { get; set; }
    }
}