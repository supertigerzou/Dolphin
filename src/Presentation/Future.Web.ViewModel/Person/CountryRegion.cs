using Future.Web.ViewModel.Sales;

namespace Future.Web.ViewModel.Person
{
    public class CountryRegionViewModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public TerritoryViewModel[] Territories { get; set; }
    }
}