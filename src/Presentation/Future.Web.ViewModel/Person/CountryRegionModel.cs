using Future.Web.ViewModel.Sales;

namespace Future.Web.ViewModel.Person
{
    public class CountryRegionModel
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public TerritoryModel[] Territories { get; set; }
    }
}